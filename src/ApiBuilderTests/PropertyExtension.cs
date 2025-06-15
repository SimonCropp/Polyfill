
namespace Polyfills;
public static class PropertyExtension
{
    extension<TSource>(IEnumerable<TSource> source)
    {
        public bool HasSingleTarget => true;
    }
}
