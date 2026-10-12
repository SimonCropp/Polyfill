namespace Polyfills;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
#if NETFRAMEWORK || NETSTANDARD || NETCOREAPP2X || WINDOWS_UWP

    /// <summary>
    /// Immediately stops the associated process, and optionally its child/descendent processes.
    /// Maps to <see cref="Process.Kill"/>.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.kill?view=net-11.0#system-diagnostics-process-kill(system-boolean)
    //Note: On Unix the descendants are found by running ps before anything is killed, so a child started in between is missed. When ps is not available only the process itself is killed.
    [SupportedOSPlatform("maccatalyst")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    public static void Kill(this Process target, bool entireProcessTree)
    {
        if (!entireProcessTree)
        {
            target.Kill();
            return;
        }

        ProcessTreeHelper.Kill(target);
    }

    [ExcludeFromCodeCoverage]
    [DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
    [global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
    static class ProcessTreeHelper
    {
        public static void Kill(Process target)
        {
            // throws InvalidOperationException when no process is associated
            if (target.HasExited)
            {
                return;
            }

            var errors = new List<Exception>();
            if (SignalHelper.IsWindows)
            {
                if (IsWindowsSelfOrDescendant(target))
                {
                    throw CallingProcessInTree();
                }

                KillWindowsTree(target, errors);
            }
            else
            {
                KillUnixTree(target, errors);
            }

            if (errors.Count != 0)
            {
                throw new AggregateException("Not all processes in process tree could be terminated.", errors);
            }
        }

        static InvalidOperationException CallingProcessInTree() =>
            new("Cannot be used to terminate a process tree containing the calling process.");

        // Windows keeps the id of the parent after the parent exits, so the tree can be walked from the top down:
        // each process is killed before its children are looked up, which stops it starting more.
        static void KillWindowsTree(Process process, List<Exception> errors)
        {
            var id = process.Id;
            var hasStartTime = TryGetStartTime(process, out var startTime);

            try
            {
                process.Kill();
            }
            catch (InvalidOperationException)
            {
                // already exited
            }
            catch (Win32Exception exception)
            {
                // access is denied to a process that is already terminating
                if (!HasExited(process))
                {
                    errors.Add(exception);
                }
            }

            if (!hasStartTime)
            {
                return;
            }

            foreach (var pair in GetWindowsParentIds())
            {
                if (pair.Value != id ||
                    pair.Key == id)
                {
                    continue;
                }

                Process child;
                try
                {
                    child = Process.GetProcessById(pair.Key);
                }
                catch (ArgumentException)
                {
                    continue;
                }

                using (child)
                {
                    // a parent id can outlive its process and be reused, so a child must have started after its parent
                    if (TryGetStartTime(child, out var childStartTime) &&
                        childStartTime >= startTime)
                    {
                        KillWindowsTree(child, errors);
                    }
                }
            }
        }

        static bool IsWindowsSelfOrDescendant(Process target)
        {
            var targetId = target.Id;
            using var current = Process.GetCurrentProcess();
            if (current.Id == targetId)
            {
                return true;
            }

            if (!TryGetStartTime(current, out var startTime))
            {
                return false;
            }

            var parents = GetWindowsParentIds();
            var id = current.Id;
            var visited = new HashSet<int>();
            // walk up from the calling process
            while (visited.Add(id) &&
                   parents.TryGetValue(id, out var parentId))
            {
                // a parent id can outlive its process and be reused, so a parent must have started before its child
                if (!TryGetStartTime(parentId, out var parentStartTime) ||
                    parentStartTime > startTime)
                {
                    return false;
                }

                if (parentId == targetId)
                {
                    return true;
                }

                id = parentId;
                startTime = parentStartTime;
            }

            return false;
        }

        // Unix reparents the children of a process that exits, so the descendants are collected before anything is killed.
        static void KillUnixTree(Process target, List<Exception> errors)
        {
            var targetId = target.Id;
            var parents = GetUnixParentIds();

            int currentId;
            using (var current = Process.GetCurrentProcess())
            {
                currentId = current.Id;
            }

            var descendants = new List<int>();
            var pending = new Queue<int>();
            pending.Enqueue(targetId);
            var visited = new HashSet<int> {targetId};
            while (pending.Count > 0)
            {
                var id = pending.Dequeue();
                if (id == currentId)
                {
                    throw CallingProcessInTree();
                }

                foreach (var pair in parents)
                {
                    if (pair.Value == id &&
                        visited.Add(pair.Key))
                    {
                        descendants.Add(pair.Key);
                        pending.Enqueue(pair.Key);
                    }
                }
            }

            try
            {
                target.Kill();
            }
            catch (InvalidOperationException)
            {
                // already exited
            }
            catch (Win32Exception exception)
            {
                errors.Add(exception);
            }

            foreach (var id in descendants)
            {
                try
                {
                    SignalHelper.Send(id, (PosixSignal) SignalHelper.SigKill);
                }
                catch (Win32Exception exception)
                {
                    errors.Add(exception);
                }
            }
        }

        static bool HasExited(Process process)
        {
            try
            {
                return process.WaitForExit(500);
            }
            catch (Exception exception) when (exception is InvalidOperationException or Win32Exception)
            {
                return false;
            }
        }

        static bool TryGetStartTime(int id, out DateTime startTime)
        {
            try
            {
                using var process = Process.GetProcessById(id);
                return TryGetStartTime(process, out startTime);
            }
            catch (ArgumentException)
            {
                startTime = default;
                return false;
            }
        }

        static bool TryGetStartTime(Process process, out DateTime startTime)
        {
            try
            {
                startTime = process.StartTime;
                return true;
            }
            catch (Exception exception) when (exception is InvalidOperationException or Win32Exception or NotSupportedException)
            {
                // exited, or access is denied
                startTime = default;
                return false;
            }
        }

        // process id => parent process id
        static Dictionary<int, int> GetWindowsParentIds()
        {
            var parents = new Dictionary<int, int>();
            var snapshot = CreateToolhelp32Snapshot(snapProcess, 0);
            if (snapshot == invalidHandle)
            {
                return parents;
            }

            try
            {
                var entry = new ProcessEntry
                {
                    Size = (uint) Marshal.SizeOf(typeof(ProcessEntry))
                };
                if (!Process32FirstW(snapshot, ref entry))
                {
                    return parents;
                }

                do
                {
                    parents[(int) entry.ProcessId] = (int) entry.ParentProcessId;
                } while (Process32NextW(snapshot, ref entry));
            }
            finally
            {
                CloseHandle(snapshot);
            }

            return parents;
        }

        // process id => parent process id
        static Dictionary<int, int> GetUnixParentIds()
        {
            var parents = new Dictionary<int, int>();
            string output;
            try
            {
                using var process = Process.Start(
                    new ProcessStartInfo
                    {
                        FileName = "ps",
                        Arguments = "-A -o pid=,ppid=",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    })!;
                output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
            }
            catch (Exception exception) when (exception is Win32Exception or InvalidOperationException)
            {
                // ps is not available, so only the process itself can be killed
                return parents;
            }

            foreach (var line in output.Split('\n'))
            {
                var parts = line.Split(new[] {' ', '\t', '\r'}, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out var id) &&
                    int.TryParse(parts[1], out var parentId))
                {
                    parents[id] = parentId;
                }
            }

            return parents;
        }

        const uint snapProcess = 2;
        static readonly IntPtr invalidHandle = new(-1);

#pragma warning disable CS0649, CS0169
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        struct ProcessEntry
        {
            public uint Size;
            public uint Usage;
            public uint ProcessId;
            public IntPtr DefaultHeapId;
            public uint ModuleId;
            public uint Threads;
            public uint ParentProcessId;
            public int PriorityClassBase;
            public uint Flags;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string ExeFile;
        }
#pragma warning restore CS0649, CS0169

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr CreateToolhelp32Snapshot(uint flags, uint processId);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern bool Process32FirstW(IntPtr snapshot, ref ProcessEntry entry);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern bool Process32NextW(IntPtr snapshot, ref ProcessEntry entry);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);
    }

#endif

#if !NET

    /// <summary>
    /// Instructs the Process component to wait for the associated process to exit, or
    /// for the <paramref name="cancellationToken"/> to be canceled.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexitasync?view=net-11.0
    public static async Task WaitForExitAsync(this Process target, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (target.HasExited)
        {
            return;
        }

        try
        {
            target.EnableRaisingEvents = true;
        }
        catch (InvalidOperationException) when (target.HasExited)
        {
            return;
        }

        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);

        EventHandler handler = (_, _) => tcs.TrySetResult(null);
        target.Exited += handler;

        try
        {
            if (!target.HasExited)
            {
                using (cancellationToken.UnsafeRegister(static (s, cancellationToken) => ((TaskCompletionSource<object>) s!).TrySetCanceled(cancellationToken), tcs))
                {
                    await tcs.Task.ConfigureAwait(false);
                }
            }
        }
        finally
        {
            target.Exited -= handler;
        }
    }
