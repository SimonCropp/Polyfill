#if !NET5_0_OR_GREATER

#pragma warning disable

namespace Polyfills;

using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

static partial class Polyfill
{
    extension(ExceptionDispatchInfo)
    {
        /// <summary>
        /// Stores the current stack trace into the specified <see cref="Exception"/> instance.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.runtime.exceptionservices.exceptiondispatchinfo.setcurrentstacktrace?view=net-11.0
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static Exception SetCurrentStackTrace(Exception source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var stackTrace = new StackTrace(1, true).ToString();

            // Store it as the "remote" stack trace, which the runtime keeps and prepends to Exception.StackTrace.
            // _stackTraceString would be cleared as soon as the exception is thrown.
            var remoteField = typeof(Exception).GetField("_remoteStackTraceString", BindingFlags.NonPublic | BindingFlags.Instance);
            if (remoteField != null)
            {
                var remoteStackTrace =
                    stackTrace.TrimEnd('\r', '\n') +
                    Environment.NewLine +
                    "--- End of stack trace from previous location where exception was thrown ---" +
                    Environment.NewLine;
                remoteField.SetValue(source, remoteStackTrace);
                return source;
            }

            var field = typeof(Exception).GetField("_stackTraceString", BindingFlags.NonPublic | BindingFlags.Instance);
            field?.SetValue(source, stackTrace);
            return source;
        }
    }
}

#endif
