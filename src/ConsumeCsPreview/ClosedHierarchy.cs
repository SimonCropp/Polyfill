namespace ConsumeCsPreview;

/// <summary>
/// Compile-only coverage for the C# `closed` modifier. The compiler emits
/// <c>IsClosedTypeAttribute</c> on the closed type and
/// <c>CompilerFeatureRequiredAttribute("ClosedClasses")</c> on its constructors,
/// so this only compiles when both are polyfilled.
/// </summary>
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
    /// <summary>
    /// No default arm: the switch is exhaustive because every direct descendant of the
    /// closed <see cref="JobStatus"/> is handled. Without exhaustiveness this warns CS8509,
    /// which is an error since TreatWarningsAsErrors is enabled.
    /// </summary>
    public static string Describe(JobStatus status) =>
        status switch
        {
            Queued => "queued",
            Running running => $"{running.PercentComplete}% complete",
            Failed failed => $"failed: {failed.Error}"
        };
}
