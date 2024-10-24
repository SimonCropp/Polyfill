### Extension methods

#### CancellationToken

 * `CancellationTokenRegistration Register(CancellationToken, Action<object?, CancellationToken>, object?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.register#system-threading-cancellationtoken-register(system-action((system-object-system-threading-cancellationtoken))-system-object)")
 * `CancellationTokenRegistration UnsafeRegister(CancellationToken, Action<object?>, object?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister#system-threading-cancellationtoken-unsaferegister(system-action((system-object))-system-object)")
 * `CancellationTokenRegistration UnsafeRegister(CancellationToken, Action<object?, CancellationToken>, object?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister#system-threading-cancellationtoken-unsaferegister(system-action((system-object-system-threading-cancellationtoken))-system-object)")


#### CancellationTokenSource

 * `Task CancelAsync(CancellationTokenSource)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.cancelasync")


#### ConcurrentDictionary<TKey,TValue>

 * `TValue GetOrAdd<TKey, TValue, TArg>(ConcurrentDictionary<TKey,TValue>, TKey, Func<TKey, TArg, TValue>, TArg) where TKey : notnull` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.getoradd#system-collections-concurrent-concurrentdictionary-2-getoradd-1(-0-system-func((-0-0-1))-0)")


#### DateOnly

 * `bool TryFormat(DateOnly, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.tryformat")


#### DateTime

 * `DateTime AddMicroseconds(DateTime, double)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.addmicroseconds")
 * `int Microsecond(DateTime)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.microsecond")
 * `int Nanosecond(DateTime)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.nanosecond")
 * `bool TryFormat(DateTime, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryformat")


#### DateTimeOffset

 * `DateTimeOffset AddMicroseconds(DateTimeOffset, double)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.addmicroseconds")
 * `int Microsecond(DateTimeOffset)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.microsecond")
 * `int Nanosecond(DateTimeOffset)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.nanosecond")
 * `bool TryFormat(DateTimeOffset, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryformat")


#### Dictionary<TKey, TValue>

 * `bool Remove<TKey, TValue>(Dictionary<TKey, TValue>, TKey, TValue) where TKey : notnull` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.remove")
 * `bool TryAdd<TKey, TValue>(Dictionary<TKey, TValue>, TKey, TValue) where TKey : notnull` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.tryadd")


#### EventInfo

 * `NullabilityState GetNullability(EventInfo)`
 * `NullabilityInfo GetNullabilityInfo(EventInfo)`
 * `bool IsNullable(EventInfo)`


#### FieldInfo

 * `NullabilityState GetNullability(FieldInfo)`
 * `NullabilityInfo GetNullabilityInfo(FieldInfo)`
 * `bool IsNullable(FieldInfo)`


#### HashSet<T>

 * `bool TryGetValue<T>(HashSet<T>, T, T)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.trygetvalue")


#### HttpClient

 * `Task<byte[]> GetByteArrayAsync(HttpClient, string, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync#system-net-http-httpclient-getbytearrayasync(system-string-system-threading-cancellationtoken)")
 * `Task<byte[]> GetByteArrayAsync(HttpClient, Uri, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync#system-net-http-httpclient-getbytearrayasync(system-uri-system-threading-cancellationtoken)")
 * `Task<Stream> GetStreamAsync(HttpClient, string, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync#system-net-http-httpclient-getstreamasync(system-string-system-threading-cancellationtoken)")
 * `Task<Stream> GetStreamAsync(HttpClient, Uri, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync#system-net-http-httpclient-getstreamasync(system-uri-system-threading-cancellationtoken)")
 * `Task<string> GetStringAsync(HttpClient, string, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync#system-net-http-httpclient-getstringasync(system-string-system-threading-cancellationtoken)")
 * `Task<string> GetStringAsync(HttpClient, Uri, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync#system-net-http-httpclient-getstringasync(system-uri-system-threading-cancellationtoken)")


#### HttpContent

 * `Task<byte[]> ReadAsByteArrayAsync(HttpContent, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasbytearrayasync#system-net-http-httpcontent-readasbytearrayasync(system-threading-cancellationtoken)")
 * `Task<Stream> ReadAsStreamAsync(HttpContent, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstreamasync#system-net-http-httpcontent-readasstreamasync(system-threading-cancellationtoken)")
 * `Task<string> ReadAsStringAsync(HttpContent, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstringasync#system-net-http-httpcontent-readasstringasync(system-threading-cancellationtoken)")


#### IDictionary<TKey, TValue>

 * `ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(IDictionary<TKey, TValue>) where TKey : notnull` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly#system-collections-generic-collectionextensions-asreadonly-2(system-collections-generic-idictionary((-0-1)))")


#### IEnumerable<TFirst>

 * `IEnumerable<(TFirst First, TSecond Second, TThird Third)> Zip<TFirst, TSecond, TThird>(IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip#system-linq-enumerable-zip-3(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-collections-generic-ienumerable((-2)))")


#### IEnumerable<TSource>

 * `IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(IEnumerable<TSource>, Func<TSource, TKey>, TAccumulate, Func<TAccumulate, TSource, TAccumulate>, IEqualityComparer<TKey>?) where TKey : notnull` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-func((-1-2))-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1)))")
 * `IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(IEnumerable<TSource>, Func<TSource, TKey>, Func<TKey, TAccumulate>, Func<TAccumulate, TSource, TAccumulate>, IEqualityComparer<TKey>?) where TKey : notnull` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-2-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1)))")
 * `IEnumerable<TSource> Append<TSource>(IEnumerable<TSource>, TSource)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.append")
 * `IEnumerable<TSource[]> Chunk<TSource>(IEnumerable<TSource>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.chunk")
 * `IEnumerable<KeyValuePair<TKey, int>> CountBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>?) where TKey : notnull` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.countby")
 * `IEnumerable<TSource> DistinctBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1)))")
 * `IEnumerable<TSource> DistinctBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1)))")
 * `TSource ElementAt<TSource>(IEnumerable<TSource>, Index)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementat#system-linq-enumerable-elementat-1(system-collections-generic-ienumerable((-0))-system-index)")
 * `TSource? ElementAtOrDefault<TSource>(IEnumerable<TSource>, Index)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementatordefault#system-linq-enumerable-elementatordefault-1(system-collections-generic-ienumerable((-0))-system-index)")
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, TSource)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0)))")
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, TSource, IEqualityComparer<TSource>?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0)))")
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, IEqualityComparer<TSource>, TSource[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0)))")
 * `IEnumerable<TSource> ExceptBy<TSource, TKey>(IEnumerable<TSource>, IEnumerable<TKey>, Func<TSource, TKey>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1)))")
 * `IEnumerable<TSource> ExceptBy<TSource, TKey>(IEnumerable<TSource>, IEnumerable<TKey>, Func<TSource, TKey>, IEqualityComparer<TKey>?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1)))")
 * `TSource FirstOrDefault<TSource>(IEnumerable<TSource>, Func<TSource, bool>, TSource)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0)")
 * `TSource FirstOrDefault<TSource>(IEnumerable<TSource>, TSource)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-0)")
 * `IEnumerable<(int Index, TSource Item)> Index<TSource>(IEnumerable<TSource>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.index#system-linq-enumerable-index-1(system-collections-generic-ienumerable((-0)))")
 * `TSource LastOrDefault<TSource>(IEnumerable<TSource>, TSource)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault#system-linq-enumerable-lastordefault-1(system-collections-generic-ienumerable((-0))-0)")
 * `TSource LastOrDefault<TSource>(IEnumerable<TSource>, Func<TSource, bool>, TSource)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault#system-linq-enumerable-lastordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0)")
 * `TSource? Max<TSource>(IEnumerable<TSource>, IComparer<TSource>?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.max?view=net-8.0#system-linq-enumerable-max-1(system-collections-generic-ienumerable((-0))-system-collections-generic-icomparer((-0)))")
 * `TSource? MaxBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1)))")
 * `TSource? MaxBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IComparer<TKey>?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1)))")
 * `TSource? Min<TSource>(IEnumerable<TSource>, IComparer<TSource>?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.min?view=net-8.0#system-linq-enumerable-min-1(system-collections-generic-ienumerable((-0))-system-collections-generic-icomparer((-0)))")
 * `TSource? MinBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.minby#system-linq-enumerable-minby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1)))")
 * `TSource? MinBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IComparer<TKey>?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.minby#system-linq-enumerable-minby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1)))")
 * `TSource SingleOrDefault<TSource>(IEnumerable<TSource>, Func<TSource, bool>, TSource)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.singleordefault#system-linq-enumerable-singleordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0)")
 * `TSource SingleOrDefault<TSource>(IEnumerable<TSource>, TSource)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.singleordefault#system-linq-enumerable-singleordefault-1(system-collections-generic-ienumerable((-0))-0)")
 * `IEnumerable<TSource> SkipLast<TSource>(IEnumerable<TSource>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.skiplast")
 * `IEnumerable<TSource> Take<TSource>(IEnumerable<TSource>, Range)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.take?view=net-8.0#system-linq-enumerable-take-1(system-collections-generic-ienumerable((-0))-system-range)")
 * `IEnumerable<TSource> TakeLast<TSource>(IEnumerable<TSource>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.takelast")
 * `HashSet<TSource> ToHashSet<TSource>(IEnumerable<TSource>, IEqualityComparer<TSource>?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tohashset#system-linq-enumerable-tohashset-1(system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0)))")
 * `bool TryGetNonEnumeratedCount<TSource>(IEnumerable<TSource>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.trygetnonenumeratedcount")


#### IList<T>

 * `ReadOnlyCollection<T> AsReadOnly<T>(IList<T>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly#system-collections-generic-collectionextensions-asreadonly-1(system-collections-generic-ilist((-0)))")


#### IReadOnlyDictionary<TKey, TValue>

 * `TValue? GetValueOrDefault<TKey, TValue>(IReadOnlyDictionary<TKey, TValue>, TKey) where TKey : notnull` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault")
 * `TValue GetValueOrDefault<TKey, TValue>(IReadOnlyDictionary<TKey, TValue>, TKey, TValue) where TKey : notnull` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault#system-collections-generic-collectionextensions-getvalueordefault-2(system-collections-generic-ireadonlydictionary((-0-1))-0-1)")


#### KeyValuePair<TKey, TValue>

 * `void Deconstruct<TKey, TValue>(KeyValuePair<TKey, TValue>, TKey, TValue)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2.deconstruct")


#### List<T>

 * `void AddRange<T>(List<T>, ReadOnlySpan<T>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.addrange")
 * `void CopyTo<T>(List<T>, Span<T>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.copyto")
 * `void InsertRange<T>(List<T>, int, ReadOnlySpan<T>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.insertrange")


#### MemberInfo

 * `NullabilityState GetNullability(MemberInfo)`
 * `NullabilityInfo GetNullabilityInfo(MemberInfo)`
 * `bool HasSameMetadataDefinitionAs(MemberInfo, MemberInfo)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo.hassamemetadatadefinitionas")
 * `bool IsNullable(MemberInfo)`


#### ParameterInfo

 * `NullabilityState GetNullability(ParameterInfo)`
 * `NullabilityInfo GetNullabilityInfo(ParameterInfo)`
 * `bool IsNullable(ParameterInfo)`


#### Process

 * `Task WaitForExitAsync(Process, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexitasync")


#### PropertyInfo

 * `NullabilityState GetNullability(PropertyInfo)`
 * `NullabilityInfo GetNullabilityInfo(PropertyInfo)`
 * `bool IsNullable(PropertyInfo)`


#### Random

 * `void Shuffle<T>(Random, T[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes#system-random-nextbytes(system-span((system-byte)))")
 * `void Shuffle<T>(Random, Span<T>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes#system-random-nextbytes(system-span((system-byte)))")


#### ReadOnlySpan<char>

 * `SpanLineEnumerator EnumerateLines(ReadOnlySpan<char>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines#system-memoryextensions-enumeratelines(system-readonlyspan((system-char)))")


#### ReadOnlySpan<T>

 * `bool EndsWith<T>(ReadOnlySpan<T>, T) where T : IEquatable<T>?` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith#system-memoryextensions-endswith-1(system-readonlyspan((-0))-0)")
 * `SpanSplitEnumerator<T> Split<T>(ReadOnlySpan<T>, T) where T : IEquatable<T>` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split#system-memoryextensions-split-1(system-readonlyspan((-0))-0)")
 * `SpanSplitEnumerator<T> Split<T>(ReadOnlySpan<T>, ReadOnlySpan<T>) where T : IEquatable<T>` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split#system-memoryextensions-split-1(system-readonlyspan((-0))-system-readonlyspan((-0)))")
 * `bool StartsWith<T>(ReadOnlySpan<T>, T) where T : IEquatable<T>?` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith#system-memoryextensions-endswith-1(system-readonlyspan((-0))-0)")


#### Regex

 * `ValueMatchEnumerator EnumerateMatches(Regex, ReadOnlySpan<char>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char)))")
 * `ValueMatchEnumerator EnumerateMatches(Regex, ReadOnlySpan<char>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-int32)")
 * `bool IsMatch(Regex, ReadOnlySpan<char>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-int32)")
 * `bool IsMatch(Regex, ReadOnlySpan<char>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char)))")


#### SortedList<TKey, TValue>

 * `TKey GetKeyAtIndex<TKey, TValue>(SortedList<TKey, TValue>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getkeyatindex")
 * `TValue GetValueAtIndex<TKey, TValue>(SortedList<TKey, TValue>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getvalueatindex")


#### Span<char>

 * `SpanLineEnumerator EnumerateLines(Span<char>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines#system-memoryextensions-enumeratelines(system-span((system-char)))")


#### Stream

 * `Task CopyToAsync(Stream, Stream, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.copytoasync#system-io-stream-copytoasync(system-io-stream-system-threading-cancellationtoken)")


#### string

 * `bool Contains(string, string, StringComparison)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-string-system-stringcomparison)")
 * `bool Contains(string, char)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char)")
 * `void CopyTo(string, Span<char>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.copyto")
 * `bool EndsWith(string, char)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char)")
 * `int GetHashCode(string, StringComparison)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode#system-string-gethashcode(system-stringcomparison)")
 * `string[] Split(string, char, StringSplitOptions)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.split#system-string-split(system-char-system-stringsplitoptions)")
 * `string[] Split(string, char, int, StringSplitOptions)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.split#system-string-split(system-char-system-int32-system-stringsplitoptions)")
 * `bool StartsWith(string, char)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.contains#system-string-contains(system-char)")
 * `bool TryCopyTo(string, Span<char>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.trycopyto")


#### StringBuilder

 * `StringBuilder Append(StringBuilder, StringBuilder.AppendInterpolatedStringHandler)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-text-stringbuilder-appendinterpolatedstringhandler@)")
 * `StringBuilder Append(StringBuilder, IFormatProvider?, StringBuilder.AppendInterpolatedStringHandler)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@)")
 * `StringBuilder Append(StringBuilder, ReadOnlySpan<char>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-readonlyspan((system-char)))")
 * `StringBuilder Append(StringBuilder, AppendInterpolatedStringHandler)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-text-stringbuilder-appendinterpolatedstringhandler@)")
 * `StringBuilder Append(StringBuilder, IFormatProvider?, AppendInterpolatedStringHandler)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append#system-text-stringbuilder-append(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@)")
 * `StringBuilder AppendJoin(StringBuilder, string, string[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-string-system-string())")
 * `StringBuilder AppendJoin(StringBuilder, string, Object[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-string-system-object())")
 * `StringBuilder AppendJoin(StringBuilder, char, string[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-char-system-string())")
 * `StringBuilder AppendJoin(StringBuilder, char, object[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin(system-char-system-object())")
 * `StringBuilder AppendJoin<T>(StringBuilder, char, T[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0)))")
 * `StringBuilder AppendJoin<T>(StringBuilder, string, T[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0)))")
 * `StringBuilder AppendLine(StringBuilder, StringBuilder.AppendInterpolatedStringHandler)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline#system-text-stringbuilder-appendline(system-text-stringbuilder-appendinterpolatedstringhandler@)")
 * `StringBuilder AppendLine(StringBuilder, IFormatProvider?, StringBuilder.AppendInterpolatedStringHandler)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline#system-text-stringbuilder-appendline(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@)")
 * `StringBuilder AppendLine(StringBuilder, AppendInterpolatedStringHandler)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline#system-text-stringbuilder-appendline(system-text-stringbuilder-appendinterpolatedstringhandler@)")
 * `StringBuilder AppendLine(StringBuilder, IFormatProvider?, AppendInterpolatedStringHandler)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline#system-text-stringbuilder-appendline(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@)")
 * `void CopyTo(StringBuilder, int, Span<char>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.copyto#system-text-stringbuilder-copyto(system-int32-system-span((system-char))-system-int32)")
 * `bool Equals(StringBuilder, ReadOnlySpan<char>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.equals#system-text-stringbuilder-equals(system-readonlyspan((system-char)))")
 * `ChunkEnumerator GetChunks(StringBuilder)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.getchunks")
 * `StringBuilder Replace(StringBuilder, ReadOnlySpan<char>, ReadOnlySpan<char>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace#system-text-stringbuilder-replace(system-readonlyspan((system-char))-system-readonlyspan((system-char)))")
 * `StringBuilder Replace(StringBuilder, ReadOnlySpan<char>, ReadOnlySpan<char>, int, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace#system-text-stringbuilder-replace(system-char-system-char-system-int32-system-int32)")


#### Task

 * `Task WaitAsync(Task, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-threading-cancellationtoken)")
 * `Task WaitAsync(Task, TimeSpan)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-timespan)")
 * `Task WaitAsync(Task, TimeSpan, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-timespan-system-threading-cancellationtoken)")


#### Task<TResult>

 * `Task<TResult> WaitAsync<TResult>(Task<TResult>, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync#system-threading-tasks-task-waitasync(system-threading-cancellationtoken)")
 * `Task<TResult> WaitAsync<TResult>(Task<TResult>, TimeSpan)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync#system-threading-tasks-task-1-waitasync(system-timespan)")
 * `Task<TResult> WaitAsync<TResult>(Task<TResult>, TimeSpan, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync#system-threading-tasks-task-1-waitasync(system-timespan-system-threading-cancellationtoken)")


#### TaskCompletionSource<T>

 * `void SetCanceled<T>(TaskCompletionSource<T>, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1.setcanceled#system-threading-tasks-taskcompletionsource-1-setcanceled(system-threading-cancellationtoken)")


#### TextReader

 * `Task<string> ReadLineAsync(TextReader, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync#system-io-textreader-readlineasync(system-threading-cancellationtoken)")
 * `Task<string> ReadToEndAsync(TextReader, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync#system-io-textreader-readtoendasync(system-threading-cancellationtoken)")


#### TextWriter

 * `Task FlushAsync(TextWriter, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.flushasync#system-io-textwriter-flushasync(system-threading-cancellationtoken)")
 * `void Write(TextWriter, StringBuilder?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write#system-io-textwriter-write(system-text-stringbuilder)")
 * `Task WriteAsync(TextWriter, StringBuilder?, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken)")


#### TimeOnly

 * `bool TryFormat(TimeOnly, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.tryformat")


#### TimeSpan

 * `int Microseconds(TimeSpan)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microseconds")
 * `int Nanoseconds(TimeSpan)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.timespan.nanoseconds")


#### Type

 * `MethodInfo? GetMethod(Type, string, int, BindingFlags, Type[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.type.getmethod#system-type-getmethod(system-string-system-int32-system-reflection-bindingflags-system-type())")
 * `bool IsAssignableFrom<T>(Type)`
 * `bool IsAssignableTo<T>(Type)`
 * `bool IsAssignableTo(Type, Type?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.type.isassignableto")
 * `bool IsGenericMethodParameter(Type)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.type.isgenericmethodparameter")


#### XDocument

 * `Task SaveAsync(XDocument, XmlWriter, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync#system-xml-linq-xdocument-saveasync(system-xml-xmlwriter-system-threading-cancellationtoken)")
 * `Task SaveAsync(XDocument, Stream, SaveOptions, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync#system-xml-linq-xdocument-saveasync(system-io-stream-system-xml-linq-saveoptions-system-threading-cancellationtoken)")
 * `Task SaveAsync(XDocument, TextWriter, SaveOptions, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync#system-xml-linq-xdocument-saveasync(system-io-textwriter-system-xml-linq-saveoptions-system-threading-cancellationtoken)")


### Static helpers

#### EnumPolyfill

 * `TEnum[] GetValues<TEnum>() where TEnum : struct, Enum` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.enum.getvalues")
 * `bool IsDefined<TEnum>(TEnum) where TEnum : struct, Enum` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.enum.isdefined#system-enum-isdefined-1(-0)")
 * `string[] GetNames<TEnum>() where TEnum : struct, Enum` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.enum.getnames")
 * `TEnum Parse<TEnum>(string) where TEnum : struct, Enum` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse#system-enum-parse-1(system-string-system-boolean)")
 * `TEnum Parse<TEnum>(string, bool) where TEnum : struct, Enum` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse#system-enum-parse-1(system-string-system-boolean)")
 * `TEnum Parse<TEnum>(ReadOnlySpan<char>) where TEnum : struct, Enum` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse#system-enum-parse-1(system-readonlyspan((system-char)))")
 * `TEnum Parse<TEnum>(ReadOnlySpan<char>, bool) where TEnum : struct, Enum` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse#system-enum-parse-1(system-readonlyspan((system-char))-system-boolean)")
 * `bool TryParse<TEnum>(ReadOnlySpan<char>, TEnum) where TEnum : struct, Enum` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryparse#system-enum-tryparse-1(system-readonlyspan((system-char))-0@)")
 * `bool TryParse<TEnum>(ReadOnlySpan<char>, bool, TEnum) where TEnum : struct, Enum` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryparse#system-enum-tryparse-1(system-readonlyspan((system-char))-system-boolean-0@)")


#### RegexPolyfill

 * `bool IsMatch(ReadOnlySpan<char>, string, RegexOptions, TimeSpan)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan)")
 * `bool IsMatch(ReadOnlySpan<char>, string, RegexOptions)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions)")
 * `bool IsMatch(ReadOnlySpan<char>, string)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string)")
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, string)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string)")
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, string, RegexOptions, TimeSpan)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan)")
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, string, RegexOptions)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions)")


#### StringPolyfill

 * `string Join(char, string[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-char-system-string())")
 * `string Join(char, object[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-char-system-object())")
 * `string Join(char, string?[], int, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join(system-char-system-string()-system-int32-system-int32)")
 * `string Join<T>(char, IEnumerable<T>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.string.join#system-string-join-1(system-char-system-collections-generic-ienumerable((-0)))")


#### BytePolyfill

 * `bool TryParse(string?, IFormatProvider?, byte)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-string-system-iformatprovider-system-byte@)")
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, byte)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-byte@)")
 * `bool TryParse(ReadOnlySpan<char>, byte)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-char))-system-byte@)")
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, byte)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-byte@)")
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, byte)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-byte@)")
 * `bool TryParse(ReadOnlySpan<byte>, byte)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-byte@)")
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, byte)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse#system-byte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-byte@)")


#### GuidPolyfill

 * `bool TryParse(string?, IFormatProvider?, Guid)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse#system-guid-tryparse(system-string-system-iformatprovider-system-guid@)")
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, Guid)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse#system-guid-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-guid@)")
 * `bool TryParse(ReadOnlySpan<char>, Guid)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-8.0#system-guid-tryparse(system-readonlyspan((system-char))-system-guid@)")


#### DateTimePolyfill

 * `bool TryParse(string?, IFormatProvider?, DateTime)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse#system-datetime-tryparse(system-string-system-iformatprovider-system-datetime@)")
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTime)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse#system-datetime-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-datetime@)")
 * `bool TryParse(ReadOnlySpan<char>, DateTime)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse#system-datetime-tryparse(system-readonlyspan((system-char))-system-datetime@)")
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTime)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse#system-datetime-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@)")
 * `bool TryParseExact(ReadOnlySpan<char>, string, IFormatProvider?, DateTimeStyles, DateTime)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparseexact#system-datetime-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@)")
 * `bool TryParseExact(ReadOnlySpan<char>, ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTime)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparseexact#system-datetime-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@)")


#### DateTimeOffsetPolyfill

 * `bool TryParse(string?, IFormatProvider?, DateTimeOffset)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse#system-datetimeoffset-tryparse(system-string-system-iformatprovider-system-datetimeoffset@)")
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTimeOffset)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-datetimeoffset@)")
 * `bool TryParse(ReadOnlySpan<char>, DateTimeOffset)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-datetimeoffset@)")
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTimeOffset)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@)")
 * `bool TryParseExact(ReadOnlySpan<char>, string, IFormatProvider?, DateTimeStyles, DateTimeOffset)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparseexact#system-datetimeoffset-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@)")
 * `bool TryParseExact(ReadOnlySpan<char>, ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTimeOffset)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparseexact#system-datetimeoffset-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@)")


#### DoublePolyfill

 * `bool TryParse(string?, IFormatProvider?, double)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-string-system-iformatprovider-system-double@)")
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, double)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-double@)")
 * `bool TryParse(ReadOnlySpan<char>, double)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-char))-system-double@)")
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, double)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-double@)")
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, double)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-double@)")
 * `bool TryParse(ReadOnlySpan<byte>, double)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-byte))-system-double@)")
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, double)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse#system-double-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-double@)")


#### IntPolyfill

 * `bool TryParse(string?, IFormatProvider?, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-string-system-iformatprovider-system-int32@)")
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int32@)")
 * `bool TryParse(ReadOnlySpan<char>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-char))-system-int32@)")
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int32@)")
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int32@)")
 * `bool TryParse(ReadOnlySpan<byte>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int32@)")
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse#system-int32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int32@)")


