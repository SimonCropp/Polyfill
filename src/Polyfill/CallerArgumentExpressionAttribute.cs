#if(NETFRAMEWORK || NETSTANDARD || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2)

namespace System.Runtime.CompilerServices;

[AttributeUsage(AttributeTargets.Parameter)]
sealed class CallerArgumentExpressionAttribute : Attribute
{
    public CallerArgumentExpressionAttribute(string parameterName) =>
        ParameterName = parameterName;

    public string ParameterName { get; }
}

#endif