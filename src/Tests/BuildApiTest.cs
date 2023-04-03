#if NET8_0 && DEBUG
[TestFixture]
class BuildApiTest
{
    static string[] namespacesToClean =
    {
        "System.Collections.Generic.",
        "System.Threading.Tasks.",
        "System.Threading.",
        "System.Net.Http.",
        "System.Text.",
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

        foreach (var type in extensions.Methods.GroupBy(_ => _.Parameters[0].ParameterType).OrderBy(_ => _.Key.Name))
        {
            var targetType = type.Key;
            var targetFullName = targetType.FullName.Replace("`1", "").Replace("`2", "");
            writer.WriteLine($"### {SimpleTypeName(targetFullName)}");
            writer.WriteLine();
            foreach (var method in type.OrderBy(_ => _.Name))
            {
                if (!method.IsPublic)
                {
                    continue;
                }
                var parameters = string.Join(", ", method.Parameters.Skip(1).Select(_ => SimpleTypeName(_.ParameterType.FullName)));
                var typeArgs = "";
                if (method.HasGenericParameters)
                {
                    typeArgs = $"<{string.Join(", ", method.GenericParameters.Select(_ => _.Name))}>";
                }

                var signature = $"{SimpleTypeName(method.ReturnType.FullName)} {method.Name}{typeArgs}({parameters})";
                var descriptionAttribute = method.CustomAttributes
                    .SingleOrDefault(_ => _.AttributeType.Name == "DescriptionAttribute");
                if (descriptionAttribute == null)
                {
                    throw new($"Description required {method.FullName}");
                }
                writer.WriteLine($" * `{signature}` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamwriter.writeasync#system-io-streamwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))");
            }

            writer.WriteLine();
        }
    }

    static string SimpleTypeName(string fullName)
    {
        var name = fullName.Replace("`1", "").Replace("`2", "");
        foreach (var toClean in namespacesToClean)
        {
            name = name.Replace(toClean, "");
        }
        
        return name;
    }
}
#endif