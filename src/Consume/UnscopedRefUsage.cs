using System.Diagnostics.CodeAnalysis;
#pragma warning disable CS0169 // Field is never used

// ReSharper disable once ReplaceWithFieldKeyword
#region UnscopedRefUsage
struct UnscopedRefUsage
{
    int field1;

    [UnscopedRef] ref int Prop1 => ref field1;
}
#endregion