#if !NET5_0_OR_GREATER

namespace Polyfills;

using System;
using System.Diagnostics;

static partial class Polyfill
{
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
}
#endif
