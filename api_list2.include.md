### Extension methods

#### CancellationToken

 * `CancellationTokenRegistration UnsafeRegister(CancellationToken, Action<object?>, object?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister#system-threading-cancellationtoken-unsaferegister(system-action((system-object))-system-object)")
 * `CancellationTokenRegistration Register(CancellationToken, Action<object?, CancellationToken>, object?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.register#system-threading-cancellationtoken-register(system-action((system-object-system-threading-cancellationtoken))-system-object)")
 * `CancellationTokenRegistration UnsafeRegister(CancellationToken, Action<object?, CancellationToken>, object?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister#system-threading-cancellationtoken-unsaferegister(system-action((system-object-system-threading-cancellationtoken))-system-object)")


#### CancellationTokenSource

 * `Task CancelAsync(CancellationTokenSource)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.cancelasync")


#### DateTime

 * `int Nanosecond(DateTime)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.nanosecond")
 * `int Microsecond(DateTime)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.microsecond")
 * `DateTime AddMicroseconds(DateTime, double)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetime.addmicroseconds")


#### DateTimeOffset

 * `int Nanosecond(DateTimeOffset)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.nanosecond")
 * `int Microsecond(DateTimeOffset)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.microsecond")
 * `DateTimeOffset AddMicroseconds(DateTimeOffset, double)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.addmicroseconds")


#### IDictionary<TKey, TValue>

 * `ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(IDictionary<TKey, TValue>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly#system-collections-generic-collectionextensions-asreadonly-2(system-collections-generic-idictionary((-0-1)))")


#### IEnumerable<TSource>

 * `IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(IEnumerable<TSource>, Func<TSource, TKey>, TAccumulate, Func<TAccumulate, TSource, TAccumulate>, IEqualityComparer<TKey>?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-func((-1-2))-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1)))")
 * `IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(IEnumerable<TSource>, Func<TSource, TKey>, Func<TKey, TAccumulate>, Func<TAccumulate, TSource, TAccumulate>, IEqualityComparer<TKey>?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-2-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1)))")
 * `IEnumerable<TSource[]> Chunk<TSource>(IEnumerable<TSource>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.chunk")
 * `IEnumerable<KeyValuePair<TKey, int>> CountBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.countby")
 * `IEnumerable<TSource> DistinctBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1)))")
 * `IEnumerable<TSource> DistinctBy<TSource, TKey>(IEnumerable<TSource>, Func<TSource, TKey>, IEqualityComparer<TKey>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1)))")
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, TSource)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0)))")
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, TSource, IEqualityComparer<TSource>?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0)))")
 * `IEnumerable<TSource> Except<TSource>(IEnumerable<TSource>, IEqualityComparer<TSource>, TSource[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0)))")
 * `IEnumerable<TSource> ExceptBy<TSource, TKey>(IEnumerable<TSource>, IEnumerable<TKey>, Func<TSource, TKey>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1)))")
 * `IEnumerable<TSource> ExceptBy<TSource, TKey>(IEnumerable<TSource>, IEnumerable<TKey>, Func<TSource, TKey>, IEqualityComparer<TKey>?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1)))")
 * `TSource FirstOrDefault<TSource>(IEnumerable<TSource>, Func<TSource, bool>, TSource)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0)")
 * `TSource FirstOrDefault<TSource>(IEnumerable<TSource>, TSource)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-0)")
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
 * `bool TryGetNonEnumeratedCount<TSource>(IEnumerable<TSource>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.trygetnonenumeratedcount")


#### IList<T>

 * `ReadOnlyCollection<T> AsReadOnly<T>(IList<T>)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly#system-collections-generic-collectionextensions-asreadonly-1(system-collections-generic-ilist((-0)))")


#### Process

 * `Task WaitForExitAsync(Process, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexitasync")


#### Random

 * `void Shuffle<T>(Random, T[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes#system-random-nextbytes(system-span((system-byte)))")


#### SortedList<TKey, TValue>

 * `TKey GetKeyAtIndex<TKey, TValue>(SortedList<TKey, TValue>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getkeyatindex")
 * `TValue GetValueAtIndex<TKey, TValue>(SortedList<TKey, TValue>, int)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getvalueatindex")


#### TaskCompletionSource<T>

 * `void SetCanceled<T>(TaskCompletionSource<T>, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1.setcanceled#system-threading-tasks-taskcompletionsource-1-setcanceled(system-threading-cancellationtoken)")


#### TextReader

 * `Task<string> ReadToEndAsync(TextReader, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync#system-io-textreader-readtoendasync(system-threading-cancellationtoken)")
 * `Task<string> ReadLineAsync(TextReader, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync#system-io-textreader-readlineasync(system-threading-cancellationtoken)")


#### TextWriter

 * `Task FlushAsync(TextWriter, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.flushasync#system-io-textwriter-flushasync(system-threading-cancellationtoken)")
 * `void Write(TextWriter, StringBuilder?)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write#system-io-textwriter-write(system-text-stringbuilder)")
 * `Task WriteAsync(TextWriter, StringBuilder?, CancellationToken)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken)")


#### TimeSpan

 * `int Nanoseconds(TimeSpan)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.timespan.nanoseconds")
 * `int Microseconds(TimeSpan)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microseconds")


#### Type

 * `MethodInfo? GetMethod(Type, string, int, BindingFlags, Type[])` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.type.getmethod#system-type-getmethod(system-string-system-int32-system-reflection-bindingflags-system-type())")
 * `bool IsGenericMethodParameter(Type)` [reference]("https://learn.microsoft.com/en-us/dotnet/api/system.type.isgenericmethodparameter")
 * `bool IsAssignableTo<T>(Type)`
 * `bool IsAssignableFrom<T>(Type)`