#endif

#if !NET11_0_OR_GREATER

#if FeatureValueTuple
    /// <summary>
    /// Reads the standard output and standard error of the process as text, waiting for the process to exit.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.readalltext?view=net-11.0
    public static (string StandardOutput, string StandardError) ReadAllText(this Process target, TimeSpan? timeout = default)
    {
        var stdoutTask = target.StandardOutput.ReadToEndAsync();
        var stderrTask = target.StandardError.ReadToEndAsync();
        WaitForExitOrThrow(target, timeout);
        return (stdoutTask.GetAwaiter().GetResult(), stderrTask.GetAwaiter().GetResult());
    }

    /// <summary>
    /// Asynchronously reads the standard output and standard error of the process as text, waiting for the process to exit.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.readalltextasync?view=net-11.0
    public static async Task<(string StandardOutput, string StandardError)> ReadAllTextAsync(this Process target, CancellationToken cancellationToken = default)
    {
        var stdoutTask = target.StandardOutput.ReadToEndAsync();
        var stderrTask = target.StandardError.ReadToEndAsync();
        await target.WaitForExitAsync(cancellationToken).ConfigureAwait(false);
        return (await stdoutTask.ConfigureAwait(false), await stderrTask.ConfigureAwait(false));
    }

    /// <summary>
    /// Reads the standard output and standard error of the process as bytes, waiting for the process to exit.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.readallbytes?view=net-11.0
    public static (byte[] StandardOutput, byte[] StandardError) ReadAllBytes(this Process target, TimeSpan? timeout = default)
    {
        var outMs = new MemoryStream();
        var errMs = new MemoryStream();
        var outTask = target.StandardOutput.BaseStream.CopyToAsync(outMs);
        var errTask = target.StandardError.BaseStream.CopyToAsync(errMs);
        WaitForExitOrThrow(target, timeout);
        Task.WaitAll(outTask, errTask);
        return (outMs.ToArray(), errMs.ToArray());
    }

    /// <summary>
    /// Asynchronously reads the standard output and standard error of the process as bytes, waiting for the process to exit.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.readallbytesasync?view=net-11.0
    public static async Task<(byte[] StandardOutput, byte[] StandardError)> ReadAllBytesAsync(this Process target, CancellationToken cancellationToken = default)
    {
        var outMs = new MemoryStream();
        var errMs = new MemoryStream();
        var outTask = target.StandardOutput.BaseStream.CopyToAsync(outMs, 81920, cancellationToken);
        var errTask = target.StandardError.BaseStream.CopyToAsync(errMs, 81920, cancellationToken);
        await target.WaitForExitAsync(cancellationToken).ConfigureAwait(false);
        await Task.WhenAll(outTask, errTask).ConfigureAwait(false);
        return (outMs.ToArray(), errMs.ToArray());
    }
