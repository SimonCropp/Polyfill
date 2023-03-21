#if NETFRAMEWORK || NETSTANDARD || NETCOREAPP2X

// ReSharper disable RedundantUsingDirective
// ReSharper disable UnusedMember.Global

using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;

namespace System.Runtime.CompilerServices;

[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Parameter)]
sealed class CallerArgumentExpressionAttribute :
    Attribute
{
    public CallerArgumentExpressionAttribute(string parameterName) =>
        ParameterName = parameterName;

    public string ParameterName { get; }
}

#endif