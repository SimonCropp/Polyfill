namespace ConsumeCsPreview;

// Compile-only coverage for the C# `closed` modifier. The compiler emits
// IsClosedTypeAttribute on the closed type and
// CompilerFeatureRequiredAttribute("ClosedClasses") on its constructors,
// so this only compiles when both are polyfilled.

#region ClosedHierarchy

// Only types in this file can derive from JobStatus
closed class JobStatus;

sealed class Queued : JobStatus;

sealed class Running(int percentComplete) : JobStatus
{
    public int PercentComplete => percentComplete;
}

sealed class Failed(string error) : JobStatus
{
    public string Error => error;
}

static class ClosedHierarchy
{
    // No default arm: the switch is exhaustive because every direct descendant of
    // the closed JobStatus is handled. Without exhaustiveness this warns CS8509.
    public static string Describe(JobStatus status) =>
        status switch
        {
            Queued => "queued",
            Running running => $"{running.PercentComplete}% complete",
            Failed failed => $"failed: {failed.Error}"
        };
}

#endregion