#endif

    /// <summary>
    /// Reads the standard output and standard error of the process line-by-line, waiting for the process to exit.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.readalllines?view=net-11.0
    //Note: The timeout is measured from the point enumeration starts, since the returned sequence is lazy.
    public static IEnumerable<ProcessOutputLine> ReadAllLines(this Process target, TimeSpan? timeout = default)
    {
        var queue = new System.Collections.Concurrent.ConcurrentQueue<ProcessOutputLine>();
        var signal = new SemaphoreSlim(0);
        var sync = new object();
        var stdoutDone = false;
        var stderrDone = false;

        DataReceivedEventHandler outHandler = (_, e) =>
        {
            if (e.Data is null)
            {
                lock (sync) stdoutDone = true;
            }
            else
            {
                queue.Enqueue(new(e.Data, false));
            }
            signal.Release();
        };
        DataReceivedEventHandler errHandler = (_, e) =>
        {
            if (e.Data is null)
            {
                lock (sync) stderrDone = true;
            }
            else
            {
                queue.Enqueue(new(e.Data, true));
            }
            signal.Release();
        };

        target.OutputDataReceived += outHandler;
        target.ErrorDataReceived += errHandler;
        target.BeginOutputReadLine();
        target.BeginErrorReadLine();

        var watch = Stopwatch.StartNew();

        try
        {
            while (true)
            {
                if (!signal.Wait(RemainingMilliseconds(watch, timeout)))
                {
                    throw new TimeoutException("The process did not exit within the specified timeout.");
                }

                while (queue.TryDequeue(out var line))
                {
                    yield return line;
                }

                bool done;
                lock (sync)
                {
                    done = stdoutDone && stderrDone;
                }

                if (done)
                {
                    while (queue.TryDequeue(out var line))
                    {
                        yield return line;
                    }

                    yield break;
                }
            }
        }
        finally
        {
            target.OutputDataReceived -= outHandler;
            target.ErrorDataReceived -= errHandler;
            // signal is not disposed: a handler already running can still Release it after unsubscribe
        }
    }

    static int RemainingMilliseconds(Stopwatch watch, TimeSpan? timeout)
    {
        if (timeout is not { } value)
        {
            return Timeout.Infinite;
        }

        var remaining = value.TotalMilliseconds - watch.Elapsed.TotalMilliseconds;
        if (remaining <= 0)
        {
            return 0;
        }

        if (remaining > int.MaxValue)
        {
            return Timeout.Infinite;
        }

        return (int) remaining;
    }

