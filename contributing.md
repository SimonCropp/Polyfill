## Solution Structure


### Polyfill

The main project that produces the nuget.


### Tests

A nUnit test project that verifies all the APIs.


### NoRefsTests

Some features of Polyfill [require nuget references](/#references) to be enabled. The NoRefsTests project had none of those refecences and tests the subset of features that do not require references.


### PublicTests

Polyfill supports [making all APIs public](#consuming-and-type-visibility). The PublicTests project tests that scenario.


### UnsafeTests

Some feature of Polyfill leverage unsafe code for better performance. For example `Append(this StringBuilder, ReadOnlySpan<char>)`. The UnsafeTests project tests this scenario vie enabling `<AllowUnsafeBlocks>True</AllowUnsafeBlocks>`.


### Consume

Polufill supports back to `net461` and `netcoreapp2.0`. However nUnit only support back to `net462` and `netcoreapp3.1`. The Consume project targets all frameworks that Polyfill supports, and consumes all APIs to ensure that they all compile on those frameworks