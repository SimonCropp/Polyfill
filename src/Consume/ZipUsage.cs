// Usings are deliberately at the compilation unit level (not inside a namespace), so System.Linq is
// in the same scope as the `global using Polyfills;`. A polyfill that duplicates a BCL extension
// method is then ambiguous (CS0121) instead of being hidden by the closer using.

#if FeatureValueTuple

using System.Collections.Generic;
using System.Linq;

class ZipUsage
{
    void Zip()
    {
        IEnumerable<int> numbers = [1, 2];
        IEnumerable<string> words = ["one", "two"];
        var pairs = numbers.Zip(words);
        var triples = numbers.Zip(words, words);
        var projected = numbers.Zip(words, (number, word) => number + word);
    }
}

#endif
