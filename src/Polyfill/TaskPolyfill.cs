#if !NET10_0_OR_GREATER && FeatureMemory

namespace Polyfills;

using System;
using System.Threading.Tasks;

static partial class Polyfill
{
    extension(Task)
    {
        /// <summary>
        /// Creates a task that will complete when all of the supplied tasks have completed.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.whenall?view=net-11.0#system-threading-tasks-task-whenall(system-readonlyspan((system-threading-tasks-task)))
        public static Task WhenAll(ReadOnlySpan<Task> tasks) =>
            Task.WhenAll(tasks.ToArray());

        /// <summary>
        /// Creates a task that will complete when all of the supplied tasks have completed.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.whenall?view=net-11.0#system-threading-tasks-task-whenall-1(system-readonlyspan((system-threading-tasks-task((-0)))))
        public static Task<TResult[]> WhenAll<TResult>(ReadOnlySpan<Task<TResult>> tasks) =>
            Task.WhenAll(tasks.ToArray());

        /// <summary>
        /// Creates a task that will complete when any of the supplied tasks have completed.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.whenany?view=net-11.0#system-threading-tasks-task-whenany(system-readonlyspan((system-threading-tasks-task)))
        public static Task<Task> WhenAny(ReadOnlySpan<Task> tasks) =>
            Task.WhenAny(tasks.ToArray());

        /// <summary>
        /// Creates a task that will complete when any of the supplied tasks have completed.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.whenany?view=net-11.0#system-threading-tasks-task-whenany-1(system-readonlyspan((system-threading-tasks-task((-0)))))
        public static Task<Task<TResult>> WhenAny<TResult>(ReadOnlySpan<Task<TResult>> tasks) =>
            Task.WhenAny(tasks.ToArray());
    }
}

#endif
