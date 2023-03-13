#if(NETSTANDARD2_0 || NETFRAMEWORK || NETCOREAPP2_1 || NETCOREAPP2_0)

namespace System.Diagnostics.CodeAnalysis;

/// <summary>
///     Specifies that when a method returns <see cref="ReturnValue"/>,
///     the parameter may be <see langword="null"/> even if the corresponding type disallows it.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
sealed class MaybeNullWhenAttribute : Attribute
{
    /// <summary>
    ///     Gets the return value condition.
    ///     If the method returns this value, the associated parameter may be <see langword="null"/>.
    /// </summary>
    public bool ReturnValue { get; }

    /// <summary>
    ///      Initializes the attribute with the specified return value condition.
    /// </summary>
    /// <param name="returnValue">
    ///     The return value condition.
    ///     If the method returns this value, the associated parameter may be <see langword="null"/>.
    /// </param>
    public MaybeNullWhenAttribute(bool returnValue) =>
        ReturnValue = returnValue;
}
#endif