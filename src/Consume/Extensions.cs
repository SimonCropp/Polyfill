using System.Collections.Generic;

public static class Extensions
{
    extension<TSource>(IEnumerable<TSource> target)
    {
        public IEnumerable<TSource> ExtensionMethod()
        {
            return [];
        }
    }
}

public class ExtensionsUsage
{
    void Foo()
    {
        new List<string>().ExtensionMethod();
    }
}