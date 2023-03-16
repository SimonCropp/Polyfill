// ReSharper disable RedundantUsingDirective
#if NETFRAMEWORK || NETSTANDARD || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;

namespace System.Runtime.CompilerServices;


[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
[AttributeUsage(AttributeTargets.Parameter)]
sealed class CallerArgumentExpressionAttribute : Attribute
{
    public CallerArgumentExpressionAttribute(string parameterName) =>
        ParameterName = parameterName;

    public string ParameterName { get; }
}

#endif