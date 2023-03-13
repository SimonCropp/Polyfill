#if(!NET7_0_OR_GREATER)

namespace System.Diagnostics.CodeAnalysis;

/// <summary>
/// Specifies that this constructor sets all required members for the current type, and callers
/// do not need to set any required members themselves.
/// </summary>
[AttributeUsage(AttributeTargets.Constructor)]

sealed class SetsRequiredMembersAttribute : Attribute
{
}
#endif