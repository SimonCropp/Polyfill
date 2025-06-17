### Extension methods

#### CancellationToken

 * `CancellationTokenRegistration UnsafeRegister(Action<object?>, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister?view=net-10.0#system-threading-cancellationtoken-unsaferegister(system-action((system-object))-system-object))
 * `CancellationTokenRegistration Register(Action<object?, CancellationToken>, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.register?view=net-10.0#system-threading-cancellationtoken-register(system-action((system-object-system-threading-cancellationtoken))-system-object))
 * `CancellationTokenRegistration UnsafeRegister(Action<object?, CancellationToken>, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister?view=net-10.0#system-threading-cancellationtoken-unsaferegister(system-action((system-object-system-threading-cancellationtoken))-system-object))


#### CancellationTokenSource

 * `Task CancelAsync()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.cancelasync?view=net-10.0)


#### ConcurrentBag

 * `void Clear()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentbag-1.clear?view=net-10.0)


#### ConcurrentDictionary

 * `TValue GetOrAdd<TArg>(TKey, Func<TKey, TArg, TValue>, TArg)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.getoradd?view=net-10.0#system-collections-concurrent-concurrentdictionary-2-getoradd-1(-0-system-func((-0-0-1))-0))


#### ConcurrentQueue

 * `void Clear()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentqueue-1.clear?view=net-10.0)


#### DateOnly

 * `void Deconstruct(DateOnly, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.deconstruct?view=net-10.0)


#### DateTime

 * `void Deconstruct(DateTime, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.deconstruct?view=net-10.0#system-datetime-deconstruct(system-int32@-system-int32@-system-int32@))
 * `void Deconstruct(DateTime, DateOnly, TimeOnly)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.deconstruct?view=net-10.0#system-datetime-deconstruct(system-dateonly@-system-timeonly@))


#### DateTimeOffset

 * `void Deconstruct(DateTimeOffset, DateOnly, TimeOnly, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.deconstruct?view=net-10.0)


#### DefaultInterpolatedStringHandler

 * `void Clear(DefaultInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.defaultinterpolatedstringhandler.clear?view=net-10.0)


#### Delegate

 * `InvocationListEnumerator<TDelegate> EnumerateInvocationList<TDelegate>(TDelegate?) where TDelegate : Delegate` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.delegate.enumerateinvocationlist?view=net-10.0)
 * `bool MoveNext()`
 * `InvocationListEnumerator<TDelegate> GetEnumerator()`
 * `Current`
 * `HasSingleTarget` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.delegate.hassingletarget?view=net-10.0)


#### Dictionary

 * `ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(IDictionary<TKey, TValue>) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly?view=net-10.0#system-collections-generic-collectionextensions-asreadonly-2(system-collections-generic-idictionary((-0-1))))
 * `void EnsureCapacity<TKey, TValue>(Dictionary<TKey, TValue>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.ensurecapacity?view=net-10.0)
 * `void TrimExcess<TKey, TValue>(Dictionary<TKey, TValue>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.trimexcess?view=net-10.0#system-collections-generic-dictionary-2-trimexcess(system-int32))
 * `void TrimExcess<TKey, TValue>(Dictionary<TKey, TValue>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.ensurecapacity?view=net-10.0)
 * `bool TryAdd<TKey, TValue>(IDictionary<TKey, TValue>, TKey, TValue) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.tryadd?view=net-10.0)
 * `bool Remove<TKey, TValue>(IDictionary<TKey, TValue>, TKey, TValue) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.remove?view=net-10.0)


