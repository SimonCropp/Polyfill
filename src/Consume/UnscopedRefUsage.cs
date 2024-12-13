using System.Diagnostics.CodeAnalysis;
#pragma warning disable CS0169 // Field is never used

struct UnscopedRefUsage
{
    // ReSharper disable once ReplaceWithFieldKeyword
    int field1;

    [UnscopedRef] ref int Prop1 => ref field1;
}