### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       262.0KB |  +254.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       214.5KB |  +206.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       270.0KB |  +261.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       269.0KB |  +262.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       269.0KB |  +262.0KB |    +9.0KB |             +6.0KB |              +8.5KB |     +13.5KB |
| net471         |          8.5KB |       269.0KB |  +260.5KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       267.5KB |  +259.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       267.5KB |  +259.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       267.5KB |  +259.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       243.5KB |  +234.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       223.5KB |  +214.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       223.5KB |  +214.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       214.0KB |  +204.5KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       212.5KB |  +203.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       175.0KB |  +165.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       130.5KB |  +120.5KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |       100.0KB |   +90.0KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        80.0KB |   +70.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        45.0KB |   +35.5KB |    +9.0KB |                    |              +1.0KB |      +3.5KB |
| net10.0        |         10.0KB |        23.5KB |   +13.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        18.0KB |    +8.0KB |    +9.5KB |                    |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       387.9KB |  +379.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       315.0KB |  +306.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       396.4KB |  +387.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       395.4KB |  +388.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       395.1KB |  +388.1KB |   +16.7KB |             +7.7KB |             +13.4KB |     +18.9KB |
| net471         |          8.5KB |       395.1KB |  +386.6KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       392.6KB |  +384.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       392.6KB |  +384.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       392.6KB |  +384.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       359.3KB |  +350.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       328.2KB |  +319.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       328.2KB |  +319.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       309.6KB |  +300.1KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       308.1KB |  +298.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       253.2KB |  +243.8KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net6.0         |         10.0KB |       192.2KB |  +182.2KB |   +17.2KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       144.7KB |  +134.7KB |   +16.6KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       114.0KB |  +104.5KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        65.0KB |   +55.5KB |   +16.5KB |                    |              +1.6KB |      +4.2KB |
| net10.0        |         10.0KB |        35.7KB |   +25.7KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        26.9KB |   +16.9KB |   +17.0KB |                    |              +1.6KB |      +4.7KB |