#### LongPolyfill

 * `bool TryParse(string?, IFormatProvider?, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-string-system-iformatprovider-system-int64@)")
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int64@)")
 * `bool TryParse(ReadOnlySpan<char>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-char))-system-int64@)")
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int64@)")
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int64@)")
 * `bool TryParse(ReadOnlySpan<byte>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int64@)")
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse#system-int64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int64@)")


#### SBytePolyfill

 * `bool TryParse(string?, IFormatProvider?, sbyte)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-string-system-iformatprovider-system-sbyte@)")
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, sbyte)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-sbyte@)")
 * `bool TryParse(ReadOnlySpan<char>, sbyte)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-char))-system-sbyte@)")
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, sbyte)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-sbyte@)")
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, sbyte)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@)")
 * `bool TryParse(ReadOnlySpan<byte>, sbyte)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@)")
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, sbyte)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse#system-sbyte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@)")


#### ShortPolyfill

 * `bool TryParse(string?, IFormatProvider?, short)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-string-system-iformatprovider-system-int16@)")
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, short)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int16@)")
 * `bool TryParse(ReadOnlySpan<char>, short)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-char))-system-int16@)")
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, short)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int16@)")
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, short)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int16@)")
 * `bool TryParse(ReadOnlySpan<byte>, short)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int16@)")
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, short)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse#system-int16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int16@)")


#### UIntPolyfill

 * `bool TryParse(string?, IFormatProvider?, uint)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-string-system-iformatprovider-system-uint32@)")
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, uint)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint32@)")
 * `bool TryParse(ReadOnlySpan<char>, uint)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-char))-system-uint32@)")
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, uint)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint32@)")
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, uint)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint32@)")
 * `bool TryParse(ReadOnlySpan<byte>, uint)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint32@)")
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, uint)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse#system-uint32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint32@)")


