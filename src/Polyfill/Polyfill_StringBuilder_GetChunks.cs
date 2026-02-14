#if !NET6_0_OR_GREATER && FeatureMemory

namespace Polyfills;

using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;

static partial class Polyfill
{
    static FieldInfo chunkCharsField = GetStringBuilderField("m_ChunkChars");
    static FieldInfo chunkPreviousField = GetStringBuilderField("m_ChunkPrevious");
    static FieldInfo chunkLengthField = GetStringBuilderField("m_ChunkLength");

    static FieldInfo GetStringBuilderField(string name)
    {
        var field = typeof(StringBuilder).GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
        if (field != null)
        {
            return field;
        }

        throw new($"Expected to find field '{name}' on StringBuilder");
    }

    static int GetChunkLength(StringBuilder stringBuilder) =>
        (int) chunkLengthField.GetValue(stringBuilder)!;

    static char[] GetChunkChars(StringBuilder stringBuilder) =>
        (char[]) chunkCharsField.GetValue(stringBuilder)!;

    static StringBuilder? GetChunkPrevious(StringBuilder stringBuilder) =>
        (StringBuilder?) chunkPreviousField.GetValue(stringBuilder);

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
