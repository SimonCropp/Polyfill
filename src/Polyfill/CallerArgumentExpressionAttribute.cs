#if(NETFRAMEWORK)
namespace System.Runtime.CompilerServices;

[AttributeUsage(AttributeTargets.Parameter)]
sealed class CallerArgumentExpressionAttribute : Attribute
{
    public CallerArgumentExpressionAttribute(string parameterName) =>
        ParameterName = parameterName;

    public string ParameterName { get; }
}

#endif