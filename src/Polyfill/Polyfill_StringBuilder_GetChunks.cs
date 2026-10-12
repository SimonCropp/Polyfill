#if !NET6_0_OR_GREATER && FeatureMemory

namespace Polyfills;

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

static partial class Polyfill
{
    // Nested so the accessors are only built when GetChunks is used, and not when Polyfill is initialized
    static class StringBuilderChunkAccessors
    {
        public static readonly Func<StringBuilder, char[]> Chars = Create<char[]>("m_ChunkChars");
        public static readonly Func<StringBuilder, StringBuilder?> Previous = Create<StringBuilder?>("m_ChunkPrevious");
        public static readonly Func<StringBuilder, int> Length = Create<int>("m_ChunkLength");

        // FieldInfo.GetValue is slow, and is needed several times per chunk, so each field read is compiled once
        static Func<StringBuilder, TField> Create<TField>(string name)
        {
            var field = typeof(StringBuilder).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
            if (field == null)
            {
                throw new($"Expected to find field '{name}' on StringBuilder");
            }

            try
            {
                var builder = Expression.Parameter(typeof(StringBuilder), "builder");
                return Expression
                    .Lambda<Func<StringBuilder, TField>>(Expression.Field(builder, field), builder)
                    .Compile();
            }
            catch (Exception)
            {
                // Expressions can not be compiled here, so read the field through reflection
                return builder => (TField) field.GetValue(builder)!;
            }
        }
    }

    static int GetChunkLength(StringBuilder stringBuilder) =>
        StringBuilderChunkAccessors.Length(stringBuilder);

    static char[] GetChunkChars(StringBuilder stringBuilder) =>
        StringBuilderChunkAccessors.Chars(stringBuilder);

    static StringBuilder? GetChunkPrevious(StringBuilder stringBuilder) =>
        StringBuilderChunkAccessors.Previous(stringBuilder);

    /// <summary>
    /// GetChunks returns ChunkEnumerator that follows the IEnumerable pattern and
    /// thus can be used in a C# 'foreach' statements to retrieve the data in the StringBuilder
    /// as chunks (ReadOnlyMemory) of characters.
    /// </summary>
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.getchunks?view=net-11.0
    public static ChunkEnumerator GetChunks(this StringBuilder target) =>
        new(target);

    /// <summary>
    /// ChunkEnumerator supports both the IEnumerable and IEnumerator pattern so foreach
    /// works (see GetChunks).  It needs to be public (so the compiler can use it
    /// when building a foreach statement) but users typically don't use it explicitly.
    /// (which is why it is a nested type).
    /// </summary>
    public struct ChunkEnumerator
    {
        StringBuilder _firstChunk;

        StringBuilder? _currentChunk;

        ManyChunkInfo? _manyChunks;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public ChunkEnumerator GetEnumerator() =>
            this;

        /// <summary>
        /// Implements the IEnumerator pattern.
        /// </summary>
        public bool MoveNext()
        {
            if (_currentChunk == _firstChunk)
            {
                return false;
            }

            if (_manyChunks != null)
            {
                return _manyChunks.MoveNext(ref _currentChunk);
            }

            var next = _firstChunk;
            while (true)
            {
                var chunkPrevious = GetChunkPrevious(next);
                if (chunkPrevious == _currentChunk)
                {
                    break;
                }

                next = chunkPrevious;
            }

            _currentChunk = next;
            return true;
        }

        /// <summary>
        /// Implements the IEnumerator pattern.
        /// </summary>
        public ReadOnlyMemory<char> Current
        {
            get
            {
                if (_currentChunk == null)
                {
                    throw new InvalidOperationException("Enumeration operation cant happen");
                }

                return new(GetChunkChars(_currentChunk), 0, GetChunkLength(_currentChunk));
            }
        }

        internal ChunkEnumerator(StringBuilder builder)
        {
            _firstChunk = builder;
            _currentChunk = null;
            _manyChunks = null;

            var chunkCount = ChunkCount(builder);
            if (8 < chunkCount)
            {
                _manyChunks = new(builder, chunkCount);
            }
        }

        static int ChunkCount(StringBuilder? builder)
        {
            var ret = 0;
            while (builder != null)
            {
                ret++;
                builder = GetChunkPrevious(builder);
            }

            return ret;
        }

        /// <summary>
        /// Used to hold all the chunks indexes when you have many chunks.
        /// </summary>
        class ManyChunkInfo
        {
            StringBuilder[] _chunks;
            int _chunkPos;

            public bool MoveNext(ref StringBuilder? current)
            {
                var pos = ++_chunkPos;
                if (_chunks.Length <= pos)
                {
                    return false;
                }

                current = _chunks[pos];
                return true;
            }

            public ManyChunkInfo(StringBuilder? builder, int chunkCount)
            {
                _chunks = new StringBuilder[chunkCount];
                while (0 <= --chunkCount)
                {
                    _chunks[chunkCount] = builder;
                    builder = GetChunkPrevious(builder);
                }

                _chunkPos = -1;
            }
        }
    }
}
#endif
