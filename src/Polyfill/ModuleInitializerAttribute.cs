#if(!NET5_0_OR_GREATER)
namespace System.Runtime.CompilerServices;

[AttributeUsage(AttributeTargets.Method, Inherited = false)]
#if(PublicPolyfill)
public
#endif
sealed class ModuleInitializerAttribute : Attribute
{
}
#endif