namespace Polyfills;

using System;
using System.Diagnostics;

static partial class Polyfill
{
#if !NET5_0_OR_GREATER
    static int processId;

    extension(Environment)
    {
        /// <summary>
        /// Gets the unique identifier for the current process.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.environment.processid?view=net-11.0#system-environment-processid
        public static int ProcessId
        {
            get
            {
                if (processId == 0)
                {
                    using var process = Process.GetCurrentProcess();
                    processId = process.Id;
                }

                return processId;
            }
        }
    }
#endif

#if !NET6_0_OR_GREATER
    static string? processPath;

    extension(Environment)
    {
        /// <summary>
        /// Gets the path of the executable that started the currently executing process.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.environment.processpath?view=net-11.0#system-environment-processpath
        public static string? ProcessPath
        {
            get
            {
                if (processPath is null)
                {
                    using var process = Process.GetCurrentProcess();
                    processPath = process.MainModule?.FileName;
                }

                return processPath;
            }
        }
    }
#endif
}
