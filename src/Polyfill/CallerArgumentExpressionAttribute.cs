#if NETFRAMEWORK || NETSTANDARD || NETCOREAPP2X

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Runtime.CompilerServices;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Parameter)]
#if PolyPublic
public
#endif
sealed class CallerArgumentExpressionAttribute :
    Attribute
{
    public CallerArgumentExpressionAttribute(string parameterName) =>
        ParameterName = parameterName;

    public string ParameterName { get; }
}

#endif