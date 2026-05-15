#if !NET11_0_OR_GREATER

namespace Polyfills;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
    extension(Process)
    {
        /// <summary>
        /// Starts the process described by <paramref name="startInfo"/>, waits for it to exit, and returns the exit status.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.run?view=net-11.0#system-diagnostics-process-run(system-diagnostics-processstartinfo-system-nullable((system-timespan)))
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public static ProcessExitStatus Run(ProcessStartInfo startInfo, TimeSpan? timeout = default)
        {
            using var process = StartOrThrow(startInfo);
            WaitForExitOrThrow(process, timeout);
            return new(process.ExitCode, canceled: false);
        }

        /// <summary>
        /// Starts the process described by <paramref name="fileName"/> and <paramref name="arguments"/>, waits for it to exit, and returns the exit status.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.run?view=net-11.0#system-diagnostics-process-run(system-string-system-collections-generic-ilist((system-string))-system-nullable((system-timespan)))
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public static ProcessExitStatus Run(string fileName, IList<string>? arguments = null, TimeSpan? timeout = default) =>
            Process.Run(BuildStartInfo(fileName, arguments), timeout);

        /// <summary>
        /// Asynchronously starts the process described by <paramref name="startInfo"/>, waits for it to exit, and returns the exit status.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.runasync?view=net-11.0#system-diagnostics-process-runasync(system-diagnostics-processstartinfo-system-threading-cancellationtoken)
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public static async Task<ProcessExitStatus> RunAsync(ProcessStartInfo startInfo, CancellationToken cancellationToken = default)
        {
            using var process = StartOrThrow(startInfo);
            var canceled = false;
            try
            {
                await process.WaitForExitAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                canceled = true;
                TryKill(process);
                try
                {
                    await process.WaitForExitAsync();
                }
                catch
                {
                }
            }
            return new(canceled ? -1 : process.ExitCode, canceled);
        }

        /// <summary>
        /// Asynchronously starts the process described by <paramref name="fileName"/> and <paramref name="arguments"/>, waits for it to exit, and returns the exit status.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.runasync?view=net-11.0#system-diagnostics-process-runasync(system-string-system-collections-generic-ilist((system-string))-system-threading-cancellationtoken)
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public static Task<ProcessExitStatus> RunAsync(string fileName, IList<string>? arguments = null, CancellationToken cancellationToken = default) =>
            Process.RunAsync(BuildStartInfo(fileName, arguments), cancellationToken);

        /// <summary>
        /// Starts the process described by <paramref name="startInfo"/>, captures its standard output and standard error as text, waits for it to exit, and returns the captured output.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.runandcapturetext?view=net-11.0#system-diagnostics-process-runandcapturetext(system-diagnostics-processstartinfo-system-nullable((system-timespan)))
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public static ProcessTextOutput RunAndCaptureText(ProcessStartInfo startInfo, TimeSpan? timeout = default)
        {
            ConfigureForCapture(startInfo);
            using var process = StartOrThrow(startInfo);
            var stdoutTask = process.StandardOutput.ReadToEndAsync();
            var stderrTask = process.StandardError.ReadToEndAsync();
            var pid = process.Id;
            WaitForExitOrThrow(process, timeout);
            var status = new ProcessExitStatus(process.ExitCode, canceled: false);
            return new(status, stdoutTask.GetAwaiter().GetResult(), stderrTask.GetAwaiter().GetResult(), pid);
        }

        /// <summary>
        /// Starts the process described by <paramref name="fileName"/> and <paramref name="arguments"/>, captures its standard output and standard error as text, waits for it to exit, and returns the captured output.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.runandcapturetext?view=net-11.0#system-diagnostics-process-runandcapturetext(system-string-system-collections-generic-ilist((system-string))-system-nullable((system-timespan)))
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public static ProcessTextOutput RunAndCaptureText(string fileName, IList<string>? arguments = null, TimeSpan? timeout = default) =>
            Process.RunAndCaptureText(BuildStartInfo(fileName, arguments), timeout);

        /// <summary>
        /// Asynchronously starts the process described by <paramref name="startInfo"/>, captures its standard output and standard error as text, waits for it to exit, and returns the captured output.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.runandcapturetextasync?view=net-11.0#system-diagnostics-process-runandcapturetextasync(system-diagnostics-processstartinfo-system-threading-cancellationtoken)
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public static async Task<ProcessTextOutput> RunAndCaptureTextAsync(ProcessStartInfo startInfo, CancellationToken cancellationToken = default)
        {
            ConfigureForCapture(startInfo);
            using var process = StartOrThrow(startInfo);
            var stdoutTask = process.StandardOutput.ReadToEndAsync();
            var stderrTask = process.StandardError.ReadToEndAsync();
            var pid = process.Id;
            var canceled = false;
            try
            {
                await process.WaitForExitAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
                canceled = true;
                TryKill(process);
                try
                {
                    await process.WaitForExitAsync();
                }
                catch
                {
                }
            }
            var status = new ProcessExitStatus(canceled ? -1 : process.ExitCode, canceled);
            return new(status, await stdoutTask, await stderrTask, pid);
        }

        /// <summary>
        /// Asynchronously starts the process described by <paramref name="fileName"/> and <paramref name="arguments"/>, captures its standard output and standard error as text, waits for it to exit, and returns the captured output.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.runandcapturetextasync?view=net-11.0#system-diagnostics-process-runandcapturetextasync(system-string-system-collections-generic-ilist((system-string))-system-threading-cancellationtoken)
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public static Task<ProcessTextOutput> RunAndCaptureTextAsync(string fileName, IList<string>? arguments = null, CancellationToken cancellationToken = default) =>
            Process.RunAndCaptureTextAsync(BuildStartInfo(fileName, arguments), cancellationToken);

        /// <summary>
        /// Starts the process described by <paramref name="startInfo"/> in detached fashion and returns its process identifier.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.startandforget?view=net-11.0#system-diagnostics-process-startandforget(system-diagnostics-processstartinfo)
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public static int StartAndForget(ProcessStartInfo startInfo)
        {
            var process = StartOrThrow(startInfo);
            try
            {
                return process.Id;
            }
            finally
            {
                process.Dispose();
            }
        }

        /// <summary>
        /// Starts the process described by <paramref name="fileName"/> and <paramref name="arguments"/> in detached fashion and returns its process identifier.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.startandforget?view=net-11.0#system-diagnostics-process-startandforget(system-string-system-collections-generic-ilist((system-string)))
        [SupportedOSPlatform("maccatalyst")]
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("tvos")]
        public static int StartAndForget(string fileName, IList<string>? arguments = null) =>
            Process.StartAndForget(BuildStartInfo(fileName, arguments));
    }

    static Process StartOrThrow(ProcessStartInfo startInfo)
    {
        var process = Process.Start(startInfo);
        if (process is null)
        {
            throw new InvalidOperationException("Failed to start process.");
        }
        return process;
    }

    static ProcessStartInfo BuildStartInfo(string fileName, IList<string>? arguments)
    {
        var info = new ProcessStartInfo
        {
            FileName = fileName,
        };
        if (arguments is not null)
        {
            foreach (var arg in arguments)
            {
                info.ArgumentList.Add(arg);
            }
        }
        return info;
    }

    static void ConfigureForCapture(ProcessStartInfo startInfo)
    {
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
        startInfo.UseShellExecute = false;
    }

    static void TryKill(Process process)
    {
        try
        {
            if (!process.HasExited)
            {
                process.Kill();
            }
        }
        catch
        {
        }
    }
}

#endif
