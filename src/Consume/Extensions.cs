using System;
using System.Collections.Generic;
using System.Linq;

public static class Extensions
{
    extension<TSource>(IEnumerable<TSource> source)
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