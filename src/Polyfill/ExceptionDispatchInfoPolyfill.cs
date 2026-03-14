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
            var field = typeof(Exception).GetField("_stackTraceString", BindingFlags.NonPublic | BindingFlags.Instance);
            field?.SetValue(source, stackTrace);
            return source;
        }
    }
}

#endif
