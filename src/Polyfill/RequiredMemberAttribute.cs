#if(!NET7_0_OR_GREATER)

namespace System.Runtime.CompilerServices;
/// <summary>
/// Specifies that a type has required members or that a member is required.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    sealed class RequiredMemberAttribute : Attribute
{ }

#endif