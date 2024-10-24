### Extension methods

#### bool

 * `bool TryFormat(bool, Span<char>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.boolean.tryformat")


#### byte

 * `bool TryFormat(byte, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryformat")


#### ConcurrentDictionary<TKey,TValue>

 * `TValue GetOrAdd<TKey, TValue, TArg>(ConcurrentDictionary<TKey,TValue>, TKey, Func<TKey, TArg, TValue>, TArg) where TKey : notnull` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.getoradd#system-collections-concurrent-concurrentdictionary-2-getoradd-1(-0-system-func((-0-0-1))-0)")


#### DateOnly

 * `bool TryFormat(DateOnly, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.tryformat")


#### DateTime

 * `int Nanosecond(DateTime)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.nanosecond")
 * `int Microsecond(DateTime)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.microsecond")
 * `bool TryFormat(DateTime, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryformat")


#### DateTimeOffset

 * `int Nanosecond(DateTimeOffset)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.nanosecond")
 * `int Microsecond(DateTimeOffset)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.microsecond")
 * `bool TryFormat(DateTimeOffset, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryformat")


#### decimal

 * `bool TryFormat(decimal, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.decimal.tryformat")


#### Dictionary<TKey, TValue>

 * `bool TryAdd<TKey, TValue>(Dictionary<TKey, TValue>, TKey, TValue) where TKey : notnull` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.tryadd")
 * `bool Remove<TKey, TValue>(Dictionary<TKey, TValue>, TKey, TValue) where TKey : notnull` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.remove")


#### double

 * `bool TryFormat(double, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.double.tryformat")


#### EventInfo

 * `NullabilityInfo GetNullabilityInfo(EventInfo)`
 * `NullabilityState GetNullability(EventInfo)`
 * `bool IsNullable(EventInfo)`


#### FieldInfo

 * `NullabilityInfo GetNullabilityInfo(FieldInfo)`
 * `NullabilityState GetNullability(FieldInfo)`
 * `bool IsNullable(FieldInfo)`


#### float

 * `bool TryFormat(float, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.single.tryformat")


#### Guid

 * `bool TryFormat(Guid, Span<char>, int, ReadOnlySpan<char>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryformat#system-guid-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char)))")


#### HashSet<T>

 * `bool TryGetValue<T>(HashSet<T>, T, T)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.trygetvalue")


#### HttpClient

 * `Task<Stream> GetStreamAsync(HttpClient, string, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync#system-net-http-httpclient-getstreamasync(system-string-system-threading-cancellationtoken)")
 * `Task<Stream> GetStreamAsync(HttpClient, Uri, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync#system-net-http-httpclient-getstreamasync(system-uri-system-threading-cancellationtoken)")
 * `Task<byte[]> GetByteArrayAsync(HttpClient, string, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync#system-net-http-httpclient-getbytearrayasync(system-string-system-threading-cancellationtoken)")
 * `Task<byte[]> GetByteArrayAsync(HttpClient, Uri, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync#system-net-http-httpclient-getbytearrayasync(system-uri-system-threading-cancellationtoken)")
 * `Task<string> GetStringAsync(HttpClient, string, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync#system-net-http-httpclient-getstringasync(system-string-system-threading-cancellationtoken)")
 * `Task<string> GetStringAsync(HttpClient, Uri, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync#system-net-http-httpclient-getstringasync(system-uri-system-threading-cancellationtoken)")


#### HttpContent

 * `Task<Stream> ReadAsStreamAsync(HttpContent, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstreamasync#system-net-http-httpcontent-readasstreamasync(system-threading-cancellationtoken)")
 * `Task<byte[]> ReadAsByteArrayAsync(HttpContent, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasbytearrayasync#system-net-http-httpcontent-readasbytearrayasync(system-threading-cancellationtoken)")
 * `Task<string> ReadAsStringAsync(HttpContent, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstringasync#system-net-http-httpcontent-readasstringasync(system-threading-cancellationtoken)")


#### IEnumerable<TFirst>

 * `IEnumerable<(TFirst First, TSecond Second)> Zip<TFirst, TSecond>(IEnumerable<TFirst>, IEnumerable<TSecond>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip#system-linq-enumerable-zip-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1)))")


#### IEnumerable<TSource>

 * `IEnumerable<TSource> Append<TSource>(IEnumerable<TSource>, TSource)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.append")
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, TSource)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0)))")
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, TSource, IEqualityComparer<TSource>?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0)))")
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, IEqualityComparer<TSource>, TSource[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0)))")
 * `IEnumerable<TSource> SkipLast<TSource>(IEnumerable<TSource>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.skiplast")
 * `IEnumerable<TSource> TakeLast<TSource>(IEnumerable<TSource>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.takelast")
 * `HashSet<TSource> ToHashSet<TSource>(IEnumerable<TSource>, IEqualityComparer<TSource>?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tohashset#system-linq-enumerable-tohashset-1(system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0)))")


#### int

 * `bool TryFormat(int, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryformat")


#### IReadOnlyDictionary<TKey, TValue>

 * `TValue? GetValueOrDefault<TKey, TValue>(IReadOnlyDictionary<TKey, TValue>, TKey) where TKey : notnull` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault")
 * `TValue GetValueOrDefault<TKey, TValue>(IReadOnlyDictionary<TKey, TValue>, TKey, TValue) where TKey : notnull` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault#system-collections-generic-collectionextensions-getvalueordefault-2(system-collections-generic-ireadonlydictionary((-0-1))-0-1)")


#### KeyValuePair<TKey, TValue>

 * `void Deconstruct<TKey, TValue>(KeyValuePair<TKey, TValue>, TKey, TValue)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2.deconstruct")


#### long

 * `bool TryFormat(long, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryformat")


#### MemberInfo

 * `bool HasSameMetadataDefinitionAs(MemberInfo, MemberInfo)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo.hassamemetadatadefinitionas")
 * `NullabilityInfo GetNullabilityInfo(MemberInfo)`
 * `NullabilityState GetNullability(MemberInfo)`
 * `bool IsNullable(MemberInfo)`


#### ParameterInfo

 * `NullabilityInfo GetNullabilityInfo(ParameterInfo)`
 * `NullabilityState GetNullability(ParameterInfo)`
 * `bool IsNullable(ParameterInfo)`


#### PropertyInfo

 * `NullabilityInfo GetNullabilityInfo(PropertyInfo)`
 * `NullabilityState GetNullability(PropertyInfo)`
 * `bool IsNullable(PropertyInfo)`


#### Random

 * `void NextBytes(Random, Span<byte>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes#system-random-nextbytes(system-span((system-byte)))")


#### ReadOnlySpan<char>

 * `bool SequenceEqual(ReadOnlySpan<char>, string)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal#system-memoryextensions-sequenceequal-1(system-readonlyspan((-0))-system-readonlyspan((-0)))")
 * `bool EndsWith(ReadOnlySpan<char>, string, StringComparison)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith#system-memoryextensions-endswith-1(system-readonlyspan((-0))-system-readonlyspan((-0)))")
 * `bool StartsWith(ReadOnlySpan<char>, string, StringComparison)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith#system-memoryextensions-startswith-1(system-readonlyspan((-0))-system-readonlyspan((-0)))")


#### ReadOnlySpan<T>

 * `bool Contains<T>(ReadOnlySpan<T>, T) where T : IEquatable<T>` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains#system-memoryextensions-contains-1(system-readonlyspan((-0))-0)")


#### sbyte

 * `bool TryFormat(sbyte, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryformat")


#### short

 * `bool TryFormat(short, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryformat")


#### Span<char>

 * `Span<char> TrimStart(Span<char>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.trimstart#system-memoryextensions-trimstart(system-span((system-char)))")
 * `Span<char> TrimEnd(Span<char>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.trimend#system-memoryextensions-trimend(system-span((system-char)))")
 * `bool SequenceEqual(Span<char>, string)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal#system-memoryextensions-sequenceequal-1(system-span((-0))-system-readonlyspan((-0)))")
 * `bool EndsWith(Span<char>, string)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith#system-memoryextensions-endswith-1(system-span((-0))-system-readonlyspan((-0)))")
 * `bool StartsWith(Span<char>, string)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith#system-memoryextensions-startswith-1(system-span((-0))-system-readonlyspan((-0)))")


#### Span<T>

 * `bool Contains<T>(Span<T>, T) where T : IEquatable<T>` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains#system-memoryextensions-contains-1(system-span((-0))-0)")


#### Stream

 * `ValueTask<int> ReadAsync(Stream, Memory<byte>, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readasync#system-io-stream-readasync(system-memory((system-byte))-system-threading-cancellationtoken)")
 * `ValueTask WriteAsync(Stream, ReadOnlyMemory<byte>, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.writeasync#system-io-stream-writeasync(system-readonlymemory((system-byte))-system-threading-cancellationtoken)")
 * `Task CopyToAsync(Stream, Stream, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.copytoasync#system-io-stream-copytoasync(system-io-stream-system-threading-cancellationtoken)")
 * `ValueTask DisposeAsync(Stream)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.disposeasync")


#### string

 * `int GetHashCode(string, StringComparison)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode#system-string-gethashcode(system-stringcomparison)")
 * `bool Contains(string, string, StringComparison)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-string-system-stringcomparison)")
 * `bool StartsWith(string, char)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char)")
 * `bool EndsWith(string, char)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char)")
 * `string[] Split(string, char, StringSplitOptions)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.split#system-string-split(system-char-system-stringsplitoptions)")
 * `string[] Split(string, char, int, StringSplitOptions)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.split#system-string-split(system-char-system-int32-system-stringsplitoptions)")
 * `bool Contains(string, char)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char)")


#### StringBuilder

 * `StringBuilder Append(StringBuilder, StringBuilder.AppendInterpolatedStringHandler)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-text-stringbuilder-appendinterpolatedstringhandler@)")
 * `StringBuilder Append(StringBuilder, IFormatProvider?, StringBuilder.AppendInterpolatedStringHandler)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@)")
 * `StringBuilder AppendLine(StringBuilder, StringBuilder.AppendInterpolatedStringHandler)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline#system-text-stringbuilder-appendline(system-text-stringbuilder-appendinterpolatedstringhandler@)")
 * `StringBuilder AppendLine(StringBuilder, IFormatProvider?, StringBuilder.AppendInterpolatedStringHandler)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline#system-text-stringbuilder-appendline(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@)")
 * `StringBuilder AppendJoin(StringBuilder, string, string[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-string-system-string())")
 * `StringBuilder AppendJoin(StringBuilder, string, Object[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-string-system-object())")
 * `StringBuilder AppendJoin(StringBuilder, char, string[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-char-system-string())")
 * `StringBuilder AppendJoin(StringBuilder, char, object[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-char-system-object())")
 * `StringBuilder AppendJoin<T>(StringBuilder, char, T[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0)))")
 * `StringBuilder AppendJoin<T>(StringBuilder, string, T[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0)))")


#### Task

 * `Task WaitAsync(Task, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-threading-cancellationtoken)")
 * `Task WaitAsync(Task, TimeSpan)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-timespan)")
 * `Task WaitAsync(Task, TimeSpan, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-timespan-system-threading-cancellationtoken)")


#### Task<TResult>

 * `Task<TResult> WaitAsync<TResult>(Task<TResult>, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-threading-cancellationtoken)")
 * `Task<TResult> WaitAsync<TResult>(Task<TResult>, TimeSpan)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync#system-threading-tasks-task-1-waitasync(system-timespan)")
 * `Task<TResult> WaitAsync<TResult>(Task<TResult>, TimeSpan, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync#system-threading-tasks-task-1-waitasync(system-timespan-system-threading-cancellationtoken)")


#### TextReader

 * `ValueTask<int> ReadAsync(TextReader, Memory<char>, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readasync#system-io-textreader-readasync(system-memory((system-char))-system-threading-cancellationtoken)")


#### TextWriter

 * `ValueTask WriteAsync(TextWriter, ReadOnlyMemory<char>, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken)")
 * `ValueTask WriteLineAsync(TextWriter, ReadOnlyMemory<char>, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync#system-io-textwriter-writelineasync(system-readonlymemory((system-char))-system-threading-cancellationtoken)")
 * `void Write(TextWriter, ReadOnlySpan<char>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write#system-io-textwriter-write(system-readonlyspan((system-char)))")
 * `void WriteLine(TextWriter, ReadOnlySpan<char>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeline#system-io-textwriter-writeline(system-readonlyspan((system-char)))")


#### TimeOnly

 * `bool TryFormat(TimeOnly, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.tryformat")


#### TimeSpan

 * `int Nanoseconds(TimeSpan)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.timespan.nanoseconds")
 * `int Microseconds(TimeSpan)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microsecond")
 * `bool TryFormat(TimeSpan, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.timespan.tryformat#system-timespan-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider)")


#### Type

 * `bool IsGenericMethodParameter(Type)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.type.isgenericmethodparameter")
 * `bool IsAssignableTo<T>(Type)`
 * `bool IsAssignableFrom<T>(Type)`
 * `bool IsAssignableTo(Type, Type?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.type.isassignableto")


#### uint

 * `bool TryFormat(uint, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryformat")


#### ulong

 * `bool TryFormat(ulong, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryformat")


#### ushort

 * `bool TryFormat(ushort, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryformat")


#### XDocument

 * `Task SaveAsync(XDocument, XmlWriter, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync#system-xml-linq-xdocument-saveasync(system-xml-xmlwriter-system-threading-cancellationtoken)")
 * `Task SaveAsync(XDocument, Stream, SaveOptions, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync#system-xml-linq-xdocument-saveasync(system-io-stream-system-xml-linq-saveoptions-system-threading-cancellationtoken)")
 * `Task SaveAsync(XDocument, TextWriter, SaveOptions, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync#system-xml-linq-xdocument-saveasync(system-io-textwriter-system-xml-linq-saveoptions-system-threading-cancellationtoken)")


