 ### System.Net.Http.HttpClient

 * System.Threading.Tasks.Task`1<System.IO.Stream> GetStreamAsync(System.String, System.Threading.CancellationToken)
 * System.Threading.Tasks.Task`1<System.IO.Stream> GetStreamAsync(System.Uri, System.Threading.CancellationToken)
 * System.Threading.Tasks.Task`1<System.Byte[]> GetByteArrayAsync(System.String, System.Threading.CancellationToken)
 * System.Threading.Tasks.Task`1<System.Byte[]> GetByteArrayAsync(System.Uri, System.Threading.CancellationToken)
 * System.Threading.Tasks.Task`1<System.String> GetStringAsync(System.String, System.Threading.CancellationToken)
 * System.Threading.Tasks.Task`1<System.String> GetStringAsync(System.Uri, System.Threading.CancellationToken)

 ### System.Net.Http.HttpContent

 * System.Threading.Tasks.Task`1<System.IO.Stream> ReadAsStreamAsync(System.Threading.CancellationToken)
 * System.Threading.Tasks.Task`1<System.Byte[]> ReadAsByteArrayAsync(System.Threading.CancellationToken)
 * System.Threading.Tasks.Task`1<System.String> ReadAsStringAsync(System.Threading.CancellationToken)

 ### System.Collections.Generic.IEnumerable<TSource>

 * System.Collections.Generic.IEnumerable`1<TSource> SkipLast(System.Int32)

 ### System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>

 * TValue GetValueOrDefault(TKey)

 ### System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>

 * TValue GetValueOrDefault(TKey, TValue)

 ### System.Collections.Generic.KeyValuePair<TKey,TValue>

 * System.Void Deconstruct(TKey&, TValue&)

 ### System.ReadOnlySpan<System.Char>

 * System.Boolean Contains(System.Char)

 ### System.Text.StringBuilder

 * System.Void Append(System.ReadOnlySpan`1<System.Char>)
 * System.Boolean Equals(System.ReadOnlySpan`1<System.Char>)

 ### System.ReadOnlySpan<System.Char>

 * System.Boolean SequenceEqual(System.String)

 ### System.Span<System.Char>

 * System.Boolean SequenceEqual(System.String)

 ### System.IO.Stream

 * System.Threading.Tasks.ValueTask`1<System.Int32> ReadAsync(System.Memory`1<System.Byte>, System.Threading.CancellationToken)
 * System.Threading.Tasks.ValueTask WriteAsync(System.ReadOnlyMemory`1<System.Byte>, System.Threading.CancellationToken)
 * System.Threading.Tasks.Task CopyToAsync(System.IO.Stream, System.Threading.CancellationToken)

 ### System.IO.StreamReader

 * System.Threading.Tasks.ValueTask`1<System.Int32> ReadAsync(System.Memory`1<System.Char>, System.Threading.CancellationToken)
 * System.Threading.Tasks.Task`1<System.String> ReadToEndAsync(System.Threading.CancellationToken)

 ### System.IO.StreamWriter

 * System.Threading.Tasks.ValueTask WriteAsync(System.ReadOnlyMemory`1<System.Char>, System.Threading.CancellationToken)

 ### System.String

 * System.Int32 GetHashCode(System.StringComparison)
 * System.Boolean Contains(System.String, System.StringComparison)
 * System.Boolean StartsWith(System.Char)
 * System.Boolean EndsWith(System.Char)
 * System.String[] Split(System.Char, System.StringSplitOptions)
 * System.String[] Split(System.Char, System.Int32, System.StringSplitOptions)
 * System.Boolean Contains(System.Char)

 ### System.StringComparison

 * System.StringComparer FromComparison()

