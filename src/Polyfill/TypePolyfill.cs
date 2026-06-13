namespace Polyfills;

using System;

static partial class Polyfill
{
#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
    extension(Type target)
    {
        /// <summary>
        /// Gets a value that indicates whether the current Type represents a type parameter in the definition of a generic method.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.type.isgenericmethodparameter?view=net-11.0
        public bool IsGenericMethodParameter =>
            target.IsGenericParameter &&
            target.DeclaringMethod != null;
    }
#endif

#if !NET11_0_OR_GREATER
    extension(Type target)
    {
        /// <summary>
        /// Returns the underlying type argument of the current nullable value type, or <c>null</c> if the current type is not a nullable value type.
        /// </summary>
        //Link: https://learn.microsoft.com/en-us/dotnet/api/system.type.getnullableunderlyingtype?view=net-11.0
        public Type? GetNullableUnderlyingType() =>
            Nullable.GetUnderlyingType(target);
    }
#endif
}
