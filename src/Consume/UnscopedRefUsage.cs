#if !NET7_0_OR_GREATER

using System.Diagnostics.CodeAnalysis;

struct UnscopedRefUsage
{
    int field;

    [UnscopedRef] ref int Prop1 => ref field;
}

#endif