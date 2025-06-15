public static class PropertyExtensions
{
    extension<TSource>(IEnumerable<TSource> source)
    {
        public bool HasSingleTarget => true;
    }
}
