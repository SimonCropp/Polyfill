using System;
using System.Buffers.Text;

public class BclMemoryConsume
{
    public Index GetIndex() => new Index(0);

    public Range GetRange() => new Range(new Index(0), new Index(1));

    // Microsoft.Bcl.Memory supplies Base64Url, but not these Base64 members,
    // so Polyfill must still provide them when the package is referenced.
    public string Base64Encode(ReadOnlySpan<byte> source) => Base64.EncodeToString(source);

    public byte[] Base64Decode(ReadOnlySpan<char> source) => Base64.DecodeFromChars(source);

    public string Base64UrlEncode(ReadOnlySpan<byte> source) => Base64Url.EncodeToString(source);
}
