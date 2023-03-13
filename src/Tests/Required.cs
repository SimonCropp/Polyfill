using System.Diagnostics.CodeAnalysis;
#region Required
public class Person
{
    public Person() { }

    [SetsRequiredMembers]
    public Person(string name) =>
        Name = name;

    public required string Name { get; init; }
}
#endregion