#### DictionaryEntry

 * `void Deconstruct(DictionaryEntry, object, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.dictionaryentry.deconstruct?view=net-10.0#system-collections-dictionaryentry-deconstruct(system-object@-system-object@))


#### Encoding

 * `int GetBytes(Encoding, ReadOnlySpan<char>, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytes?view=net-10.0#system-text-encoding-getbytes(system-readonlyspan((system-char))-system-span((system-byte))))
 * `string GetString(Encoding, ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getstring?view=net-10.0#system-text-encoding-getstring(system-readonlyspan((system-byte))))


#### Encoding_GetByteCount

 * `int GetByteCount(Encoding, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytecount?view=net-10.0#system-text-encoding-getbytecount(system-readonlyspan((system-char))))


#### Encoding_GetCharCount

 * `int GetCharCount(Encoding, ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getcharcount?view=net-10.0#system-text-encoding-getcharcount(system-readonlyspan((system-byte))))


#### Encoding_GetChars

 * `int GetChars(Encoding, ReadOnlySpan<byte>, Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getchars?view=net-10.0#system-text-encoding-getchars(system-readonlyspan((system-byte))-system-span((system-char))))


#### Encoding_TryGet

 * `bool TryGetChars(Encoding, ReadOnlySpan<byte>, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.trygetchars?view=net-10.0)
 * `bool TryGetBytes(Encoding, ReadOnlySpan<char>, Span<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.trygetbytes?view=net-10.0)


#### Guid

 * `bool TryFormat(Guid, Span<byte>, int, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryformat?view=net-10.0#system-guid-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))))


#### HashSet

 * `bool TryGetValue<T>(HashSet<T>, T, T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.trygetvalue?view=net-10.0)
 * `void EnsureCapacity<T>(HashSet<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.ensurecapacity?view=net-10.0#system-collections-generic-hashset-1-ensurecapacity(system-int32))
 * `void TrimExcess<T>(HashSet<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.trimexcess?view=net-10.0#system-collections-generic-hashset-1-trimexcess(system-int32))


#### HttpClient

 * `Task<Stream> GetStreamAsync(HttpClient, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync?view=net-10.0#system-net-http-httpclient-getstreamasync(system-string-system-threading-cancellationtoken))
 * `Task<Stream> GetStreamAsync(HttpClient, Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync?view=net-10.0#system-net-http-httpclient-getstreamasync(system-uri-system-threading-cancellationtoken))
 * `Task<byte[]> GetByteArrayAsync(HttpClient, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync?view=net-10.0#system-net-http-httpclient-getbytearrayasync(system-string-system-threading-cancellationtoken))
 * `Task<byte[]> GetByteArrayAsync(HttpClient, Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync?view=net-10.0#system-net-http-httpclient-getbytearrayasync(system-uri-system-threading-cancellationtoken))
 * `Task<string> GetStringAsync(HttpClient, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync?view=net-10.0#system-net-http-httpclient-getstringasync(system-string-system-threading-cancellationtoken))
 * `Task<string> GetStringAsync(HttpClient, Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync?view=net-10.0#system-net-http-httpclient-getstringasync(system-uri-system-threading-cancellationtoken))


#### HttpContent

 * `Task<Stream> ReadAsStreamAsync(HttpContent, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstreamasync?view=net-10.0#system-net-http-httpcontent-readasstreamasync(system-threading-cancellationtoken))
 * `Task<byte[]> ReadAsByteArrayAsync(HttpContent, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasbytearrayasync?view=net-10.0#system-net-http-httpcontent-readasbytearrayasync(system-threading-cancellationtoken))
 * `Task<string> ReadAsStringAsync(HttpContent, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstringasync?view=net-10.0#system-net-http-httpcontent-readasstringasync(system-threading-cancellationtoken))


#### IEnumerable_AggregateBy

 * `IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(IEnumerable<TSource>, Func<TSource, TKey>, TAccumulate, Func<TAccumulate, TSource, TAccumulate>, IEqualityComparer<TKey>?) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby?view=net-10.0#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-func((-1-2))-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1))))
 * `IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(IEnumerable<TSource>, Func<TSource, TKey>, Func<TKey, TAccumulate>, Func<TAccumulate, TSource, TAccumulate>, IEqualityComparer<TKey>?) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby?view=net-10.0#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-2-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1))))


#### IEnumerable_Append

 * `IEnumerable<TSource> Append<TSource>(IEnumerable<TSource>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.append?view=net-10.0)


#### IEnumerable_Chunk

 * `IEnumerable<TSource[]> Chunk<TSource>(IEnumerable<TSource>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.chunk?view=net-10.0)


#### IEnumerable_CountBy

 * `IEnumerable<KeyValuePair<TKey, int>> CountBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>?) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.countby?view=net-10.0)


#### IEnumerable_DistinctBy

 * `IEnumerable<TSource> DistinctBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby?view=net-10.0#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `IEnumerable<TSource> DistinctBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby?view=net-10.0#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1))))


#### IEnumerable_ElementAt

 * `TSource ElementAt<TSource>(IEnumerable<TSource>, Index)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementat?view=net-10.0#system-linq-enumerable-elementat-1(system-collections-generic-ienumerable((-0))-system-index))
 * `TSource? ElementAtOrDefault<TSource>(IEnumerable<TSource>, Index)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementatordefault?view=net-10.0#system-linq-enumerable-elementatordefault-1(system-collections-generic-ienumerable((-0))-system-index))


#### IEnumerable_Except

 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-10.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))))
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, TSource, IEqualityComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-10.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, IEqualityComparer<TSource>?, TSource[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-10.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `IEnumerable<TSource> ExceptBy<TSource, TKey>(IEnumerable<TSource>, IEnumerable<TKey>, Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby?view=net-10.0#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))))
 * `IEnumerable<TSource> ExceptBy<TSource, TKey>(IEnumerable<TSource>, IEnumerable<TKey>, Func<TSource, TKey>, IEqualityComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby?view=net-10.0#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1))))


#### IEnumerable_FirstOrDefault

 * `TSource FirstOrDefault<TSource>(IEnumerable<TSource>, Func<TSource, bool>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault?view=net-10.0#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource FirstOrDefault<TSource>(IEnumerable<TSource>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault?view=net-10.0#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-0))


#### IEnumerable_Index

 * `IEnumerable<(int Index, TSource Item)> Index<TSource>(IEnumerable<TSource>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.index?view=net-10.0#system-linq-enumerable-index-1(system-collections-generic-ienumerable((-0))))


#### IEnumerable_LastOrDefault

 * `TSource LastOrDefault<TSource>(IEnumerable<TSource>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault?view=net-10.0#system-linq-enumerable-lastordefault-1(system-collections-generic-ienumerable((-0))-0))
 * `TSource LastOrDefault<TSource>(IEnumerable<TSource>, Func<TSource, bool>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault?view=net-10.0#system-linq-enumerable-lastordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))


#### IEnumerable_Max

 * `TSource? Max<TSource>(IEnumerable<TSource>, IComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.max?view=net-10.0#system-linq-enumerable-max-1(system-collections-generic-ienumerable((-0))-system-collections-generic-icomparer((-0))))


#### IEnumerable_MaxBy

 * `TSource? MaxBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby?view=net-10.0#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `TSource? MaxBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby?view=net-10.0#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1))))


#### IEnumerable_Min

 * `TSource? Min<TSource>(IEnumerable<TSource>, IComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.min?view=net-10.0#system-linq-enumerable-min-1(system-collections-generic-ienumerable((-0))-system-collections-generic-icomparer((-0))))


#### IEnumerable_MinBy

 * `TSource? MinBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.minby?view=net-10.0#system-linq-enumerable-minby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `TSource? MinBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.minby?view=net-10.0#system-linq-enumerable-minby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1))))


#### IEnumerable_SingleOrDefault

 * `TSource SingleOrDefault<TSource>(IEnumerable<TSource>, Func<TSource, bool>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.singleordefault?view=net-10.0#system-linq-enumerable-singleordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource SingleOrDefault<TSource>(IEnumerable<TSource>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.singleordefault?view=net-10.0#system-linq-enumerable-singleordefault-1(system-collections-generic-ienumerable((-0))-0))


#### IEnumerable_SkipLast

 * `IEnumerable<TSource> SkipLast<TSource>(IEnumerable<TSource>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.skiplast?view=net-10.0)


#### IEnumerable_Take

 * `IEnumerable<TSource> Take<TSource>(IEnumerable<TSource>, Range)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.take?view=net-10.0#system-linq-enumerable-take-1(system-collections-generic-ienumerable((-0))-system-range))


#### IEnumerable_TakeLast

 * `IEnumerable<TSource> TakeLast<TSource>(IEnumerable<TSource>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.takelast?view=net-10.0)


#### IEnumerable_ThrowHelper



#### IEnumerable_ToHashSet

 * `HashSet<TSource> ToHashSet<TSource>(IEnumerable<TSource>, IEqualityComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tohashset?view=net-10.0#system-linq-enumerable-tohashset-1(system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))


#### IEnumerable_TryGetNonEnumeratedCount

 * `bool TryGetNonEnumeratedCount<TSource>(IEnumerable<TSource>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.trygetnonenumeratedcount?view=net-10.0)


#### IEnumerable_Zip

 * `IEnumerable<(TFirst First, TSecond Second, TThird Third)> Zip<TFirst, TSecond, TThird>(IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip?view=net-10.0#system-linq-enumerable-zip-3(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-collections-generic-ienumerable((-2))))
 * `IEnumerable<(TFirst First, TSecond Second)> Zip<TFirst, TSecond>(IEnumerable<TFirst>, IEnumerable<TSecond>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip?view=net-10.0#system-linq-enumerable-zip-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))))


#### IReadOnlyDictionary

 * `TValue GetValueOrDefault<TKey, TValue>(IReadOnlyDictionary<TKey, TValue>, TKey, TValue) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault?view=net-10.0#system-collections-generic-collectionextensions-getvalueordefault-2(system-collections-generic-ireadonlydictionary((-0-1))-0-1))


#### KeyValuePair

 * `void Deconstruct<TKey, TValue>(KeyValuePair<TKey, TValue>, TKey, TValue)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2.deconstruct?view=net-10.0)


#### List

 * `ReadOnlyCollection<T> AsReadOnly<T>(IList<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly?view=net-10.0#system-collections-generic-collectionextensions-asreadonly-1(system-collections-generic-ilist((-0))))
 * `void AddRange<T>(List<T>, ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.addrange?view=net-10.0)
 * `void InsertRange<T>(List<T>, int, ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.insertrange?view=net-10.0)
 * `void CopyTo<T>(List<T>, Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.copyto?view=net-10.0)
 * `void EnsureCapacity<T>(List<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.ensurecapacity?view=net-10.0#system-collections-generic-list-1-ensurecapacity(system-int32))
 * `void TrimExcess<T>(List<T>)`


#### Memory

 * `SpanLineEnumerator EnumerateLines(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines?view=net-10.0#system-memoryextensions-enumeratelines(system-readonlyspan((system-char))))
 * `SpanLineEnumerator EnumerateLines(Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines?view=net-10.0#system-memoryextensions-enumeratelines(system-span((system-char))))
 * `Span<char> TrimStart(Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.trimstart?view=net-10.0#system-memoryextensions-trimstart(system-span((system-char))))
 * `Span<char> TrimEnd(Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.trimend?view=net-10.0#system-memoryextensions-trimend(system-span((system-char))))
 * `bool SequenceEqual(ReadOnlySpan<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal?view=net-10.0#system-memoryextensions-sequenceequal-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `bool SequenceEqual(Span<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal?view=net-10.0#system-memoryextensions-sequenceequal-1(system-span((-0))-system-readonlyspan((-0))))


#### Memory_CommonPrefixLength

 * `int CommonPrefixLength<T>(Span<T>, ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-10.0#system-memoryextensions-commonprefixlength-1(system-span((-0))-system-readonlyspan((-0))))
 * `int CommonPrefixLength<T>(Span<T>, ReadOnlySpan<T>, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-10.0#system-memoryextensions-commonprefixlength-1(system-span((-0))-system-readonlyspan((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `int CommonPrefixLength<T>(ReadOnlySpan<T>, ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-10.0#system-memoryextensions-commonprefixlength-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `int CommonPrefixLength<T>(ReadOnlySpan<T>, ReadOnlySpan<T>, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-10.0#system-memoryextensions-commonprefixlength-1(system-span((-0))-system-readonlyspan((-0))-system-collections-generic-iequalitycomparer((-0))))


#### Memory_Contains

 * `bool Contains<T>(ReadOnlySpan<T>, T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-10.0#system-memoryextensions-contains-1(system-readonlyspan((-0))-0))
 * `bool Contains<T>(Span<T>, T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-10.0#system-memoryextensions-contains-1(system-span((-0))-0))
 * `bool Contains<T>(ReadOnlySpan<T>, T, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-10.0#system-memoryextensions-contains-1(system-readonlyspan((-0))-0-system-collections-generic-iequalitycomparer((-0))))


#### Memory_CountAny

 * `int CountAny<T>(ReadOnlySpan<T>, SearchValues<T>) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-10.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0))))
 * `int CountAny<T>(ReadOnlySpan<T>, ReadOnlySpan<T>) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-10.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `int CountAny<T>(ReadOnlySpan<T>, ReadOnlySpan<T>, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-10.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-readonlyspan((-0))-system-collections-generic-iequalitycomparer((-0))))


#### Memory_EndsWith

 * `bool EndsWith<T>(ReadOnlySpan<T>, T) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-10.0#system-memoryextensions-endswith-1(system-readonlyspan((-0))-0))
 * `bool EndsWith(ReadOnlySpan<char>, string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-10.0#system-memoryextensions-endswith-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `bool EndsWith(Span<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-10.0#system-memoryextensions-endswith-1(system-span((-0))-system-readonlyspan((-0))))


#### Memory_IndexOf

 * `int IndexOf<T>(ReadOnlySpan<T>, T, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.indexof?view=net-10.0#system-memoryextensions-indexof-1(system-readonlyspan((-0))-0-system-collections-generic-iequalitycomparer((-0))))


#### Memory_IndexOfAny

 * `int IndexOfAny<T>(ReadOnlySpan<T>, ReadOnlySpan<T>, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-10.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0))))


#### Memory_SpanSplit

 * `SpanSplitEnumerator<T> Split<T>(ReadOnlySpan<T>, T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split?view=net-10.0#system-memoryextensions-split-1(system-readonlyspan((-0))-0))
 * `SpanSplitEnumerator<T> Split<T>(ReadOnlySpan<T>, ReadOnlySpan<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split?view=net-10.0#system-memoryextensions-split-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `SpanSplitEnumerator<T> SplitAny<T>(ReadOnlySpan<T>, ReadOnlySpan<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany?view=net-10.0#system-memoryextensions-splitany-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `SpanSplitEnumerator<T> SplitAny<T>(ReadOnlySpan<T>, SearchValues<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany?view=net-10.0#system-memoryextensions-splitany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0))))


#### Memory_SpanSplitEnumerator



#### Memory_SpanSplitEnumeratorMode



#### Memory_StartsWith

 * `bool StartsWith<T>(ReadOnlySpan<T>, T) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-10.0#system-memoryextensions-endswith-1(system-readonlyspan((-0))-0))
 * `bool StartsWith(ReadOnlySpan<char>, string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith?view=net-10.0#system-memoryextensions-startswith-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `bool StartsWith(Span<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith?view=net-10.0#system-memoryextensions-startswith-1(system-span((-0))-system-readonlyspan((-0))))


#### MethodInfo

 * `T CreateDelegate<T>(MethodInfo) where T : Delegate` [reference](https://learn.microsoft.com/en-us/dotnet/api/System.Reflection.MethodInfo.CreateDelegate?view=net-10.0#system-reflection-methodinfo-createdelegate-1)
 * `T CreateDelegate<T>(MethodInfo, object?) where T : Delegate` [reference](https://learn.microsoft.com/en-us/dotnet/api/System.Reflection.MethodInfo.CreateDelegate?view=net-10.0#system-reflection-methodinfo-createdelegate-1(system-object))


#### MicroNanosecond

 * `int Nanoseconds(TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.nanoseconds?view=net-10.0)
 * `int Nanosecond(DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.nanosecond?view=net-10.0)
 * `int Nanosecond(DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.nanosecond?view=net-10.0)
 * `int Microseconds(TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microseconds?view=net-10.0)
 * `int Microsecond(DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.microsecond?view=net-10.0)
 * `int Microsecond(DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.microsecond?view=net-10.0)


#### MicroNanosecondAdd

 * `DateTime AddMicroseconds(DateTime, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.addmicroseconds?view=net-10.0)
 * `DateTimeOffset AddMicroseconds(DateTimeOffset, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.addmicroseconds?view=net-10.0)


#### OrderedDictionary

 * `bool TryAdd<TKey, TValue>(OrderedDictionary<TKey, TValue>, TKey, TValue, int) where TKey : notnull` [reference](https://github.com/dotnet/core/blob/main/release-notes/10.0/preview/preview1/libraries.md#additional-tryadd-and-trygetvalue-overloads-for-ordereddictionarytkey-tvalue?view=net-10.0)
 * `bool TryGetValue<TKey, TValue>(OrderedDictionary<TKey, TValue>, TKey, TValue, int) where TKey : notnull` [reference](https://github.com/dotnet/core/blob/main/release-notes/10.0/preview/preview1/libraries.md#additional-tryadd-and-trygetvalue-overloads-for-ordereddictionarytkey-tvalue?view=net-10.0)


#### Process

 * `Task WaitForExitAsync(Process, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexitasync?view=net-10.0)


#### Queue

 * `void EnsureCapacity<T>(Queue<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.ensurecapacity?view=net-10.0#system-collections-generic-queue-1-ensurecapacity(system-int32))
 * `void TrimExcess<T>(Queue<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.trimexcess?view=net-10.0#system-collections-generic-queue-1-trimexcess(system-int32))


#### Random

 * `void GetItems<T>(Random, ReadOnlySpan<T>, Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems?view=net-10.0#system-random-getitems-1(system-readonlyspan((-0))-system-span((-0))))
 * `T[] GetItems<T>(Random, ReadOnlySpan<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems?view=net-10.0#system-random-getitems-1(system-readonlyspan((-0))-system-int32))
 * `T[] GetItems<T>(Random, T[], int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems?view=net-10.0#system-random-getitems-1(-0()-system-int32))
 * `void NextBytes(Random, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes?view=net-10.0#system-random-nextbytes(system-span((system-byte))))
 * `void Shuffle<T>(Random, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes?view=net-10.0#system-random-nextbytes(system-span((system-byte))))
 * `void Shuffle<T>(Random, Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes?view=net-10.0#system-random-nextbytes(system-span((system-byte))))


#### Set

 * `ReadOnlySet<T> AsReadOnly<T>(ISet<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly?view=net-10.0#system-collections-generic-collectionextensions-asreadonly-1(system-collections-generic-iset((-0))))


#### SortedList

 * `TKey GetKeyAtIndex<TKey, TValue>(SortedList<TKey, TValue>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getkeyatindex?view=net-10.0)
 * `TValue GetValueAtIndex<TKey, TValue>(SortedList<TKey, TValue>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getvalueatindex?view=net-10.0)


#### Stack

 * `void EnsureCapacity<T>(Stack<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.ensurecapacity?view=net-10.0)
 * `void TrimExcess<T>(Stack<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trimexcess?view=net-10.0#system-collections-generic-stack-1-trimexcess(system-int32))
 * `bool TryPeek<T>(Stack<T>, T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trypeek?view=net-10.0)
 * `bool TryPop<T>(Stack<T>, T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trypop?view=net-10.0)


#### Stream

 * `ValueTask<int> ReadAsync(Stream, Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readasync?view=net-10.0#system-io-stream-readasync(system-memory((system-byte))-system-threading-cancellationtoken))
 * `ValueTask WriteAsync(Stream, ReadOnlyMemory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.writeasync?view=net-10.0#system-io-stream-writeasync(system-readonlymemory((system-byte))-system-threading-cancellationtoken))
 * `Task CopyToAsync(Stream, Stream, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.copytoasync?view=net-10.0#system-io-stream-copytoasync(system-io-stream-system-threading-cancellationtoken))


#### Stream_DisposeAsync

 * `ValueTask DisposeAsync(Stream)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.disposeasync?view=net-10.0)


#### String

 * `void CopyTo(string, Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.copyto?view=net-10.0)
 * `bool TryCopyTo(string, Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.trycopyto?view=net-10.0)
 * `bool Contains(string, char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-10.0#system-string-contains(system-char))
 * `string ReplaceLineEndings(string, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.replacelineendings?view=net-10.0#system-string-replacelineendings(system-string))
 * `string ReplaceLineEndings(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.replacelineendings?view=net-10.0#system-string-replacelineendings)
 * `int GetHashCode(string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-10.0#system-string-gethashcode(system-stringcomparison))
 * `bool Contains(string, char, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-10.0#system-string-contains(system-char-system-stringcomparison))
 * `int IndexOf(string, char, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.indexof?view=net-10.0#system-string-indexof(system-char-system-stringcomparison))
 * `bool Contains(string, string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-10.0#system-string-contains(system-string-system-stringcomparison))
 * `bool StartsWith(string, char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.startswith?view=net-10.0#system-string-startswith(system-char))
 * `bool EndsWith(string, char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.endswith?view=net-10.0#system-string-endswith(system-char))
 * `string[] Split(string, char, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-10.0#system-string-split(system-char-system-stringsplitoptions))
 * `string[] Split(string, char, int, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-10.0#system-string-split(system-char-system-int32-system-stringsplitoptions))


#### StringBuilder

 * `bool Equals(StringBuilder, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.equals?view=net-10.0#system-text-stringbuilder-equals(system-readonlyspan((system-char))))


#### StringBuilder_Append

 * `StringBuilder Append(StringBuilder, StringBuilder?, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-10.0#system-text-stringbuilder-append(system-text-stringbuilder-system-int32-system-int32))
 * `StringBuilder Append(StringBuilder, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-10.0#system-text-stringbuilder-append(system-readonlyspan((system-char))))


#### StringBuilder_AppendJoin

 * `StringBuilder AppendJoin<T>(StringBuilder, char, IEnumerable<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin<T>(StringBuilder, string?, IEnumerable<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin(StringBuilder, string?, string?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin(system-string-system-string()))
 * `StringBuilder AppendJoin(StringBuilder, string?, object?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin(system-string-system-object()))
 * `StringBuilder AppendJoin(StringBuilder, char, string?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin(system-char-system-string()))
 * `StringBuilder AppendJoin(StringBuilder, char, object?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin(system-char-system-object()))
 * `StringBuilder AppendJoin<T>(StringBuilder, char, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin<T>(StringBuilder, string, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0))))


#### StringBuilder_CopyTo

 * `void CopyTo(StringBuilder, int, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.copyto?view=net-10.0#system-text-stringbuilder-copyto(system-int32-system-span((system-char))-system-int32))


#### StringBuilder_GetChunks

 * `ChunkEnumerator GetChunks(StringBuilder)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.getchunks?view=net-10.0)


#### StringBuilder_Insert

 * `StringBuilder Insert(StringBuilder, int, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.insert?view=net-10.0#system-text-stringbuilder-insert(system-int32-system-readonlyspan((system-char))))


#### StringBuilder_Replace

 * `StringBuilder Replace(StringBuilder, ReadOnlySpan<char>, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace?view=net-10.0#system-text-stringbuilder-replace(system-readonlyspan((system-char))-system-readonlyspan((system-char))))
 * `StringBuilder Replace(StringBuilder, ReadOnlySpan<char>, ReadOnlySpan<char>, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace?view=net-10.0#system-text-stringbuilder-replace(system-char-system-char-system-int32-system-int32))


#### StringNormalization

 * `int GetNormalizedLength(ReadOnlySpan<char>, NormalizationForm)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.getnormalizedlength?view=net-10.0#system-stringnormalizationextensions-getnormalizedlength(system-readonlyspan((system-char))-system-text-normalizationform))
 * `bool IsNormalized(ReadOnlySpan<char>, NormalizationForm)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.isnormalized?view=net-10.0#system-stringnormalizationextensions-isnormalized(system-readonlyspan((system-char))-system-text-normalizationform))
 * `bool TryNormalize(ReadOnlySpan<char>, Span<char>, int, NormalizationForm)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.trynormalize?view=net-10.0#system-stringnormalizationextensions-trynormalize(system-readonlyspan((system-char))-system-span((system-char))-system-int32@-system-text-normalizationform))


#### Task

 * `Task WaitAsync(Task, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-10.0#system-threading-tasks-task-waitasync(system-threading-cancellationtoken))
 * `Task WaitAsync(Task, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-10.0#system-threading-tasks-task-waitasync(system-timespan))
 * `Task WaitAsync(Task, TimeSpan, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-10.0#system-threading-tasks-task-waitasync(system-timespan-system-threading-cancellationtoken))
 * `Task<TResult> WaitAsync<TResult>(Task<TResult>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-10.0#system-threading-tasks-task-waitasync(system-threading-cancellationtoken))
 * `Task<TResult> WaitAsync<TResult>(Task<TResult>, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync?view=net-10.0#system-threading-tasks-task-1-waitasync(system-timespan))
 * `Task<TResult> WaitAsync<TResult>(Task<TResult>, TimeSpan, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync?view=net-10.0#system-threading-tasks-task-1-waitasync(system-timespan-system-threading-cancellationtoken))


#### TaskCompletionSource

 * `void SetCanceled<T>(TaskCompletionSource<T>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1.setcanceled?view=net-10.0#system-threading-tasks-taskcompletionsource-1-setcanceled(system-threading-cancellationtoken))


#### TextReader

 * `ValueTask<int> ReadAsync(TextReader, Memory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readasync?view=net-10.0#system-io-textreader-readasync(system-memory((system-char))-system-threading-cancellationtoken))
 * `Task<string> ReadToEndAsync(TextReader, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync?view=net-10.0#system-io-textreader-readtoendasync(system-threading-cancellationtoken))
 * `Task<string> ReadLineAsync(TextReader, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync?view=net-10.0#system-io-textreader-readlineasync(system-threading-cancellationtoken))


#### TextWriter

 * `Task FlushAsync(TextWriter, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.flushasync?view=net-10.0#system-io-textwriter-flushasync(system-threading-cancellationtoken))
 * `void Write(TextWriter, StringBuilder?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write?view=net-10.0#system-io-textwriter-write(system-text-stringbuilder))
 * `Task WriteAsync(TextWriter, StringBuilder?, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-10.0#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `ValueTask WriteAsync(TextWriter, ReadOnlyMemory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-10.0#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `ValueTask WriteLineAsync(TextWriter, ReadOnlyMemory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync?view=net-10.0#system-io-textwriter-writelineasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `void Write(TextWriter, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write?view=net-10.0#system-io-textwriter-write(system-readonlyspan((system-char))))
 * `void WriteLine(TextWriter, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeline?view=net-10.0#system-io-textwriter-writeline(system-readonlyspan((system-char))))


#### TimeOnly

 * `void Deconstruct(TimeOnly, int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-10.0#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@-system-int32@))
 * `void Deconstruct(TimeOnly, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-10.0#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@))
 * `void Deconstruct(TimeOnly, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-10.0#system-timeonly-deconstruct(system-int32@-system-int32@))
 * `void Deconstruct(TimeOnly, int, int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-10.0#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@-system-int32@-system-int32@))


#### Timespan

 * `bool TryFormat(TimeSpan, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.tryformat?view=net-10.0#system-timespan-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(TimeSpan, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.tryformat?view=net-10.0#system-timespan-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### TryFormatToByteSpan

 * `bool TryFormat(sbyte, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryformat?view=net-10.0#system-sbyte-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(byte, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryformat?view=net-10.0#system-byte-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(short, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryformat?view=net-10.0#system-int16-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(ushort, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryformat?view=net-10.0#system-uint16-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(int, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryformat?view=net-10.0#system-int32-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(uint, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryformat?view=net-10.0#system-uint32-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(long, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryformat?view=net-10.0#system-int64-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(ulong, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryformat?view=net-10.0#system-uint64-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(float, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.single.tryformat?view=net-10.0#system-single-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(double, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryformat?view=net-10.0#system-double-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(decimal, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.decimal.tryformat?view=net-10.0#system-decimal-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(DateTimeOffset, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryformat?view=net-10.0#system-datetimeoffset-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(DateTime, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryformat?view=net-10.0#system-datetime-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(DateOnly, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.tryformat?view=net-10.0#system-dateonly-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(TimeOnly, Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.tryformat?view=net-10.0#system-timeonly-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### TryFormatToCharSpan

 * `bool TryFormat(Guid, Span<char>, int, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryformat?view=net-10.0#system-guid-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))))
 * `bool TryFormat(sbyte, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryformat?view=net-10.0#system-sbyte-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(byte, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryformat?view=net-10.0#system-byte-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(short, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryformat?view=net-10.0#system-int16-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(ushort, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryformat?view=net-10.0#system-uint16-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(int, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryformat?view=net-10.0#system-int32-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(uint, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryformat?view=net-10.0#system-uint32-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(long, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryformat?view=net-10.0#system-int64-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(ulong, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryformat?view=net-10.0#system-uint64-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(float, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.single.tryformat?view=net-10.0#system-single-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(double, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryformat?view=net-10.0#system-double-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(decimal, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.decimal.tryformat?view=net-10.0#system-decimal-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(bool, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.boolean.tryformat?view=net-10.0)
 * `bool TryFormat(DateTimeOffset, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryformat?view=net-10.0#system-datetimeoffset-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(DateTime, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryformat?view=net-10.0#system-datetime-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(DateOnly, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.tryformat?view=net-10.0#system-dateonly-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(TimeOnly, Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.tryformat?view=net-10.0#system-timeonly-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### Type

 * `bool HasSameMetadataDefinitionAs(MemberInfo, MemberInfo)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo.hassamemetadatadefinitionas?view=net-10.0)
 * `MethodInfo? GetMethod(Type, string, int, BindingFlags, Type[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.getmethod?view=net-10.0#system-type-getmethod(system-string-system-int32-system-reflection-bindingflags-system-type()))
 * `bool IsGenericMethodParameter(Type)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.isgenericmethodparameter?view=net-10.0)
 * `bool IsAssignableTo<T>(Type)`
 * `bool IsAssignableFrom<T>(Type)`
 * `bool IsAssignableTo(Type, Type?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.isassignableto?view=net-10.0)


#### XDocument

 * `Task SaveAsync(XDocument, XmlWriter, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync?view=net-10.0#system-xml-linq-xdocument-saveasync(system-xml-xmlwriter-system-threading-cancellationtoken))
 * `Task SaveAsync(XDocument, Stream, SaveOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync?view=net-10.0#system-xml-linq-xdocument-saveasync(system-io-stream-system-xml-linq-saveoptions-system-threading-cancellationtoken))
 * `Task SaveAsync(XDocument, TextWriter, SaveOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync?view=net-10.0#system-xml-linq-xdocument-saveasync(system-io-textwriter-system-xml-linq-saveoptions-system-threading-cancellationtoken))


#### ZipArchive

 * `Task<ZipArchiveEntry> CreateEntryFromFileAsync(string, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.createentryfromfileasync?view=net-10.0)
 * `Task<ZipArchiveEntry> CreateEntryFromFileAsync(string, string, CompressionLevel, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.createentryfromfileasync?view=net-10.0)
 * `Task ExtractToDirectoryAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttodirectory?view=net-10.0#system-io-compression-zipfileextensions-extracttodirectory(system-io-compression-ziparchive-system-string))
 * `Task ExtractToDirectoryAsync(string, bool, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttodirectoryasync?view=net-10.0#system-io-compression-zipfileextensions-extracttodirectoryasync(system-io-compression-ziparchive-system-string-system-boolean-system-threading-cancellationtoken))
 * `void ExtractToDirectory(string, bool)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttodirectory?view=net-10.0#system-io-compression-zipfileextensions-extracttodirectory(system-io-compression-ziparchive-system-string-system-boolean))


#### ZipArchiveEntry

 * `Task<Stream> OpenAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.ziparchiveentry.openasync?view=net-10.0)
 * `Task ExtractToFileAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttofileasync?view=net-10.0#system-io-compression-zipfileextensions-extracttofileasync(system-io-compression-ziparchiveentry-system-string-system-threading-cancellationtoken))
 * `Task ExtractToFileAsync(string, bool, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttofileasync?view=net-10.0#system-io-compression-zipfileextensions-extracttofileasync(system-io-compression-ziparchiveentry-system-string-system-boolean-system-threading-cancellationtoken))


#### Guard

 * `void DirectoryExists(string)`
 * `void FileExists(string)`
 * `void NotEmpty(string?)`
 * `void NotEmpty<T>(Memory<T>?)`
 * `void NotEmpty<T>(Memory<T>)`
 * `void NotEmpty<T>(ReadOnlyMemory<T>?)`
 * `void NotEmpty<T>(ReadOnlyMemory<T>)`
 * `void NotEmpty<T>(ReadOnlySpan<T>)`
 * `void NotEmpty<T>(Span<T>)`
 * `void NotEmpty<T>(T?) where T : IEnumerable`
 * `string NotNull(string?)`
 * `T NotNull<T>(T?) where T : class`
 * `Memory<char> NotNullOrEmpty(Memory<char>?)`
 * `ReadOnlyMemory<char> NotNullOrEmpty(ReadOnlyMemory<char>?)`
 * `string NotNullOrEmpty(string?)`
 * `T NotNullOrEmpty<T>(T?) where T : IEnumerable`
 * `Memory<char> NotNullOrWhiteSpace(Memory<char>?)`
 * `ReadOnlyMemory<char> NotNullOrWhiteSpace(ReadOnlyMemory<char>?)`
 * `string NotNullOrWhiteSpace(string?)`
 * `void NotWhiteSpace(Memory<char>?)`
 * `void NotWhiteSpace(ReadOnlyMemory<char>?)`
 * `void NotWhiteSpace(ReadOnlySpan<char>)`
 * `void NotWhiteSpace(Span<char>)`
 * `void NotWhiteSpace(string?)`


#### Lock

 * `void Enter()`
 * `Scope EnterScope()`
 * `void Exit()`
 * `bool TryEnter()`
 * `bool TryEnter(int)`
 * `bool TryEnter(TimeSpan)`


#### KeyValuePair

 * `KeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey, TValue)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair.create?view=net-10.0)


#### TaskCompletionSource

#### UnreachableException

