### Extension methods

#### ArraySegment<T>

 * `void CopyTo<T>(ArraySegment<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.arraysegment-1.copyto?view=net-10.0#system-arraysegment-1-copyto(-0()))
 * `void CopyTo<T>(T[], int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.arraysegment-1.copyto?view=net-10.0#system-arraysegment-1-copyto(-0()-system-int32))
 * `void CopyTo<T>(T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.arraysegment-1.copyto?view=net-10.0#system-arraysegment-1-copyto(-0()))
 * `ArraySegmentEnumerator<T> GetEnumerator<T>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.arraysegment-1.getenumerator?view=net-10.0)


#### Boolean

 * `bool TryFormat(Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.boolean.tryformat?view=net-10.0)


#### Byte

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryformat?view=net-10.0#system-byte-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryformat?view=net-10.0#system-byte-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<byte>, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-10.0#system-byte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-byte@))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-10.0#system-byte-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-byte@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-10.0#system-byte-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-byte@))
 * `bool TryParse(ReadOnlySpan<char>, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-10.0#system-byte-tryparse(system-readonlyspan((system-char))-system-byte@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-10.0#system-byte-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-byte@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-10.0#system-byte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-byte@))
 * `bool TryParse(string?, IFormatProvider?, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.byte.tryparse?view=net-10.0#system-byte-tryparse(system-string-system-iformatprovider-system-byte@))


#### CancellationToken

 * `CancellationTokenRegistration Register(Action<object?, CancellationToken>, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.register?view=net-10.0#system-threading-cancellationtoken-register(system-action((system-object-system-threading-cancellationtoken))-system-object))
 * `CancellationTokenRegistration UnsafeRegister(Action<object?, CancellationToken>, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister?view=net-10.0#system-threading-cancellationtoken-unsaferegister(system-action((system-object-system-threading-cancellationtoken))-system-object))
 * `CancellationTokenRegistration UnsafeRegister(Action<object?>, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken.unsaferegister?view=net-10.0#system-threading-cancellationtoken-unsaferegister(system-action((system-object))-system-object))


#### CancellationTokenSource

 * `Task CancelAsync()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource.cancelasync?view=net-10.0)


#### ConcurrentBag<T>

 * `void Clear<T>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentbag-1.clear?view=net-10.0)


#### ConcurrentDictionary<TKey, TValue>

 * `TValue GetOrAdd<TKey, TValue, TArg>(TKey, Func<TKey, TArg, TValue>, TArg) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentdictionary-2.getoradd?view=net-10.0#system-collections-concurrent-concurrentdictionary-2-getoradd-1(-0-system-func((-0-0-1))-0))


#### ConcurrentQueue<T>

 * `void Clear<T>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.concurrent.concurrentqueue-1.clear?view=net-10.0)


#### Convert

 * `byte[] FromHexString(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.fromhexstring?view=net-10.0#system-convert-fromhexstring(system-readonlyspan((system-char))))
 * `byte[] FromHexString(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.fromhexstring?view=net-10.0#system-convert-fromhexstring(system-string))
 * `string ToHexString(byte[], int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstring?view=net-10.0#system-convert-tohexstring(system-byte()-system-int32-system-int32))
 * `string ToHexString(byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstring?view=net-10.0#system-convert-tohexstring(system-byte()))
 * `string ToHexString(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstring?view=net-10.0#system-convert-tohexstring(system-readonlyspan((system-byte))))
 * `string ToHexStringLower(byte[], int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstringlower?view=net-10.0#system-convert-tohexstringlower(system-byte()-system-int32-system-int32))
 * `string ToHexStringLower(byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstringlower?view=net-10.0#system-convert-tohexstringlower(system-byte()))
 * `string ToHexStringLower(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.tohexstringlower?view=net-10.0#system-convert-tohexstringlower(system-readonlyspan((system-byte))))
 * `bool TryToHexString(ReadOnlySpan<byte>, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.trytohexstring?view=net-10.0)
 * `bool TryToHexStringLower(ReadOnlySpan<byte>, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.convert.trytohexstringlower?view=net-10.0)


#### DateOnly

 * `void Deconstruct(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.deconstruct?view=net-10.0)
 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.tryformat?view=net-10.0#system-dateonly-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.dateonly.tryformat?view=net-10.0#system-dateonly-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### DateTime

 * `DateTime AddMicroseconds(double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.addmicroseconds?view=net-10.0)
 * `void Deconstruct(DateOnly, TimeOnly)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.deconstruct?view=net-10.0#system-datetime-deconstruct(system-dateonly@-system-timeonly@))
 * `void Deconstruct(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.deconstruct?view=net-10.0#system-datetime-deconstruct(system-int32@-system-int32@-system-int32@))
 * `int Microsecond()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.microsecond?view=net-10.0)
 * `int Nanosecond()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.nanosecond?view=net-10.0)
 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryformat?view=net-10.0#system-datetime-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryformat?view=net-10.0#system-datetime-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<char>, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse?view=net-10.0#system-datetime-tryparse(system-readonlyspan((system-char))-system-datetime@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse?view=net-10.0#system-datetime-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-datetime@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse?view=net-10.0#system-datetime-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@))
 * `bool TryParse(string?, IFormatProvider?, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparse?view=net-10.0#system-datetime-tryparse(system-string-system-iformatprovider-system-datetime@))
 * `bool TryParseExact(ReadOnlySpan<char>, ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparseexact?view=net-10.0#system-datetime-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@))
 * `bool TryParseExact(ReadOnlySpan<char>, string, IFormatProvider?, DateTimeStyles, DateTime)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.tryparseexact?view=net-10.0#system-datetime-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetime@))


#### DateTimeOffset

 * `DateTimeOffset AddMicroseconds(double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.addmicroseconds?view=net-10.0)
 * `void Deconstruct(DateOnly, TimeOnly, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.deconstruct?view=net-10.0)
 * `int Microsecond()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.microsecond?view=net-10.0)
 * `int Nanosecond()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.nanosecond?view=net-10.0)
 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryformat?view=net-10.0#system-datetimeoffset-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryformat?view=net-10.0#system-datetimeoffset-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<char>, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse?view=net-10.0#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-datetimeoffset@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse?view=net-10.0#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-datetimeoffset@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse?view=net-10.0#system-datetimeoffset-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@))
 * `bool TryParse(string?, IFormatProvider?, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparse?view=net-10.0#system-datetimeoffset-tryparse(system-string-system-iformatprovider-system-datetimeoffset@))
 * `bool TryParseExact(ReadOnlySpan<char>, ReadOnlySpan<char>, IFormatProvider?, DateTimeStyles, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparseexact?view=net-10.0#system-datetimeoffset-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@))
 * `bool TryParseExact(ReadOnlySpan<char>, string, IFormatProvider?, DateTimeStyles, DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset.tryparseexact?view=net-10.0#system-datetimeoffset-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-iformatprovider-system-globalization-datetimestyles-system-datetimeoffset@))


#### Decimal

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.decimal.tryformat?view=net-10.0#system-decimal-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.decimal.tryformat?view=net-10.0#system-decimal-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### DefaultInterpolatedStringHandler

 * `void Clear()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.defaultinterpolatedstringhandler.clear?view=net-10.0)


#### Delegate

 * `InvocationListEnumerator<TDelegate> EnumerateInvocationList<TDelegate>(TDelegate?) where TDelegate : Delegate` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.delegate.enumerateinvocationlist?view=net-10.0)
 * `HasSingleTarget` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.delegate.hassingletarget?view=net-10.0)


#### Dictionary<TKey, TValue>

 * `void EnsureCapacity<TKey, TValue>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.ensurecapacity?view=net-10.0)
 * `void TrimExcess<TKey, TValue>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.trimexcess?view=net-10.0#system-collections-generic-dictionary-2-trimexcess(system-int32))
 * `void TrimExcess<TKey, TValue>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2.ensurecapacity?view=net-10.0)


#### DictionaryEntry

 * `void Deconstruct(object, object?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.dictionaryentry.deconstruct?view=net-10.0#system-collections-dictionaryentry-deconstruct(system-object@-system-object@))


#### Double

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryformat?view=net-10.0#system-double-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryformat?view=net-10.0#system-double-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<byte>, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-10.0#system-double-tryparse(system-readonlyspan((system-byte))-system-double@))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-10.0#system-double-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-double@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-10.0#system-double-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-double@))
 * `bool TryParse(ReadOnlySpan<char>, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-10.0#system-double-tryparse(system-readonlyspan((system-char))-system-double@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-10.0#system-double-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-double@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-10.0#system-double-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-double@))
 * `bool TryParse(string?, IFormatProvider?, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.double.tryparse?view=net-10.0#system-double-tryparse(system-string-system-iformatprovider-system-double@))


#### Encoding

 * `int GetByteCount(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytecount?view=net-10.0#system-text-encoding-getbytecount(system-readonlyspan((system-char))))
 * `int GetBytes(ReadOnlySpan<char>, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getbytes?view=net-10.0#system-text-encoding-getbytes(system-readonlyspan((system-char))-system-span((system-byte))))
 * `int GetCharCount(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getcharcount?view=net-10.0#system-text-encoding-getcharcount(system-readonlyspan((system-byte))))
 * `int GetChars(ReadOnlySpan<byte>, Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getchars?view=net-10.0#system-text-encoding-getchars(system-readonlyspan((system-byte))-system-span((system-char))))
 * `string GetString(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.getstring?view=net-10.0#system-text-encoding-getstring(system-readonlyspan((system-byte))))
 * `bool TryGetBytes(ReadOnlySpan<char>, Span<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.trygetbytes?view=net-10.0)
 * `bool TryGetChars(ReadOnlySpan<byte>, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.encoding.trygetchars?view=net-10.0)


#### Enum

 * `string[] GetNames<TEnum>() where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.getnames?view=net-10.0)
 * `TEnum[] GetValues<TEnum>() where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.getvalues?view=net-10.0)
 * `bool IsDefined<TEnum>(TEnum) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.isdefined?view=net-10.0#system-enum-isdefined-1(-0))
 * `TEnum Parse<TEnum>(ReadOnlySpan<char>, bool) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse?view=net-10.0#system-enum-parse-1(system-readonlyspan((system-char))-system-boolean))
 * `TEnum Parse<TEnum>(ReadOnlySpan<char>) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse?view=net-10.0#system-enum-parse-1(system-readonlyspan((system-char))))
 * `TEnum Parse<TEnum>(string, bool) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse?view=net-10.0#system-enum-parse-1(system-string-system-boolean))
 * `TEnum Parse<TEnum>(string) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.parse?view=net-10.0#system-enum-parse-1(system-string-system-boolean))
 * `bool TryFormat<TEnum>(TEnum, Span<char>, int, ReadOnlySpan<char>) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryformat?view=net-10.0)
 * `bool TryParse<TEnum>(ReadOnlySpan<char>, bool, TEnum) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryparse?view=net-10.0#system-enum-tryparse-1(system-readonlyspan((system-char))-system-boolean-0@))
 * `bool TryParse<TEnum>(ReadOnlySpan<char>, TEnum) where TEnum : struct, Enum` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.enum.tryparse?view=net-10.0#system-enum-tryparse-1(system-readonlyspan((system-char))-0@))


#### Environment

 * `ProcessId` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.environment.processid?view=net-10.0#system-environment-processid)


#### EventInfo

 * `NullabilityState GetNullability()`
 * `NullabilityInfo GetNullabilityInfo()`
 * `bool IsNullable()`


#### FieldInfo

 * `NullabilityState GetNullability()`
 * `NullabilityInfo GetNullabilityInfo()`
 * `bool IsNullable()`


#### File

 * `void AppendAllBytes(string, byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytes?view=net-10.0#system-io-file-appendallbytes(system-string-system-byte()))
 * `void AppendAllBytes(string, ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytes?view=net-10.0#system-io-file-appendallbytes(system-string-system-readonlyspan((system-byte))))
 * `Task AppendAllBytesAsync(string, byte[], CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytesasync?view=net-10.0#system-io-file-appendallbytesasync(system-string-system-byte()-system-threading-cancellationtoken))
 * `Task AppendAllBytesAsync(string, ReadOnlyMemory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytesasync?view=net-10.0#system-io-file-appendallbytesasync(system-string-system-readonlymemory((system-byte))-system-threading-cancellationtoken))
 * `Task AppendAllLinesAsync(string, IEnumerable<string>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalllinesasync?view=net-10.0#system-io-file-appendalllinesasync(system-string-system-collections-generic-ienumerable((system-string))-system-threading-cancellationtoken))
 * `Task AppendAllLinesAsync(string, IEnumerable<string>, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalllinesasync?view=net-10.0#system-io-file-appendalllinesasync(system-string-system-collections-generic-ienumerable((system-string))-system-text-encoding-system-threading-cancellationtoken))
 * `void AppendAllText(string, ReadOnlySpan<char>, Encoding)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltext?view=net-10.0#system-io-file-appendalltext(system-string-system-readonlyspan((system-char))-system-text-encoding))
 * `void AppendAllText(string, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltext?view=net-10.0#system-io-file-appendalltext(system-string-system-readonlyspan((system-char))))
 * `Task AppendAllTextAsync(string, ReadOnlyMemory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltextasync?view=net-10.0#system-io-file-appendalltextasync(system-string-system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `Task AppendAllTextAsync(string, ReadOnlyMemory<char>, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltextasync?view=net-10.0#system-io-file-appendalltextasync(system-string-system-readonlymemory((system-char))-system-text-encoding-system-threading-cancellationtoken))
 * `Task AppendAllTextAsync(string, string?, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltextasync?view=net-10.0#system-io-file-appendalltextasync(system-string-system-string-system-threading-cancellationtoken))
 * `Task AppendAllTextAsync(string, string?, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendalltextasync?view=net-10.0#system-io-file-appendalltextasync(system-string-system-string-system-text-encoding-system-threading-cancellationtoken))
 * `void Move(string, string, bool)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.move?system-io-file-move(system-string-system-string-system-boolean)?view=net-10.0)
 * `Task<byte[]> ReadAllBytesAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readallbytesasync?view=net-10.0)
 * `Task<string[]> ReadAllLinesAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalllinesasync?view=net-10.0#system-io-file-readalllinesasync(system-string-system-threading-cancellationtoken))
 * `Task<string[]> ReadAllLinesAsync(string, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalllinesasync?view=net-10.0#system-io-file-readalllinesasync(system-string-system-text-encoding-system-threading-cancellationtoken))
 * `Task<string> ReadAllTextAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalltextasync?view=net-10.0#system-io-file-readalltextasync(system-string-system-threading-cancellationtoken))
 * `Task<string> ReadAllTextAsync(string, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalltextasync?view=net-10.0#system-io-file-readalltextasync(system-string-system-text-encoding-system-threading-cancellationtoken))
 * `IAsyncEnumerable<string> ReadLinesAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readlinesasync?view=net-10.0#system-io-file-readalllinesasync(system-string-system-threading-cancellationtoken))
 * `IAsyncEnumerable<string> ReadLinesAsync(string, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readlinesasync?view=net-10.0#system-io-file-readalllinesasync(system-string-system-text-encoding-system-threading-cancellationtoken))
 * `Task WriteAllBytesAsync(string, byte[], CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writeallbytesasync?view=net-10.0#system-io-file-writeallbytesasync(system-string-system-byte()-system-threading-cancellationtoken))
 * `Task WriteAllBytesAsync(string, ReadOnlyMemory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendallbytesasync?view=net-10.0#system-io-file-appendallbytesasync(system-string-system-readonlymemory((system-byte))-system-threading-cancellationtoken))
 * `Task WriteAllLinesAsync(string, IEnumerable<string>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealllinesasync?view=net-10.0#system-io-file-writealllinesasync(system-string-system-collections-generic-ienumerable((system-string))-system-threading-cancellationtoken))
 * `Task WriteAllLinesAsync(string, IEnumerable<string>, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealllinesasync?view=net-10.0#system-io-file-writealllinesasync(system-string-system-collections-generic-ienumerable((system-string))-system-text-encoding-system-threading-cancellationtoken))
 * `void WriteAllText(string, ReadOnlySpan<char>, Encoding)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltext?view=net-10.0#system-io-file-writealltext(system-string-system-readonlyspan((system-char))-system-text-encoding))
 * `void WriteAllText(string, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltext?view=net-10.0#system-io-file-writealltext(system-string-system-readonlyspan((system-char))))
 * `Task WriteAllTextAsync(string, string?, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltextasync?view=net-10.0#system-io-file-writealltextasync(system-string-system-string-system-text-encoding-system-threading-cancellationtoken))
 * `Task WriteAllTextAsync(string, string?, Encoding, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltextasync?view=net-10.0#system-io-file-writealltextasync(system-string-system-string-system-text-encoding-system-threading-cancellationtoken))


#### FileUnixMode



#### Guid

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryformat?view=net-10.0#system-guid-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryformat?view=net-10.0#system-guid-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))))
 * `Guid CreateVersion7()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.createversion7?view=net-10.0#system-guid-createversion7)
 * `Guid CreateVersion7(DateTimeOffset)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.createversion7?view=net-10.0#system-guid-createversion7(system-datetimeoffset))
 * `bool TryParse(ReadOnlySpan<char>, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-10.0#system-guid-tryparse(system-readonlyspan((system-char))-system-guid@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-10.0#system-guid-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-guid@))
 * `bool TryParse(string?, IFormatProvider?, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparse?view=net-10.0#system-guid-tryparse(system-string-system-iformatprovider-system-guid@))
 * `bool TryParseExact(ReadOnlySpan<char>, ReadOnlySpan<char>, Guid)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparseexact?view=net-10.0#system-guid-tryparseexact(system-readonlyspan((system-char))-system-readonlyspan((system-char))-system-guid@))


#### HashSet<T>

 * `void EnsureCapacity<T>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.ensurecapacity?view=net-10.0#system-collections-generic-hashset-1-ensurecapacity(system-int32))
 * `void TrimExcess<T>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.trimexcess?view=net-10.0#system-collections-generic-hashset-1-trimexcess(system-int32))
 * `bool TryGetValue<T>(T, T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1.trygetvalue?view=net-10.0)


#### HttpClient

 * `Task<byte[]> GetByteArrayAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync?view=net-10.0#system-net-http-httpclient-getbytearrayasync(system-string-system-threading-cancellationtoken))
 * `Task<byte[]> GetByteArrayAsync(Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getbytearrayasync?view=net-10.0#system-net-http-httpclient-getbytearrayasync(system-uri-system-threading-cancellationtoken))
 * `Task<Stream> GetStreamAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync?view=net-10.0#system-net-http-httpclient-getstreamasync(system-string-system-threading-cancellationtoken))
 * `Task<Stream> GetStreamAsync(Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstreamasync?view=net-10.0#system-net-http-httpclient-getstreamasync(system-uri-system-threading-cancellationtoken))
 * `Task<string> GetStringAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync?view=net-10.0#system-net-http-httpclient-getstringasync(system-string-system-threading-cancellationtoken))
 * `Task<string> GetStringAsync(Uri, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.getstringasync?view=net-10.0#system-net-http-httpclient-getstringasync(system-uri-system-threading-cancellationtoken))


#### HttpContent

 * `Task<byte[]> ReadAsByteArrayAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasbytearrayasync?view=net-10.0#system-net-http-httpcontent-readasbytearrayasync(system-threading-cancellationtoken))
 * `Task<Stream> ReadAsStreamAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstreamasync?view=net-10.0#system-net-http-httpcontent-readasstreamasync(system-threading-cancellationtoken))
 * `Task<string> ReadAsStringAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpcontent.readasstringasync?view=net-10.0#system-net-http-httpcontent-readasstringasync(system-threading-cancellationtoken))


#### IDictionary<TKey, TValue>

 * `ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>() where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly?view=net-10.0#system-collections-generic-collectionextensions-asreadonly-2(system-collections-generic-idictionary((-0-1))))
 * `bool Remove<TKey, TValue>(TKey, TValue) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.remove?view=net-10.0)
 * `bool TryAdd<TKey, TValue>(TKey, TValue) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.tryadd?view=net-10.0)


#### IEnumerable<TFirst>

 * `IEnumerable<(TFirst First, TSecond Second, TThird Third)> Zip<TFirst, TSecond, TThird>(IEnumerable<TSecond>, IEnumerable<TThird>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip?view=net-10.0#system-linq-enumerable-zip-3(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-collections-generic-ienumerable((-2))))
 * `IEnumerable<(TFirst First, TSecond Second)> Zip<TFirst, TSecond>(IEnumerable<TSecond>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip?view=net-10.0#system-linq-enumerable-zip-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))))


#### IEnumerable<TSource>

 * `IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(Func<TSource, TKey>, Func<TKey, TAccumulate>, Func<TAccumulate, TSource, TAccumulate>, IEqualityComparer<TKey>?) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby?view=net-10.0#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-2-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1))))
 * `IEnumerable<KeyValuePair<TKey, TAccumulate>> AggregateBy<TSource, TKey, TAccumulate>(Func<TSource, TKey>, TAccumulate, Func<TAccumulate, TSource, TAccumulate>, IEqualityComparer<TKey>?) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.aggregateby?view=net-10.0#system-linq-enumerable-aggregateby-3(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-func((-1-2))-system-func((-2-0-2))-system-collections-generic-iequalitycomparer((-1))))
 * `IEnumerable<TSource> Append<TSource>(TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.append?view=net-10.0)
 * `IEnumerable<TSource[]> Chunk<TSource>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.chunk?view=net-10.0)
 * `IEnumerable<KeyValuePair<TKey, int>> CountBy<TSource, TKey>(Func<TSource, TKey>, IEqualityComparer<TKey>?) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.countby?view=net-10.0)
 * `IEnumerable<TSource> DistinctBy<TSource, TKey>(Func<TSource, TKey>, IEqualityComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby?view=net-10.0#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1))))
 * `IEnumerable<TSource> DistinctBy<TSource, TKey>(Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.distinctby?view=net-10.0#system-linq-enumerable-distinctby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `TSource ElementAt<TSource>(Index)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementat?view=net-10.0#system-linq-enumerable-elementat-1(system-collections-generic-ienumerable((-0))-system-index))
 * `TSource? ElementAtOrDefault<TSource>(Index)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.elementatordefault?view=net-10.0#system-linq-enumerable-elementatordefault-1(system-collections-generic-ienumerable((-0))-system-index))
 * `IEnumerable<TSource> Except<TSource>(IEqualityComparer<TSource>?, TSource[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-10.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `IEnumerable<TSource> Except<TSource>(TSource, IEqualityComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-10.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `IEnumerable<TSource> Except<TSource>(TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.except?view=net-10.0#system-linq-enumerable-except-1(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-0))))
 * `IEnumerable<TSource> ExceptBy<TSource, TKey>(IEnumerable<TKey>, Func<TSource, TKey>, IEqualityComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby?view=net-10.0#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))-system-collections-generic-iequalitycomparer((-1))))
 * `IEnumerable<TSource> ExceptBy<TSource, TKey>(IEnumerable<TKey>, Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.exceptby?view=net-10.0#system-linq-enumerable-exceptby-2(system-collections-generic-ienumerable((-0))-system-collections-generic-ienumerable((-1))-system-func((-0-1))))
 * `TSource FirstOrDefault<TSource>(Func<TSource, bool>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault?view=net-10.0#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource FirstOrDefault<TSource>(TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.firstordefault?view=net-10.0#system-linq-enumerable-firstordefault-1(system-collections-generic-ienumerable((-0))-0))
 * `IEnumerable<(int Index, TSource Item)> Index<TSource>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.index?view=net-10.0#system-linq-enumerable-index-1(system-collections-generic-ienumerable((-0))))
 * `TSource LastOrDefault<TSource>(Func<TSource, bool>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault?view=net-10.0#system-linq-enumerable-lastordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource LastOrDefault<TSource>(TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.lastordefault?view=net-10.0#system-linq-enumerable-lastordefault-1(system-collections-generic-ienumerable((-0))-0))
 * `TSource? Max<TSource>(IComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.max?view=net-10.0#system-linq-enumerable-max-1(system-collections-generic-ienumerable((-0))-system-collections-generic-icomparer((-0))))
 * `TSource? MaxBy<TSource, TKey>(Func<TSource, TKey>, IComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby?view=net-10.0#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1))))
 * `TSource? MaxBy<TSource, TKey>(Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.maxby?view=net-10.0#system-linq-enumerable-maxby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `TSource? Min<TSource>(IComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.min?view=net-10.0#system-linq-enumerable-min-1(system-collections-generic-ienumerable((-0))-system-collections-generic-icomparer((-0))))
 * `TSource? MinBy<TSource, TKey>(Func<TSource, TKey>, IComparer<TKey>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.minby?view=net-10.0#system-linq-enumerable-minby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))-system-collections-generic-icomparer((-1))))
 * `TSource? MinBy<TSource, TKey>(Func<TSource, TKey>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.minby?view=net-10.0#system-linq-enumerable-minby-2(system-collections-generic-ienumerable((-0))-system-func((-0-1))))
 * `TSource SingleOrDefault<TSource>(Func<TSource, bool>, TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.singleordefault?view=net-10.0#system-linq-enumerable-singleordefault-1(system-collections-generic-ienumerable((-0))-system-func((-0-system-boolean))-0))
 * `TSource SingleOrDefault<TSource>(TSource)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.singleordefault?view=net-10.0#system-linq-enumerable-singleordefault-1(system-collections-generic-ienumerable((-0))-0))
 * `IEnumerable<TSource> SkipLast<TSource>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.skiplast?view=net-10.0)
 * `IEnumerable<TSource> Take<TSource>(Range)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.take?view=net-10.0#system-linq-enumerable-take-1(system-collections-generic-ienumerable((-0))-system-range))
 * `IEnumerable<TSource> TakeLast<TSource>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.takelast?view=net-10.0)
 * `HashSet<TSource> ToHashSet<TSource>(IEqualityComparer<TSource>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.tohashset?view=net-10.0#system-linq-enumerable-tohashset-1(system-collections-generic-ienumerable((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `bool TryGetNonEnumeratedCount<TSource>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.trygetnonenumeratedcount?view=net-10.0)


#### IList<T>

 * `ReadOnlyCollection<T> AsReadOnly<T>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly?view=net-10.0#system-collections-generic-collectionextensions-asreadonly-1(system-collections-generic-ilist((-0))))


#### Int16

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryformat?view=net-10.0#system-int16-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryformat?view=net-10.0#system-int16-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-10.0#system-int16-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-10.0#system-int16-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<byte>, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-10.0#system-int16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-10.0#system-int16-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-10.0#system-int16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int16@))
 * `bool TryParse(ReadOnlySpan<char>, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-10.0#system-int16-tryparse(system-readonlyspan((system-char))-system-int16@))
 * `bool TryParse(string?, IFormatProvider?, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int16.tryparse?view=net-10.0#system-int16-tryparse(system-string-system-iformatprovider-system-int16@))


#### Int32

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryformat?view=net-10.0#system-int32-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryformat?view=net-10.0#system-int32-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0#system-int32-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int32@))
 * `bool TryParse(ReadOnlySpan<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0#system-int32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int32@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0#system-int32-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int32@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0#system-int32-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int32@))
 * `bool TryParse(ReadOnlySpan<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0#system-int32-tryparse(system-readonlyspan((system-char))-system-int32@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0#system-int32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int32@))
 * `bool TryParse(string?, IFormatProvider?, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int32.tryparse?view=net-10.0#system-int32-tryparse(system-string-system-iformatprovider-system-int32@))


#### Int64

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryformat?view=net-10.0#system-int64-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryformat?view=net-10.0#system-int64-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-10.0#system-int64-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-int64@))
 * `bool TryParse(ReadOnlySpan<byte>, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-10.0#system-int64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int64@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-10.0#system-int64-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-int64@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-10.0#system-int64-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-int64@))
 * `bool TryParse(ReadOnlySpan<char>, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-10.0#system-int64-tryparse(system-readonlyspan((system-char))-system-int64@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-10.0#system-int64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-int64@))
 * `bool TryParse(string?, IFormatProvider?, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.int64.tryparse?view=net-10.0#system-int64-tryparse(system-string-system-iformatprovider-system-int64@))


#### IReadOnlyDictionary<TKey, TValue>

 * `TValue GetValueOrDefault<TKey, TValue>(TKey, TValue) where TKey : notnull` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.getvalueordefault?view=net-10.0#system-collections-generic-collectionextensions-getvalueordefault-2(system-collections-generic-ireadonlydictionary((-0-1))-0-1))


#### ISet<T>

 * `ReadOnlySet<T> AsReadOnly<T>()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.asreadonly?view=net-10.0#system-collections-generic-collectionextensions-asreadonly-1(system-collections-generic-iset((-0))))


#### KeyValuePair<TKey, TValue>

 * `void Deconstruct<TKey, TValue>(TKey, TValue)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.keyvaluepair-2.deconstruct?view=net-10.0)


#### List<T>

 * `void AddRange<T>(ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.addrange?view=net-10.0)
 * `void CopyTo<T>(Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.copyto?view=net-10.0)
 * `void EnsureCapacity<T>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.ensurecapacity?view=net-10.0#system-collections-generic-list-1-ensurecapacity(system-int32))
 * `void InsertRange<T>(int, ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.collectionextensions.insertrange?view=net-10.0)
 * `void TrimExcess<T>()`


#### Math

 * `byte Clamp(byte, byte, byte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `decimal Clamp(decimal, decimal, decimal)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `double Clamp(double, double, double)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `float Clamp(float, float, float)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `int Clamp(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `long Clamp(long, long, long)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `nint Clamp(nint, nint, nint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `nuint Clamp(nuint, nuint, nuint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `sbyte Clamp(sbyte, sbyte, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `short Clamp(short, short, short)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `uint Clamp(uint, uint, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `ulong Clamp(ulong, ulong, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)
 * `ushort Clamp(ushort, ushort, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.math.clamp?view=net-10.0)


#### MemberInfo

 * `NullabilityState GetNullability()`
 * `NullabilityInfo GetNullabilityInfo()`
 * `bool HasSameMetadataDefinitionAs(MemberInfo)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo.hassamemetadatadefinitionas?view=net-10.0)
 * `bool IsNullable()`


#### MethodInfo

 * `T CreateDelegate<T>(object?) where T : Delegate` [reference](https://learn.microsoft.com/en-us/dotnet/api/System.Reflection.MethodInfo.CreateDelegate?view=net-10.0#system-reflection-methodinfo-createdelegate-1(system-object))
 * `T CreateDelegate<T>() where T : Delegate` [reference](https://learn.microsoft.com/en-us/dotnet/api/System.Reflection.MethodInfo.CreateDelegate?view=net-10.0#system-reflection-methodinfo-createdelegate-1)


#### OperatingSystem

 * `bool IsAndroid()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isandroid?view=net-10.0)
 * `bool IsAndroidVersionAtLeast(int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isandroidversionatleast?view=net-10.0)
 * `bool IsBrowser()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isbrowser?view=net-10.0)
 * `bool IsFreeBSD()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isfreebsd?view=net-10.0)
 * `bool IsFreeBSDVersionAtLeast(int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isfreebsdversionatleast?view=net-10.0)
 * `bool IsIOS()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isios?view=net-10.0)
 * `bool IsIOSVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isiosversionatleast?view=net-10.0)
 * `bool IsLinux()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.islinux?view=net-10.0)
 * `bool IsMacCatalyst()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismaccatalyst?view=net-10.0)
 * `bool IsMacCatalystVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismaccatalystversionatleast?view=net-10.0)
 * `bool IsMacOS()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismacos?view=net-10.0)
 * `bool IsMacOSVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.ismacosversionatleast?view=net-10.0)
 * `bool IsOSPlatform(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isosplatform?view=net-10.0)
 * `bool IsOSPlatformVersionAtLeast(string, int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.isosplatformversionatleast?view=net-10.0)
 * `bool IsTvOS()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.istvos?view=net-10.0)
 * `bool IsTvOSVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.istvosversionatleast?view=net-10.0)
 * `bool IsWasi()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswasi?view=net-10.0)
 * `bool IsWatchOS()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswatchos?view=net-10.0)
 * `bool IsWatchOSVersionAtLeast(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswatchosversionatleast?view=net-10.0)
 * `bool IsWindows()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswindows?view=net-10.0)
 * `bool IsWindowsVersionAtLeast(int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.operatingsystem.iswindowsversionatleast?view=net-10.0)


#### OrderedDictionary<TKey, TValue>

 * `bool TryAdd<TKey, TValue>(TKey, TValue, int) where TKey : notnull` [reference](https://github.com/dotnet/core/blob/main/release-notes/10.0/preview/preview1/libraries.md#additional-tryadd-and-trygetvalue-overloads-for-ordereddictionarytkey-tvalue?view=net-10.0)
 * `bool TryGetValue<TKey, TValue>(TKey, TValue, int) where TKey : notnull` [reference](https://github.com/dotnet/core/blob/main/release-notes/10.0/preview/preview1/libraries.md#additional-tryadd-and-trygetvalue-overloads-for-ordereddictionarytkey-tvalue?view=net-10.0)


#### ParameterInfo

 * `NullabilityState GetNullability()`
 * `NullabilityInfo GetNullabilityInfo()`
 * `bool IsNullable()`


#### Path

 * `string Combine(ReadOnlySpan<string>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.combine?view=net-10.0#system-io-path-combine(system-readonlyspan((system-string))))
 * `bool EndsInDirectorySeparator(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.endsindirectoryseparator?view=net-10.0#system-io-path-endsindirectoryseparator(system-readonlyspan((system-char))))
 * `bool EndsInDirectorySeparator(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.endsindirectoryseparator?view=net-10.0#system-io-path-endsindirectoryseparator(system-string))
 * `bool Exists(string?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.exists?view=net-10.0)
 * `ReadOnlySpan<char> GetDirectoryName(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getdirectoryname?view=net-10.0#system-io-path-getdirectoryname(system-readonlyspan((system-char))))
 * `ReadOnlySpan<char> GetExtension(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getextension?view=net-10.0#system-io-path-getextension(system-readonlyspan((system-char))))
 * `ReadOnlySpan<char> GetFileName(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilename?view=net-10.0#system-io-path-getfilename(system-readonlyspan((system-char))))
 * `ReadOnlySpan<char> GetFileNameWithoutExtension(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilenamewithoutextension?view=net-10.0#system-io-path-getfilenamewithoutextension(system-readonlyspan((system-char))))
 * `bool HasExtension(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.getfilenamewithoutextension?view=net-10.0#system-io-path-getfilenamewithoutextension(system-readonlyspan((system-char))))
 * `ReadOnlySpan<char> TrimEndingDirectorySeparator(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.trimendingdirectoryseparator?view=net-10.0#system-io-path-trimendingdirectoryseparator(system-readonlyspan((system-char))))
 * `string TrimEndingDirectorySeparator(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.path.trimendingdirectoryseparator?view=net-10.0#system-io-path-trimendingdirectoryseparator(system-string))


#### Process

 * `void Kill(bool)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.kill?view=net-10.0#system-diagnostics-process-kill(system-boolean))
 * `Task WaitForExitAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.process.waitforexitasync?view=net-10.0)


#### PropertyInfo

 * `NullabilityState GetNullability()`
 * `NullabilityInfo GetNullabilityInfo()`
 * `bool IsNullable()`


#### Queue<T>

 * `void EnsureCapacity<T>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.ensurecapacity?view=net-10.0#system-collections-generic-queue-1-ensurecapacity(system-int32))
 * `void TrimExcess<T>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.trimexcess?view=net-10.0#system-collections-generic-queue-1-trimexcess(system-int32))


#### Random

 * `T[] GetItems<T>(ReadOnlySpan<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems?view=net-10.0#system-random-getitems-1(system-readonlyspan((-0))-system-int32))
 * `void GetItems<T>(ReadOnlySpan<T>, Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems?view=net-10.0#system-random-getitems-1(system-readonlyspan((-0))-system-span((-0))))
 * `T[] GetItems<T>(T[], int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.getitems?view=net-10.0#system-random-getitems-1(-0()-system-int32))
 * `void NextBytes(Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes?view=net-10.0#system-random-nextbytes(system-span((system-byte))))
 * `void Shuffle<T>(Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes?view=net-10.0#system-random-nextbytes(system-span((system-byte))))
 * `void Shuffle<T>(T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.nextbytes?view=net-10.0#system-random-nextbytes(system-span((system-byte))))
 * `Shared` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.random.shared?view=net-10.0)


#### RandomNumberGenerator

 * `void Fill(Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.fill?view=net-10.0)
 * `byte[] GetBytes(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getbytes?view=net-10.0#system-security-cryptography-randomnumbergenerator-getbytes(system-int32))
 * `string GetHexString(int, bool)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.gethexstring?view=net-10.0#system-security-cryptography-randomnumbergenerator-gethexstring(system-int32-system-boolean))
 * `void GetHexString(Span<char>, bool)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getstring?view=net-10.0)
 * `int GetInt32(int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getint32?view=net-10.0#system-security-cryptography-randomnumbergenerator-getint32(system-int32-system-int32))
 * `int GetInt32(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getint32?view=net-10.0#system-security-cryptography-randomnumbergenerator-getint32(system-int32))
 * `T[] GetItems<T>(ReadOnlySpan<T>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getitems?view=net-10.0#system-security-cryptography-randomnumbergenerator-getitems-1(system-readonlyspan((-0))-system-int32))
 * `void GetItems<T>(ReadOnlySpan<T>, Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getitems?view=net-10.0#system-security-cryptography-randomnumbergenerator-getitems-1(system-readonlyspan((-0))-system-span((-0))))
 * `string GetString(ReadOnlySpan<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.getstring?view=net-10.0)
 * `void Shuffle<T>(Span<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.randomnumbergenerator.shuffle?view=net-10.0)


#### ReadOnlySpan<char>

 * `bool EndsWith(string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-10.0#system-memoryextensions-endswith-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `SpanLineEnumerator EnumerateLines()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines?view=net-10.0#system-memoryextensions-enumeratelines(system-readonlyspan((system-char))))
 * `int GetNormalizedLength(NormalizationForm)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.getnormalizedlength?view=net-10.0#system-stringnormalizationextensions-getnormalizedlength(system-readonlyspan((system-char))-system-text-normalizationform))
 * `bool IsNormalized(NormalizationForm)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.isnormalized?view=net-10.0#system-stringnormalizationextensions-isnormalized(system-readonlyspan((system-char))-system-text-normalizationform))
 * `bool SequenceEqual(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal?view=net-10.0#system-memoryextensions-sequenceequal-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `bool StartsWith(string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith?view=net-10.0#system-memoryextensions-startswith-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `bool TryNormalize(Span<char>, int, NormalizationForm)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.stringnormalizationextensions.trynormalize?view=net-10.0#system-stringnormalizationextensions-trynormalize(system-readonlyspan((system-char))-system-span((system-char))-system-int32@-system-text-normalizationform))


#### ReadOnlySpan<T>

 * `int CommonPrefixLength<T>(ReadOnlySpan<T>, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-10.0#system-memoryextensions-commonprefixlength-1(system-span((-0))-system-readonlyspan((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `int CommonPrefixLength<T>(ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-10.0#system-memoryextensions-commonprefixlength-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `bool Contains<T>(T, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-10.0#system-memoryextensions-contains-1(system-readonlyspan((-0))-0-system-collections-generic-iequalitycomparer((-0))))
 * `bool Contains<T>(T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-10.0#system-memoryextensions-contains-1(system-readonlyspan((-0))-0))
 * `int CountAny<T>(ReadOnlySpan<T>, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-10.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-readonlyspan((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `int CountAny<T>(ReadOnlySpan<T>) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-10.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `int CountAny<T>(SearchValues<T>) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-10.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0))))
 * `bool EndsWith<T>(T) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-10.0#system-memoryextensions-endswith-1(system-readonlyspan((-0))-0))
 * `int IndexOf<T>(T, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.indexof?view=net-10.0#system-memoryextensions-indexof-1(system-readonlyspan((-0))-0-system-collections-generic-iequalitycomparer((-0))))
 * `int IndexOfAny<T>(ReadOnlySpan<T>, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.countany?view=net-10.0#system-memoryextensions-countany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0))))
 * `SpanSplitEnumerator<T> Split<T>(ReadOnlySpan<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split?view=net-10.0#system-memoryextensions-split-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `SpanSplitEnumerator<T> Split<T>(T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.split?view=net-10.0#system-memoryextensions-split-1(system-readonlyspan((-0))-0))
 * `SpanSplitEnumerator<T> SplitAny<T>(ReadOnlySpan<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany?view=net-10.0#system-memoryextensions-splitany-1(system-readonlyspan((-0))-system-readonlyspan((-0))))
 * `SpanSplitEnumerator<T> SplitAny<T>(SearchValues<T>) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.splitany?view=net-10.0#system-memoryextensions-splitany-1(system-readonlyspan((-0))-system-buffers-searchvalues((-0))))
 * `bool StartsWith<T>(T) where T : IEquatable<T>?` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-10.0#system-memoryextensions-endswith-1(system-readonlyspan((-0))-0))


#### Regex

 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-10.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-int32))
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-10.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))))
 * `bool IsMatch(ReadOnlySpan<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-10.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-int32))
 * `bool IsMatch(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-10.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))))
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, string, RegexOptions, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-10.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan))
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, string, RegexOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-10.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions))
 * `ValueMatchEnumerator EnumerateMatches(ReadOnlySpan<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.enumeratematches?view=net-10.0#system-text-regularexpressions-regex-enumeratematches(system-readonlyspan((system-char))-system-string))
 * `bool IsMatch(ReadOnlySpan<char>, string, RegexOptions, TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-10.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions-system-timespan))
 * `bool IsMatch(ReadOnlySpan<char>, string, RegexOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-10.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string-system-text-regularexpressions-regexoptions))
 * `bool IsMatch(ReadOnlySpan<char>, string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.ismatch?view=net-10.0#system-text-regularexpressions-regex-ismatch(system-readonlyspan((system-char))-system-string))


#### SByte

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryformat?view=net-10.0#system-sbyte-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryformat?view=net-10.0#system-sbyte-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<byte>, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-sbyte@))
 * `bool TryParse(ReadOnlySpan<char>, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-readonlyspan((system-char))-system-sbyte@))
 * `bool TryParse(string?, IFormatProvider?, sbyte)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.sbyte.tryparse?view=net-10.0#system-sbyte-tryparse(system-string-system-iformatprovider-system-sbyte@))


#### SHA256

 * `byte[] HashData(byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-10.0#system-security-cryptography-sha256-hashdata(system-byte()))
 * `int HashData(ReadOnlySpan<byte>, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-10.0?system-security-cryptography-sha256-hashdata(system-readonlyspan((system-byte))-system-span((system-byte))))
 * `byte[] HashData(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-10.0?system-security-cryptography-sha256-hashdata(system-readonlyspan((system-byte))))
 * `int HashData(Stream, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-10.0?system-security-cryptography-sha256-hashdata(system-io-stream-system-span((system-byte))))
 * `byte[] HashData(Stream)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdata?view=net-10.0?system-security-cryptography-sha256-hashdata(system-io-stream))
 * `ValueTask<byte[]> HashDataAsync(Stream, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdataasync?view=net-10.0?system-security-cryptography-sha256-hashdataasync(system-io-stream-system-threading-cancellationtoken))
 * `ValueTask<int> HashDataAsync(Stream, Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.hashdataasync?view=net-10.0?system-security-cryptography-sha256-hashdataasync(system-io-stream-system-memory((system-byte))-system-threading-cancellationtoken))
 * `bool TryHashData(ReadOnlySpan<byte>, Span<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha256.tryhashdata?view=net-10.0)


#### SHA512

 * `byte[] HashData(byte[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-10.0#system-security-cryptography-sha512-hashdata(system-byte()))
 * `int HashData(ReadOnlySpan<byte>, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-10.0?system-security-cryptography-sha512-hashdata(system-readonlyspan((system-byte))-system-span((system-byte))))
 * `byte[] HashData(ReadOnlySpan<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-10.0?system-security-cryptography-sha512-hashdata(system-readonlyspan((system-byte))))
 * `int HashData(Stream, Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-10.0?system-security-cryptography-sha512-hashdata(system-io-stream-system-span((system-byte))))
 * `byte[] HashData(Stream)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdata?view=net-10.0?system-security-cryptography-sha512-hashdata(system-io-stream))
 * `ValueTask<byte[]> HashDataAsync(Stream, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdataasync?view=net-10.0?system-security-cryptography-sha512-hashdataasync(system-io-stream-system-threading-cancellationtoken))
 * `ValueTask<int> HashDataAsync(Stream, Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.hashdataasync?view=net-10.0?system-security-cryptography-sha512-hashdataasync(system-io-stream-system-memory((system-byte))-system-threading-cancellationtoken))
 * `bool TryHashData(ReadOnlySpan<byte>, Span<byte>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.sha512.tryhashdata?view=net-10.0)


#### Single

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.single.tryformat?view=net-10.0#system-single-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.single.tryformat?view=net-10.0#system-single-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### SortedList<TKey, TValue>

 * `TKey GetKeyAtIndex<TKey, TValue>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getkeyatindex?view=net-10.0)
 * `TValue GetValueAtIndex<TKey, TValue>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.sortedlist-2.getvalueatindex?view=net-10.0)


#### Span<char>

 * `bool EndsWith(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.endswith?view=net-10.0#system-memoryextensions-endswith-1(system-span((-0))-system-readonlyspan((-0))))
 * `SpanLineEnumerator EnumerateLines()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.enumeratelines?view=net-10.0#system-memoryextensions-enumeratelines(system-span((system-char))))
 * `bool SequenceEqual(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.sequenceequal?view=net-10.0#system-memoryextensions-sequenceequal-1(system-span((-0))-system-readonlyspan((-0))))
 * `bool StartsWith(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.startswith?view=net-10.0#system-memoryextensions-startswith-1(system-span((-0))-system-readonlyspan((-0))))
 * `Span<char> TrimEnd()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.trimend?view=net-10.0#system-memoryextensions-trimend(system-span((system-char))))
 * `Span<char> TrimStart()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.trimstart?view=net-10.0#system-memoryextensions-trimstart(system-span((system-char))))


#### Span<T>

 * `int CommonPrefixLength<T>(ReadOnlySpan<T>, IEqualityComparer<T>?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-10.0#system-memoryextensions-commonprefixlength-1(system-span((-0))-system-readonlyspan((-0))-system-collections-generic-iequalitycomparer((-0))))
 * `int CommonPrefixLength<T>(ReadOnlySpan<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.commonprefixlength?view=net-10.0#system-memoryextensions-commonprefixlength-1(system-span((-0))-system-readonlyspan((-0))))
 * `bool Contains<T>(T) where T : IEquatable<T>` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.memoryextensions.contains?view=net-10.0#system-memoryextensions-contains-1(system-span((-0))-0))


#### Stack<T>

 * `void EnsureCapacity<T>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.ensurecapacity?view=net-10.0)
 * `void TrimExcess<T>(int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trimexcess?view=net-10.0#system-collections-generic-stack-1-trimexcess(system-int32))
 * `bool TryPeek<T>(T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trypeek?view=net-10.0)
 * `bool TryPop<T>(T)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1.trypop?view=net-10.0)


#### Stream

 * `Task CopyToAsync(Stream, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.copytoasync?view=net-10.0#system-io-stream-copytoasync(system-io-stream-system-threading-cancellationtoken))
 * `ValueTask DisposeAsync()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.disposeasync?view=net-10.0)
 * `int Read(Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.read?view=net-10.0#system-io-stream-read(system-span((system-byte))))
 * `ValueTask<int> ReadAsync(Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readasync?view=net-10.0#system-io-stream-readasync(system-memory((system-byte))-system-threading-cancellationtoken))
 * `int ReadAtLeast(Span<byte>, int, bool)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readatleast?view=net-10.0)
 * `ValueTask<int> ReadAtLeastAsync(Memory<byte>, int, bool, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readatleastasync?view=net-10.0)
 * `void ReadExactly(byte[], int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readexactly?view=net-10.0#system-io-stream-readexactly(system-byte()-system-int32-system-int32))
 * `void ReadExactly(Span<byte>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readexactly?view=net-10.0#system-io-stream-readexactly(system-span((system-byte))))
 * `ValueTask ReadExactlyAsync(byte[], int, int, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readexactlyasync?view=net-10.0#system-io-stream-readexactlyasync(system-byte()-system-int32-system-int32-system-threading-cancellationtoken))
 * `ValueTask ReadExactlyAsync(Memory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.readatleastasync?view=net-10.0)
 * `ValueTask WriteAsync(ReadOnlyMemory<byte>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream.writeasync?view=net-10.0#system-io-stream-writeasync(system-readonlymemory((system-byte))-system-threading-cancellationtoken))


#### String

 * `bool Contains(char, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-10.0#system-string-contains(system-char-system-stringcomparison))
 * `bool Contains(char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-10.0#system-string-contains(system-char))
 * `bool Contains(string, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.contains?view=net-10.0#system-string-contains(system-string-system-stringcomparison))
 * `void CopyTo(Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.copyto?view=net-10.0)
 * `bool EndsWith(char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.endswith?view=net-10.0#system-string-endswith(system-char))
 * `int GetHashCode(StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-10.0#system-string-gethashcode(system-stringcomparison))
 * `int IndexOf(char, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.indexof?view=net-10.0#system-string-indexof(system-char-system-stringcomparison))
 * `string ReplaceLineEndings(string)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.replacelineendings?view=net-10.0#system-string-replacelineendings(system-string))
 * `string ReplaceLineEndings()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.replacelineendings?view=net-10.0#system-string-replacelineendings)
 * `string[] Split(char, int, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-10.0#system-string-split(system-char-system-int32-system-stringsplitoptions))
 * `string[] Split(char, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-10.0#system-string-split(system-char-system-stringsplitoptions))
 * `string[] Split(string, int, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-10.0#system-string-split(system-string-system-int32-system-stringsplitoptions))
 * `string[] Split(string, StringSplitOptions)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.split?view=net-10.0#system-string-split(system-string-system-stringsplitoptions))
 * `bool StartsWith(char)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.startswith?view=net-10.0#system-string-startswith(system-char))
 * `bool TryCopyTo(Span<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.trycopyto?view=net-10.0)
 * `int GetHashCode(ReadOnlySpan<char>, StringComparison)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-10.0#system-string-gethashcode(system-readonlyspan((system-char))-system-stringcomparison))
 * `int GetHashCode(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.gethashcode?view=net-10.0#system-string-gethashcode(system-readonlyspan((system-char))))
 * `string Join(char, object?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join(system-char-system-object()))
 * `string Join(char, ReadOnlySpan<object?>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join(system-char-system-readonlyspan((system-object))))
 * `string Join(char, ReadOnlySpan<string?>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join(system-char-system-readonlyspan((system-string))))
 * `string Join(char, string?[], int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join(system-char-system-string()-system-int32-system-int32))
 * `string Join(char, string?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join(system-char-system-string()))
 * `string Join(string?, ReadOnlySpan<object?>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join(system-string-system-readonlyspan((system-object))))
 * `string Join(string?, ReadOnlySpan<string?>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join(system-string-system-readonlyspan((system-string))))
 * `string Join<T>(char, IEnumerable<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.string.join?view=net-10.0#system-string-join-1(system-char-system-collections-generic-ienumerable((-0))))


#### StringBuilder

 * `StringBuilder Append(StringBuilder, AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-10.0#system-text-stringbuilder-append(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder Append(StringBuilder, IFormatProvider?, AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-10.0#system-text-stringbuilder-append(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder Append(StringBuilder, IFormatProvider?, StringBuilder.AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-10.0#system-text-stringbuilder-append(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder Append(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-10.0#system-text-stringbuilder-append(system-readonlyspan((system-char))))
 * `StringBuilder Append(StringBuilder?, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-10.0#system-text-stringbuilder-append(system-text-stringbuilder-system-int32-system-int32))
 * `StringBuilder Append(StringBuilder, StringBuilder.AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.append?view=net-10.0#system-text-stringbuilder-append(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendJoin(char, object?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin(system-char-system-object()))
 * `StringBuilder AppendJoin(char, string?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin(system-char-system-string()))
 * `StringBuilder AppendJoin(string?, object?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin(system-string-system-object()))
 * `StringBuilder AppendJoin(string?, string?[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin(system-string-system-string()))
 * `StringBuilder AppendJoin<T>(char, IEnumerable<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin<T>(char, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin<T>(string, T[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin-1(system-string-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendJoin<T>(string?, IEnumerable<T>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendjoin?view=net-10.0#system-text-stringbuilder-appendjoin-1(system-char-system-collections-generic-ienumerable((-0))))
 * `StringBuilder AppendLine(StringBuilder, AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline?view=net-10.0#system-text-stringbuilder-appendline(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendLine(StringBuilder, IFormatProvider?, AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline?view=net-10.0#system-text-stringbuilder-appendline(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendLine(StringBuilder, IFormatProvider?, StringBuilder.AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline?view=net-10.0#system-text-stringbuilder-appendline(system-iformatprovider-system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `StringBuilder AppendLine(StringBuilder, StringBuilder.AppendInterpolatedStringHandler)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.appendline?view=net-10.0#system-text-stringbuilder-appendline(system-text-stringbuilder-appendinterpolatedstringhandler@))
 * `void CopyTo(int, Span<char>, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.copyto?view=net-10.0#system-text-stringbuilder-copyto(system-int32-system-span((system-char))-system-int32))
 * `bool Equals(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.equals?view=net-10.0#system-text-stringbuilder-equals(system-readonlyspan((system-char))))
 * `ChunkEnumerator GetChunks()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.getchunks?view=net-10.0)
 * `StringBuilder Insert(int, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.insert?view=net-10.0#system-text-stringbuilder-insert(system-int32-system-readonlyspan((system-char))))
 * `StringBuilder Replace(ReadOnlySpan<char>, ReadOnlySpan<char>, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace?view=net-10.0#system-text-stringbuilder-replace(system-char-system-char-system-int32-system-int32))
 * `StringBuilder Replace(ReadOnlySpan<char>, ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.text.stringbuilder.replace?view=net-10.0#system-text-stringbuilder-replace(system-readonlyspan((system-char))-system-readonlyspan((system-char))))


#### Task

 * `Task WaitAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-10.0#system-threading-tasks-task-waitasync(system-threading-cancellationtoken))
 * `Task WaitAsync(TimeSpan, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-10.0#system-threading-tasks-task-waitasync(system-timespan-system-threading-cancellationtoken))
 * `Task WaitAsync(TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-10.0#system-threading-tasks-task-waitasync(system-timespan))


#### Task<TResult>

 * `Task<TResult> WaitAsync<TResult>(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task.waitasync?view=net-10.0#system-threading-tasks-task-waitasync(system-threading-cancellationtoken))
 * `Task<TResult> WaitAsync<TResult>(TimeSpan, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync?view=net-10.0#system-threading-tasks-task-1-waitasync(system-timespan-system-threading-cancellationtoken))
 * `Task<TResult> WaitAsync<TResult>(TimeSpan)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1.waitasync?view=net-10.0#system-threading-tasks-task-1-waitasync(system-timespan))


#### TaskCompletionSource<T>

 * `void SetCanceled<T>(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.taskcompletionsource-1.setcanceled?view=net-10.0#system-threading-tasks-taskcompletionsource-1-setcanceled(system-threading-cancellationtoken))


#### TextReader

 * `ValueTask<int> ReadAsync(Memory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readasync?view=net-10.0#system-io-textreader-readasync(system-memory((system-char))-system-threading-cancellationtoken))
 * `Task<string> ReadLineAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync?view=net-10.0#system-io-textreader-readlineasync(system-threading-cancellationtoken))
 * `Task<string> ReadToEndAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textreader.readtoendasync?view=net-10.0#system-io-textreader-readtoendasync(system-threading-cancellationtoken))


#### TextWriter

 * `Task FlushAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.flushasync?view=net-10.0#system-io-textwriter-flushasync(system-threading-cancellationtoken))
 * `void Write(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write?view=net-10.0#system-io-textwriter-write(system-readonlyspan((system-char))))
 * `void Write(StringBuilder?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.write?view=net-10.0#system-io-textwriter-write(system-text-stringbuilder))
 * `ValueTask WriteAsync(ReadOnlyMemory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-10.0#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `Task WriteAsync(StringBuilder?, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeasync?view=net-10.0#system-io-textwriter-writeasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))
 * `void WriteLine(ReadOnlySpan<char>)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writeline?view=net-10.0#system-io-textwriter-writeline(system-readonlyspan((system-char))))
 * `ValueTask WriteLineAsync(ReadOnlyMemory<char>, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.textwriter.writelineasync?view=net-10.0#system-io-textwriter-writelineasync(system-readonlymemory((system-char))-system-threading-cancellationtoken))


#### TimeOnly

 * `void Deconstruct(int, int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-10.0#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@-system-int32@-system-int32@))
 * `void Deconstruct(int, int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-10.0#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@-system-int32@))
 * `void Deconstruct(int, int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-10.0#system-timeonly-deconstruct(system-int32@-system-int32@-system-int32@))
 * `void Deconstruct(int, int)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.deconstruct?view=net-10.0#system-timeonly-deconstruct(system-int32@-system-int32@))
 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.tryformat?view=net-10.0#system-timeonly-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timeonly.tryformat?view=net-10.0#system-timeonly-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### TimeSpan

 * `int Microseconds()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.microseconds?view=net-10.0)
 * `int Nanoseconds()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.nanoseconds?view=net-10.0)
 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.tryformat?view=net-10.0#system-timespan-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.timespan.tryformat?view=net-10.0#system-timespan-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))


#### Type

 * `MethodInfo? GetMethod(string, int, BindingFlags, Type[])` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.getmethod?view=net-10.0#system-type-getmethod(system-string-system-int32-system-reflection-bindingflags-system-type()))
 * `bool IsAssignableFrom<T>()`
 * `bool IsAssignableTo(Type?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.isassignableto?view=net-10.0)
 * `bool IsAssignableTo<T>()`
 * `bool IsGenericMethodParameter()` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.type.isgenericmethodparameter?view=net-10.0)


#### UInt16

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryformat?view=net-10.0#system-uint16-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryformat?view=net-10.0#system-uint16-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-10.0#system-uint16-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-10.0#system-uint16-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<byte>, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-10.0#system-uint16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-10.0#system-uint16-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-10.0#system-uint16-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint16@))
 * `bool TryParse(ReadOnlySpan<char>, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-10.0#system-uint16-tryparse(system-readonlyspan((system-char))-system-uint16@))
 * `bool TryParse(string?, IFormatProvider?, ushort)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.tryparse?view=net-10.0#system-uint16-tryparse(system-string-system-iformatprovider-system-uint16@))


#### UInt32

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryformat?view=net-10.0#system-uint32-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryformat?view=net-10.0#system-uint32-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-10.0#system-uint32-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-10.0#system-uint32-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<byte>, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-10.0#system-uint32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-10.0#system-uint32-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-10.0#system-uint32-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint32@))
 * `bool TryParse(ReadOnlySpan<char>, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-10.0#system-uint32-tryparse(system-readonlyspan((system-char))-system-uint32@))
 * `bool TryParse(string?, IFormatProvider?, uint)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint32.tryparse?view=net-10.0#system-uint32-tryparse(system-string-system-iformatprovider-system-uint32@))


#### UInt64

 * `bool TryFormat(Span<byte>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryformat?view=net-10.0#system-uint64-tryformat(system-span((system-byte))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryFormat(Span<char>, int, ReadOnlySpan<char>, IFormatProvider?)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryformat?view=net-10.0#system-uint64-tryformat(system-span((system-char))-system-int32@-system-readonlyspan((system-char))-system-iformatprovider))
 * `bool TryParse(ReadOnlySpan<byte>, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-10.0#system-uint64-tryparse(system-readonlyspan((system-byte))-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<byte>, NumberStyles, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-10.0#system-uint64-tryparse(system-readonlyspan((system-byte))-system-globalization-numberstyles-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<byte>, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-10.0#system-uint64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<char>, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-10.0#system-uint64-tryparse(system-readonlyspan((system-char))-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<char>, NumberStyles, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-10.0#system-uint64-tryparse(system-readonlyspan((system-char))-system-globalization-numberstyles-system-iformatprovider-system-uint64@))
 * `bool TryParse(ReadOnlySpan<char>, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-10.0#system-uint64-tryparse(system-readonlyspan((system-char))-system-uint64@))
 * `bool TryParse(string?, IFormatProvider?, ulong)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.uint64.tryparse?view=net-10.0#system-uint64-tryparse(system-string-system-iformatprovider-system-uint64@))


#### XDocument

 * `Task SaveAsync(Stream, SaveOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync?view=net-10.0#system-xml-linq-xdocument-saveasync(system-io-stream-system-xml-linq-saveoptions-system-threading-cancellationtoken))
 * `Task SaveAsync(TextWriter, SaveOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync?view=net-10.0#system-xml-linq-xdocument-saveasync(system-io-textwriter-system-xml-linq-saveoptions-system-threading-cancellationtoken))
 * `Task SaveAsync(XmlWriter, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.saveasync?view=net-10.0#system-xml-linq-xdocument-saveasync(system-xml-xmlwriter-system-threading-cancellationtoken))
 * `Task<XDocument> LoadAsync(Stream, LoadOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.loadasync?view=net-10.0#system-xml-linq-xdocument-loadasync(system-io-stream-system-xml-linq-loadoptions-system-threading-cancellationtoken))
 * `Task<XDocument> LoadAsync(TextReader, LoadOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.loadasync?view=net-10.0#system-xml-linq-xdocument-loadasync(system-io-textreader-system-xml-linq-loadoptions-system-threading-cancellationtoken))
 * `Task<XDocument> LoadAsync(XmlReader, LoadOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.loadasync?view=net-10.0#system-xml-linq-xdocument-loadasync(system-xml-xmlreader-system-xml-linq-loadoptions-system-threading-cancellationtoken))


#### XElement

 * `Task<XElement> LoadAsync(Stream, LoadOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.loadasync?view=net-10.0#system-xml-linq-xelement-loadasync(system-io-stream-system-xml-linq-loadoptions-system-threading-cancellationtoken))
 * `Task<XElement> LoadAsync(TextReader, LoadOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.loadasync?view=net-10.0#system-xml-linq-xelement-loadasync(system-io-textreader-system-xml-linq-loadoptions-system-threading-cancellationtoken))
 * `Task<XElement> LoadAsync(XmlReader, LoadOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.loadasync?view=net-10.0#system-xml-linq-xelement-loadasync(system-xml-xmlreader-system-xml-linq-loadoptions-system-threading-cancellationtoken))


#### XElement

 * `Task SaveAsync(Stream, SaveOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.saveasync?view=net-10.0#system-xml-linq-xelement-saveasync(system-io-stream-system-xml-linq-saveoptions-system-threading-cancellationtoken))
 * `Task SaveAsync(TextWriter, SaveOptions, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.saveasync?view=net-10.0#system-xml-linq-xelement-saveasync(system-io-textwriter-system-xml-linq-saveoptions-system-threading-cancellationtoken))
 * `Task SaveAsync(XmlWriter, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xelement.saveasync?view=net-10.0#system-xml-linq-xelement-saveasync(system-xml-xmlwriter-system-threading-cancellationtoken))


#### ZipArchive

 * `Task<ZipArchiveEntry> CreateEntryFromFileAsync(string, string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.createentryfromfileasync?view=net-10.0)
 * `Task<ZipArchiveEntry> CreateEntryFromFileAsync(string, string, CompressionLevel, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.createentryfromfileasync?view=net-10.0)
 * `void ExtractToDirectory(string, bool)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttodirectory?view=net-10.0#system-io-compression-zipfileextensions-extracttodirectory(system-io-compression-ziparchive-system-string-system-boolean))
 * `Task ExtractToDirectoryAsync(string, bool, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttodirectoryasync?view=net-10.0#system-io-compression-zipfileextensions-extracttodirectoryasync(system-io-compression-ziparchive-system-string-system-boolean-system-threading-cancellationtoken))
 * `Task ExtractToDirectoryAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttodirectory?view=net-10.0#system-io-compression-zipfileextensions-extracttodirectory(system-io-compression-ziparchive-system-string))


#### ZipArchiveEntry

 * `Task ExtractToFileAsync(string, bool, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttofileasync?view=net-10.0#system-io-compression-zipfileextensions-extracttofileasync(system-io-compression-ziparchiveentry-system-string-system-boolean-system-threading-cancellationtoken))
 * `Task ExtractToFileAsync(string, CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.zipfileextensions.extracttofileasync?view=net-10.0#system-io-compression-zipfileextensions-extracttofileasync(system-io-compression-ziparchiveentry-system-string-system-threading-cancellationtoken))
 * `Task<Stream> OpenAsync(CancellationToken)` [reference](https://learn.microsoft.com/en-us/dotnet/api/system.io.compression.ziparchiveentry.openasync?view=net-10.0)


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

