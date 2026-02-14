#if !NET5_0_OR_GREATER && FeatureValueTask

namespace Polyfills;

using System.Threading.Tasks;

static partial class Polyfill
{
    extension(ValueTask)
    {
        /// <summary>Gets a task that has already completed successfully.</summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask.completedtask?view=net-11.0
        public static ValueTask CompletedTask => default;
    }
}
#endif
