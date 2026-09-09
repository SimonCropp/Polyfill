using System.Runtime.CompilerServices;

namespace ConsumeCsPreview;

// Compile-only coverage for C# union types. The compiler emits a struct that is
// annotated with UnionAttribute and implements IUnion, so this only compiles
// when both are polyfilled.

#region UnionDeclaration

record Cat(string Name);

record Dog(string Name);

record Bird(string Name);

union Pet(Cat, Dog, Bird);

#endregion

static class Unions
{
    #region UnionUsage

    public static string Describe(Dog dog)
    {
        // implicit conversion from one of the case types
        Pet pet = dog;

        // no default arm: the switch is exhaustive because every case type is handled
        return pet switch
        {
            Cat cat => $"cat {cat.Name}",
            Dog other => $"dog {other.Name}",
            Bird bird => $"bird {bird.Name}"
        };
    }

    #endregion

    #region UnionValue

    // every union implements IUnion, which exposes the current value as object
    public static object? CurrentValue(Pet pet) =>
        ((IUnion) pet).Value;

    #endregion
}
