// <auto-generated />
#pragma warning disable

#if !NET

namespace System.Diagnostics.CodeAnalysis;

using Targets = AttributeTargets;

/// <summary>
///   Specifies that the method or property will ensure that the listed field and property members have
///   non-<see langword="null"/> values when returning with the specified return value condition.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(
    validOn: Targets.Method |
             Targets.Property,
    Inherited = false,
    AllowMultiple = true)]
#if PolyPublic
public
#endif
sealed class MemberNotNullWhenAttribute :
    Attribute
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
        Members = [member];
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
#else
using System.Runtime.CompilerServices;
[assembly: TypeForwardedTo(typeof(System.Diagnostics.CodeAnalysis.MemberNotNullWhenAttribute))]
#endif