#if FeatureAsyncInterfaces
    /// <summary>
    /// Asynchronously reads the standard output and standard error of the process line-by-line, waiting for the process to exit.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.readalllinesasync?view=net-11.0
    public static async IAsyncEnumerable<ProcessOutputLine> ReadAllLinesAsync(
        this Process target,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var queue = new System.Collections.Concurrent.ConcurrentQueue<ProcessOutputLine>();
        var signal = new SemaphoreSlim(0);
        var sync = new object();
        var stdoutDone = false;
        var stderrDone = false;

        DataReceivedEventHandler outHandler = (_, e) =>
        {
            if (e.Data is null)
            {
                lock (sync) stdoutDone = true;
            }
            else
            {
                queue.Enqueue(new(e.Data, false));
            }
            signal.Release();
        };
        DataReceivedEventHandler errHandler = (_, e) =>
        {
            if (e.Data is null)
            {
                lock (sync) stderrDone = true;
            }
            else
            {
                queue.Enqueue(new(e.Data, true));
            }
            signal.Release();
        };

        target.OutputDataReceived += outHandler;
        target.ErrorDataReceived += errHandler;
        target.BeginOutputReadLine();
        target.BeginErrorReadLine();

        try
        {
            while (true)
            {
                await signal.WaitAsync(cancellationToken).ConfigureAwait(false);
                while (queue.TryDequeue(out var line))
                {
                    yield return line;
                }
                bool done;
                lock (sync)
                {
                    done = stdoutDone && stderrDone;
                }
                if (done)
                {
                    while (queue.TryDequeue(out var line))
                    {
                        yield return line;
                    }
                    yield break;
                }
            }
        }
        finally
        {
            target.OutputDataReceived -= outHandler;
            target.ErrorDataReceived -= errHandler;
            // signal is not disposed: a handler already running can still Release it after unsubscribe
        }
    }
