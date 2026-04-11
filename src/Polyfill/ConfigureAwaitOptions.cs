#if !NET8_0_OR_GREATER

namespace System.Threading.Tasks;

/// <summary>
/// Options to control behavior when awaiting.
/// </summary>
//Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.configureawaitoptions?view=net-11.0
[Flags]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
enum ConfigureAwaitOptions
{
    /// <summary>
    /// No options specified.
    /// </summary>
    None = 0,

    /// <summary>
    /// Attempts to marshal the continuation back to the original <see cref="System.Threading.SynchronizationContext"/>
    /// or <see cref="System.Threading.Tasks.TaskScheduler"/> present on the originating thread at the time of the await.
    /// </summary>
    ContinueOnCapturedContext = 1,

    /// <summary>
    /// Avoids throwing an exception at the completion of awaiting a <see cref="Task"/> that ends
    /// in the <see cref="TaskStatus.Faulted"/> or <see cref="TaskStatus.Canceled"/> state.
    /// </summary>
    SuppressThrowing = 2,

    /// <summary>
    /// Forces an await on an already completed <see cref="Task"/> to behave as if the <see cref="Task"/>
    /// wasn't yet completed, such that the current asynchronous method will be forced to yield its execution.
    /// </summary>
    ForceYielding = 4
}

#else
[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(System.Threading.Tasks.ConfigureAwaitOptions))]
#endif
