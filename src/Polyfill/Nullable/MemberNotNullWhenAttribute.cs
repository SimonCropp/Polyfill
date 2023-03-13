#if(NETSTANDARD2_0 || NETSTANDARD2_1 || NETFRAMEWORK || NETCOREAPP2_1 || NETCOREAPP2_0 || NETCOREAPP2_2 || NETCOREAPP3_1 || NETCOREAPP3_0)

namespace System.Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

/// <summary>
///   Specifies that the method or property will ensure that the listed field and property members have
///   non-<see langword="null"/> values when returning with the specified return value condition.
/// </summary>
[AttributeUsage(
    Targets.Method | Targets.Property,
    Inherited = false,
    AllowMultiple = true)]
[ExcludeFromCodeCoverage, DebuggerNonUserCode]
sealed class MemberNotNullWhenAttribute : Attribute
{
    /// <summary>
    ///   Gets the return value condition.
    /// </summary>
    public bool ReturnValue { get; }

    /// <summary>
    ///   Gets field or property member names.
    /// </summary>
    public string[] Members { get; }

    /// <summary>
    ///   Initializes the attribute with the specified return value condition and a field or property member.
    /// </summary>
    /// <param name="returnValue">
    ///   The return value condition. If the method returns this value,
    ///   the associated parameter will not be <see langword="null"/>.
    /// </param>
    /// <param name="member">
    ///   The field or property member that is promised to be not-<see langword="null"/>.
    /// </param>
    public MemberNotNullWhenAttribute(bool returnValue, string member)
    {
        ReturnValue = returnValue;
        Members = new[] { member };
    }

    /// <summary>
    ///   Initializes the attribute with the specified return value condition and list
    ///   of field and property members.
    /// </summary>
    /// <param name="returnValue">
    ///   The return value condition. If the method returns this value,
    ///   the associated parameter will not be <see langword="null"/>.
    /// </param>
    /// <param name="members">
    ///   The list of field and property members that are promised to be not-null.
    /// </param>
    public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
    {
        ReturnValue = returnValue;
        Members = members;
    }
}

#endif