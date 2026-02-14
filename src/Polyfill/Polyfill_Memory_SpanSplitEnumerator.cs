#if FeatureMemory && !NET9_0_OR_GREATER && FeatureValueTuple

namespace Polyfills;

using System;
using System.Buffers;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

static partial class Polyfill
{
    /// <summary>
    /// Enables enumerating each split within a <see cref="ReadOnlySpan{T}"/> that has been divided using one or more separators.
    /// </summary>
    //https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/MemoryExtensions.cs
    //Link: https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.spansplitenumerator-1?view=net-11.0
    public ref struct SpanSplitEnumerator<T> : IEnumerator<Range>
        where T : IEquatable<T>
    {
        private readonly ReadOnlySpan<T> _source;

        private readonly T _separator = default!;

        private readonly ReadOnlySpan<T> _separatorBuffer;

#if NET8_0
        private readonly SearchValues<T> _searchValues = default!;
#endif

        private SpanSplitEnumeratorMode _splitMode;

        private int _startCurrent = 0;

        private int _endCurrent = 0;

        private int _startNext = 0;

        public SpanSplitEnumerator<T> GetEnumerator() => this;

        /// <summary>Gets the source span being enumerated.</summary>
        public readonly ReadOnlySpan<T> Source => _source;

        public Range Current => new Range(_startCurrent, _endCurrent);

#if NET8_0
        internal SpanSplitEnumerator(ReadOnlySpan<T> source, SearchValues<T> searchValues)
        {
            _source = source;
            _splitMode = SpanSplitEnumeratorMode.SearchValues;
            _searchValues = searchValues;
        }
#endif

        internal SpanSplitEnumerator(ReadOnlySpan<T> source, ReadOnlySpan<T> separators)
        {
            _source = source;
            if (typeof(T) == typeof(char) && separators.Length == 0)
            {
#if NET8_0
                _searchValues = Unsafe.As<SearchValues<T>>(WhiteSpaceChars);
                _splitMode = SpanSplitEnumeratorMode.SearchValues;
#else
                _separatorBuffer = WhiteSpaceChars.AsSpan<T>();
                _splitMode = SpanSplitEnumeratorMode.Any;
#endif
                return;
            }

            _separatorBuffer = separators;
            _splitMode = SpanSplitEnumeratorMode.Any;
        }

        internal SpanSplitEnumerator(ReadOnlySpan<T> source, ReadOnlySpan<T> separator, bool treatAsSingleSeparator)
        {
            _source = source;
            _separatorBuffer = separator;
            _splitMode = separator.Length == 0 ? SpanSplitEnumeratorMode.EmptySequence : SpanSplitEnumeratorMode.Sequence;
        }

        internal SpanSplitEnumerator(ReadOnlySpan<T> source, T separator)
        {
            _source = source;
            _separator = separator;
            _splitMode = SpanSplitEnumeratorMode.SingleElement;
        }

        public bool MoveNext()
        {
            // Search for the next separator index.
            int separatorIndex, separatorLength;
            switch (_splitMode)
            {
                case SpanSplitEnumeratorMode.None:
                    return false;

                case SpanSplitEnumeratorMode.SingleElement:
                    separatorLength = 1;
#if NETFRAMEWORK
                    if (_separator is null)
                    {
                        separatorIndex = -1;
                        for (int i = _startNext; i < _source.Length; i++)
                        {
                            if (_source[i] == null)
                            {
                                separatorIndex = i;
                                break;
                            }
                        }
                        break;
                    }
#endif
                    separatorIndex = _source.Slice(_startNext)
                        .IndexOf(_separator);
                    break;

                case SpanSplitEnumeratorMode.Any:

                    separatorLength = 1;
#if !NETCOREAPP
                    //https://github.com/dotnet/coreclr/pull/25075
                    if (_separatorBuffer.Length == 0)
                    {
                        separatorIndex = -1;
                        break;
                    }
#endif
                    separatorIndex = _source.Slice(_startNext)
                        .IndexOfAny(_separatorBuffer);
                    break;

                case SpanSplitEnumeratorMode.Sequence:
                    separatorIndex = _source.Slice(_startNext).IndexOf(_separatorBuffer);
                    separatorLength = _separatorBuffer.Length;
                    break;

                case SpanSplitEnumeratorMode.EmptySequence:
                    separatorIndex = -1;
                    separatorLength = 1;
                    break;

#if NET8_0
                case SpanSplitEnumeratorMode.SearchValues:
                    separatorIndex = _source.Slice(_startNext).IndexOfAny(_searchValues);
                    separatorLength = 1;
                    break;
#endif
                default:
                    throw new Exception($"Invalid split mode: {_splitMode}");
            }

            _startCurrent = _startNext;
            if (separatorIndex >= 0)
            {
                _endCurrent = _startCurrent + separatorIndex;
                _startNext = _endCurrent + separatorLength;
            }
            else
            {
                _startNext = _endCurrent = _source.Length;

                // Set _splitMode to None so that subsequent MoveNext calls will return false.
                _splitMode = SpanSplitEnumeratorMode.None;
            }

            return true;
        }

        object IEnumerator.Current => Current;

        void IEnumerator.Reset() => throw new NotSupportedException();

        void IDisposable.Dispose()
        {
        }

        const string whitespaces = "\t\n\v\f\r\u0020\u0085\u00a0\u1680\u2000\u2001\u2002\u2003\u2004\u2005\u2006\u2007\u2008\u2009\u200a\u2028\u2029\u202f\u205f\u3000";

#if NET8_0
        public static readonly SearchValues<char> WhiteSpaceChars =
            SearchValues.Create(whitespaces.AsSpan());
#else
        public static readonly T[] WhiteSpaceChars;

        static SpanSplitEnumerator()
        {
            if (typeof(T) == typeof(char))
            {
                WhiteSpaceChars = whitespaces
                    .Cast<T>()
                    .ToArray();
            }
        }
#endif
    }
}
#endif
