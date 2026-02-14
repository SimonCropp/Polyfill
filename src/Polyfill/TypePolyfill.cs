#if !NETCOREAPP2_1_OR_GREATER && !NETSTANDARD2_1_OR_GREATER
namespace Polyfills;

using System;

static partial class Polyfill
{
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
}
#endif