#### ULongPolyfill

 * `bool TryParse(string?, IFormatProvider?, ulong)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-string-system-iformatprovider-system-uint64@)")
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, ulong)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint64@)")
 * `bool TryParse(ReadOnlySpan<char>, ulong)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-char))-system-uint64@)")
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, ulong)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint64@)")
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, ulong)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint64@)")
 * `bool TryParse(ReadOnlySpan<byte>, ulong)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint64@)")
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, ulong)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse#system-uint64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint64@)")


#### UShortPolyfill

 * `bool TryParse(string?, IFormatProvider?, ushort)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-string-system-iformatprovider-system-uint16@)")
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, ushort)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint16@)")
 * `bool TryParse(ReadOnlySpan<char>, ushort)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-char))-system-uint16@)")
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, ushort)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint16@)")
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, ushort)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint16@)")
 * `bool TryParse(ReadOnlySpan<byte>, ushort)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint16@)")
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, ushort)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse#system-uint16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint16@)")


#### Guard

 * `void FileExists(string)`
 * `void DirectoryExists(string)`
 * `void NotEmpty(string?)`
 * `void NotEmpty<T>(T?) where T : IEnumerable`
 * `T NotNull<T>(T?) where T : class`
 * `string NotNull(string?)`
 * `string NotNullOrEmpty(string?)`
 * `T NotNullOrEmpty<T>(T?) where T : IEnumerable`
 * `string NotNullOrWhiteSpace(string?)`
 * `void NotWhiteSpace(string?)`


#### Lock

 * `void Enter()`
 * `bool TryEnter()`
 * `bool TryEnter(TimeSpan)`
 * `bool TryEnter(int)`
 * `void Exit()`
 * `Scope EnterScope()`


#### TaskCompletionSource
