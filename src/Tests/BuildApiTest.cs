[TestFixture]
class BuildApiTest
{
#if NET8_0 && DEBUG
    static string[] namespacesToClean =
    {
        "System.Collections.Generic.",
        "System.Threading.Tasks.",
        "System.Threading.",
        "System.Net.Http.",
        "System.Text",
        "System.IO.",
        "System.",
    };

    [Test]
    public void Run()
    {
        var solutionDirectory = VerifyTests.AttributeReader.GetSolutionDirectory();
        var path = Path.Combine(solutionDirectory, @"Consume\bin\Debug\netstandard2.0\Consume.dll");
        var md = Path.Combine(solutionDirectory, @"..\api_list.include.md");
        File.Delete(md);
        using var module = Mono.Cecil.ModuleDefinition.ReadModule(path);
        var extensions = module.GetTypes().Single(_ => _.Name == nameof(PolyfillExtensions));
        using var writer = File.CreateText(md);

        foreach (var type in extensions.Methods.GroupBy(_ => _.Parameters[0].ParameterType))
        {
            var targetType = type.Key;
            var targetFullName = targetType.FullName.Replace("`1", "").Replace("`2", "");
            writer.WriteLine($" ### {SimpleTypeName(targetFullName)}");
            writer.WriteLine();
            foreach (var method in type)
            {
                var parameters = string.Join(", ", method.Parameters.Skip(1).Select(_ => SimpleTypeName(_.ParameterType.FullName)));
                writer.WriteLine($" * {SimpleTypeName(method.ReturnType.FullName)} {method.Name}({parameters})");
            }

            writer.WriteLine();
        }
    }

    static string SimpleTypeName(string fullName)
    {
        return fullName;
        // var name = fullName.Replace("`1", "").Replace("`2", "");
        // foreach (var toClean in namespacesToClean)
        // {
        //     name = name.Replace(toClean, "");
        // }
        //
        // return name;
    }
#endif
}