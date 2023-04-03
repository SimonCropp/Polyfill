### HttpClient

 * Task<Stream> GetStreamAsync(String, CancellationToken)
 * Task<Stream> GetStreamAsync(Uri, CancellationToken)
 * Task<Byte[]> GetByteArrayAsync(String, CancellationToken)
 * Task<Byte[]> GetByteArrayAsync(Uri, CancellationToken)
 * Task<String> GetStringAsync(String, CancellationToken)
 * Task<String> GetStringAsync(Uri, CancellationToken)

### HttpContent

 * Task<Stream> ReadAsStreamAsync(CancellationToken)
 * Task<Byte[]> ReadAsByteArrayAsync(CancellationToken)
 * Task<String> ReadAsStringAsync(CancellationToken)

### IEnumerable<TSource>

 * IEnumerable<TSource> SkipLast<TSource>(Int32)

### IReadOnlyDictionary<TKey,TValue>

 * TValue GetValueOrDefault<TKey, TValue>(TKey)

### IReadOnlyDictionary<TKey,TValue>

 * TValue GetValueOrDefault<TKey, TValue>(TKey, TValue)

### KeyValuePair<TKey,TValue>

 * Void Deconstruct<TKey, TValue>(TKey&, TValue&)

### ReadOnlySpan<Char>

 * Boolean Contains(Char)

### StringBuilder

 * Void Append(ReadOnlySpan<Char>)
 * Boolean Equals(ReadOnlySpan<Char>)

### ReadOnlySpan<Char>

 * Boolean SequenceEqual(String)

### Span<Char>

 * Boolean SequenceEqual(String)

### Stream

 * ValueTask<Int32> ReadAsync(Memory<Byte>, CancellationToken)
 * ValueTask WriteAsync(ReadOnlyMemory<Byte>, CancellationToken)
 * Task CopyToAsync(Stream, CancellationToken)

### StreamReader

 * ValueTask<Int32> ReadAsync(Memory<Char>, CancellationToken)
 * Task<String> ReadToEndAsync(CancellationToken)

### StreamWriter

 * ValueTask WriteAsync(ReadOnlyMemory<Char>, CancellationToken)

### String

 * Int32 GetHashCode(StringComparison)
 * Boolean Contains(String, StringComparison)
 * Boolean StartsWith(Char)
 * Boolean EndsWith(Char)
 * String[] Split(Char, StringSplitOptions)
 * String[] Split(Char, Int32, StringSplitOptions)
 * Boolean Contains(Char)

### StringComparison

 * StringComparer FromComparison()

