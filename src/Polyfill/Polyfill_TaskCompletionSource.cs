#if !NET

namespace Polyfills;

using System;
using System.Threading;
using System.Threading.Tasks;

static partial class Polyfill
{
    /// <summary>
    /// Transitions the underlying <see cref="Task{TResult}"/> into the <see cref="TaskStatus.Canceled"/> state
    /// using the specified token.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1.setcanceled?view=net-11.0#system-threading-tasks-taskcompletionsource-1-setcanceled(system-threading-cancellationtoken)
    public static void SetCanceled<T>(
        this TaskCompletionSource<T> target,
        CancellationToken cancellationToken)
    {
        if (target.TrySetCanceled(cancellationToken))
        {
            return;
        }

        throw new InvalidOperationException("An attempt was made to transition a task to a final state when it had already completed.");
    }
}
#endif
