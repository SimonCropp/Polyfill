
#pragma warning disable


namespace System.Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

/// <summary>
///   Specifies that the method or property will ensure that the listed field and property members have
///   not-<see langword="null"/> values.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Method |
             Targets.Property,
    Inherited = false,
    AllowMultiple = true)]
sealed class MemberNotNullAttribute :
    Attribute
{
    /// <summary>
    ///   Gets field or property member names.
    /// </summary>
    public string[] Members { get; }

    /// <summary>
    ///   Initializes the attribute with a field or property member.
    /// </summary>
    /// <param name="member">
    ///   The field or property member that is promised to be not-null.
    /// </param>
    public MemberNotNullAttribute(string member) =>
        Members = [member];

    /// <summary>
    ///   Initializes the attribute with the list of field and property members.
    /// </summary>
    /// <param name="members">
    ///   The list of field and property members that are promised to be not-null.
    /// </param>
    public MemberNotNullAttribute(params string[] members) =>
        Members = members;
}