#endif

    /// <summary>
    /// Sends the specified POSIX signal to the associated process.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.signal?view=net-11.0
    //Note: On Windows, as on net11, only SIGKILL is supported and is mapped to Process.Kill. All other signals throw PlatformNotSupportedException.
    //Note: PosixSignal.SIGKILL was added in net11, so it cannot be named on earlier target frameworks. Its numeric value, (PosixSignal)(-11), is accepted.
    [SupportedOSPlatform("maccatalyst")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    public static bool Signal(this Process target, PosixSignal signal)
    {
        if (SignalHelper.IsWindows)
        {
            if (target.HasExited)
            {
                // matches net11, where opening a handle to an exited process fails
                return false;
            }

            if ((int) signal != SignalHelper.SigKill)
            {
                throw new PlatformNotSupportedException();
            }

            try
            {
                target.Kill();
                return true;
            }
            catch (InvalidOperationException)
            {
                // the process exited between the check and the kill
                return false;
            }
        }

        return SignalHelper.Send(target.Id, signal);
    }

    /// <summary>
    /// Instructs the Process component to wait for the associated process to exit, and returns its exit status.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexitstatus?view=net-11.0
    //Note: On Unix the terminating signal is derived from the exit code, which is 128 plus the signal number, instead of from the raw wait status. A process that exits normally with, for example, code 143 is therefore reported as terminated by SIGTERM.
    [SupportedOSPlatform("maccatalyst")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    public static ProcessExitStatus WaitForExitStatus(this Process target)
    {
        target.WaitForExit();
        return ToExitStatus(target.ExitCode);
    }

    /// <summary>
    /// Instructs the Process component to wait up to <paramref name="timeout"/> for the associated process to exit,
    /// and returns a value indicating whether it exited.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.trywaitforexitstatus?view=net-11.0
    //Note: On Unix the terminating signal is derived from the exit code, which is 128 plus the signal number, instead of from the raw wait status. A process that exits normally with, for example, code 143 is therefore reported as terminated by SIGTERM.
    [SupportedOSPlatform("maccatalyst")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    public static bool TryWaitForExitStatus(this Process target, TimeSpan timeout, [NotNullWhen(true)] out ProcessExitStatus? exitStatus)
    {
        var milliseconds = ToTimeoutMilliseconds(timeout);
        if (!target.WaitForExit(milliseconds))
        {
            exitStatus = null;
            return false;
        }

        exitStatus = ToExitStatus(target.ExitCode);
        return true;
    }

    /// <summary>
    /// Instructs the Process component to wait for the associated process to exit, or for the
    /// <paramref name="cancellationToken"/> to be canceled, and returns its exit status.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexitstatusasync?view=net-11.0
    //Note: On Unix the terminating signal is derived from the exit code, which is 128 plus the signal number, instead of from the raw wait status. A process that exits normally with, for example, code 143 is therefore reported as terminated by SIGTERM.
    [SupportedOSPlatform("maccatalyst")]
    [UnsupportedOSPlatform("ios")]
    [UnsupportedOSPlatform("tvos")]
    public static async Task<ProcessExitStatus> WaitForExitStatusAsync(this Process target, CancellationToken cancellationToken = default)
    {
        await target.WaitForExitAsync(cancellationToken).ConfigureAwait(false);
        return ToExitStatus(target.ExitCode);
    }

    static ProcessExitStatus ToExitStatus(int exitCode)
    {
        if (!SignalHelper.IsWindows)
        {
            // Unix reports termination by a signal as an exit code of 128 plus the signal number
            var signal = SignalHelper.ToPosixSignal(exitCode - 128);
            if (signal != null)
            {
                return new(exitCode, canceled: false, signal);
            }
        }

        return new(exitCode, canceled: false);
    }

    static int ToTimeoutMilliseconds(TimeSpan timeout)
    {
        var milliseconds = (long) timeout.TotalMilliseconds;
        if (milliseconds < -1 ||
            milliseconds > int.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(timeout), timeout, "Timeout must be -1 milliseconds, or between 0 and Int32.MaxValue milliseconds.");
        }

        return (int) milliseconds;
    }

    [ExcludeFromCodeCoverage]
    [DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
    [global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
    static class SignalHelper
    {
        /// <summary>
        /// The value of PosixSignal.SIGKILL, which was added in net11 and so cannot be named on earlier target frameworks.
        /// </summary>
        public const int SigKill = -11;

        const int ESRCH = 3;

#if FeatureRuntimeInformation
        public static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        // macOS and the BSDs number SIGCHLD, SIGCONT and SIGTSTP differently to Linux
        static readonly bool isBsd = RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                                     RuntimeInformation.OSDescription.ToLower().Contains("bsd");
#else
        public static readonly bool IsWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;

        static readonly bool isBsd = Environment.OSVersion.Platform == PlatformID.MacOSX;
#endif

        public static bool Send(int processId, PosixSignal signal)
        {
            var number = ToSignalNumber(signal);
            if (number == 0)
            {
                throw new PlatformNotSupportedException();
            }

            if (kill(processId, number) == 0)
            {
                return true;
            }

            var error = Marshal.GetLastWin32Error();
            if (error == ESRCH)
            {
                // the process has already exited, or never existed
                return false;
            }

            throw new Win32Exception(error);
        }

        static int ToSignalNumber(PosixSignal signal)
        {
            var value = (int) signal;
            // positive values are platform signal numbers
            if (value > 0)
            {
                return value;
            }

            return value switch
            {
                -1 => 1, // SIGHUP
                -2 => 2, // SIGINT
                -3 => 3, // SIGQUIT
                -4 => 15, // SIGTERM
                -5 => isBsd ? 20 : 17, // SIGCHLD
                -6 => isBsd ? 19 : 18, // SIGCONT
                -7 => 28, // SIGWINCH
                -8 => 21, // SIGTTIN
                -9 => 22, // SIGTTOU
                -10 => isBsd ? 18 : 20, // SIGTSTP
                SigKill => 9, // SIGKILL
                _ => 0
            };
        }

        public static PosixSignal? ToPosixSignal(int number) =>
            number switch
            {
                1 => PosixSignal.SIGHUP,
                2 => PosixSignal.SIGINT,
                3 => PosixSignal.SIGQUIT,
                9 => (PosixSignal) SigKill,
                15 => PosixSignal.SIGTERM,
                17 => isBsd ? null : PosixSignal.SIGCHLD,
                18 => isBsd ? PosixSignal.SIGTSTP : PosixSignal.SIGCONT,
                19 => isBsd ? PosixSignal.SIGCONT : null,
                20 => isBsd ? PosixSignal.SIGCHLD : PosixSignal.SIGTSTP,
                21 => PosixSignal.SIGTTIN,
                22 => PosixSignal.SIGTTOU,
                28 => PosixSignal.SIGWINCH,
                _ => null
            };

        [DllImport("libc", EntryPoint = "kill", SetLastError = true)]
        static extern int kill(int pid, int signal);
    }

    static void WaitForExitOrThrow(Process target, TimeSpan? timeout)
    {
        if (!TryWaitForExit(target, timeout))
        {
            throw new TimeoutException("The process did not exit within the specified timeout.");
        }
    }

    static bool TryWaitForExit(Process target, TimeSpan? timeout)
    {
        if (timeout is { } value)
        {
            var ms = (long)value.TotalMilliseconds;
            if (ms >= 0 && ms <= int.MaxValue)
            {
                return target.WaitForExit((int)ms);
            }
        }

        target.WaitForExit();
        return true;
    }
#endif
}
