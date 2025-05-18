using System;
using System.Collections.Generic;
using System.Linq;

public static class Enumerable
{
    extension<TSource>(IEnumerable<TSource> source)
    {
        public IEnumerable<TSource> Where(Func<TSource, bool> predicate)
        {
            return [];
        }
    }
}