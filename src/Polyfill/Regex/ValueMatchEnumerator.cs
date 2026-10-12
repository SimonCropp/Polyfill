// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#if !NET7_0_OR_GREATER && FeatureMemory

namespace System.Text.RegularExpressions;

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

//https://github.com/dotnet/runtime/blob/main/src/libraries/System.Text.RegularExpressions/src/System/Text/RegularExpressions/Regex.EnumerateMatches.cs
/// <summary>
/// Represents an enumerator containing the set of successful matches found by iteratively applying a regular expression pattern to the input span.
/// </summary>
[ExcludeFromCodeCoverage]
[DebuggerNonUserCode]
#if PolyUseEmbeddedAttribute
[global::Microsoft.CodeAnalysis.EmbeddedAttribute]
#endif
#if PolyPublic
public
#endif
ref struct ValueMatchEnumerator
{
    ReadOnlySpan<char> _input;
    ValueMatch _current;
    readonly Regex _regex;
    readonly string _inputString;
    readonly int _startAt;
    Match? _match;

    internal ValueMatchEnumerator(Regex regex, ReadOnlySpan<char> input, int startAt)
    {
        if ((uint)startAt > (uint)input.Length)
        {
            throw new ArgumentOutOfRangeException("startat");
        }

        _regex = regex;
        _inputString = input.ToString();
        _startAt = startAt;
        _match = null;
        _input = input;
        _current = default;
    }

    public readonly ValueMatchEnumerator GetEnumerator() => this;

    // Matches are found one at a time, so stopping early does not pay for the matches that were never read.
    public bool MoveNext()
    {
        Match match;
        if (_match is null)
        {
            match = _regex.Match(_inputString, _startAt);
        }
        else if (_match.Success)
        {
            match = _match.NextMatch();
        }
        else
        {
            return false;
        }

        _match = match;
        if (!match.Success)
        {
            return false;
        }

        _current = new ValueMatch(match.Index, match.Length);
        return true;
    }

    public readonly ValueMatch Current => _current;
}
#endif