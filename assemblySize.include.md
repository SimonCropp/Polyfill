### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       368.0KB |  +360.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       323.5KB |  +315.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       367.0KB |  +358.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.0KB |
| net462         |          7.0KB |       372.0KB |  +365.0KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net47          |          7.0KB |       371.5KB |  +364.5KB |    +7.5KB |             +6.5KB |              +9.0KB |     +12.0KB |
| net471         |          8.5KB |       369.5KB |  +361.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.0KB |
| net472         |          8.5KB |       368.0KB |  +359.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       368.0KB |  +359.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       368.0KB |  +359.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       347.0KB |  +338.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       327.0KB |  +318.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       327.0KB |  +318.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       323.0KB |  +313.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       321.0KB |  +311.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net5.0         |          9.5KB |       285.5KB |  +276.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net6.0         |         10.0KB |       227.0KB |  +217.0KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       192.0KB |  +182.0KB |    +9.5KB |             +6.0KB |              +1.0KB |      +3.5KB |
| net8.0         |          9.5KB |       162.5KB |  +153.0KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net9.0         |          9.5KB |       114.0KB |  +104.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        92.0KB |   +82.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        21.0KB |   +11.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       539.7KB |  +531.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       468.6KB |  +460.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       539.8KB |  +531.3KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.4KB |
| net462         |          7.0KB |       544.8KB |  +537.8KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net47          |          7.0KB |       544.0KB |  +537.0KB |   +15.2KB |             +8.2KB |             +13.9KB |     +17.4KB |
| net471         |          8.5KB |       541.7KB |  +533.2KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.4KB |
| net472         |          8.5KB |       539.1KB |  +530.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       539.1KB |  +530.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       539.1KB |  +530.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       508.2KB |  +499.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       475.8KB |  +466.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       475.8KB |  +466.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       465.1KB |  +455.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       463.1KB |  +453.6KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net5.0         |          9.5KB |       409.5KB |  +400.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net6.0         |         10.0KB |       331.0KB |  +321.0KB |   +17.2KB |             +8.2KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       277.7KB |  +267.7KB |   +17.1KB |             +7.4KB |              +1.6KB |      +4.2KB |
| net8.0         |          9.5KB |       234.4KB |  +224.9KB |   +16.0KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |          9.5KB |       164.6KB |  +155.1KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       133.9KB |  +123.9KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        30.9KB |   +20.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
