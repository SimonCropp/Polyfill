### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       358.5KB |  +350.5KB |    +8.0KB |             +7.0KB |              +8.0KB |     +12.5KB |
| netstandard2.1 |          8.5KB |       313.0KB |  +304.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       357.5KB |  +349.0KB |    +7.5KB |             +6.5KB |              +8.0KB |     +12.0KB |
| net462         |          7.0KB |       361.0KB |  +354.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       361.0KB |  +354.0KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       360.0KB |  +351.5KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net472         |          8.5KB |       358.5KB |  +350.0KB |    +7.5KB |             +6.5KB |              +8.0KB |     +12.5KB |
| net48          |          8.5KB |       358.5KB |  +350.0KB |    +7.5KB |             +6.5KB |              +8.0KB |     +12.5KB |
| net481         |          8.5KB |       358.5KB |  +350.0KB |    +8.0KB |             +6.5KB |              +8.0KB |     +12.5KB |
| netcoreapp2.0  |          9.0KB |       336.5KB |  +327.5KB |    +8.5KB |             +6.0KB |              +8.5KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       316.5KB |  +307.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       316.5KB |  +307.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       309.5KB |  +300.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       308.0KB |  +298.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       272.0KB |  +262.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net6.0         |         10.0KB |       213.0KB |  +203.0KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       175.0KB |  +165.0KB |   +11.5KB |             +8.0KB |           +512bytes |      +3.0KB |
| net8.0         |          9.5KB |       145.5KB |  +136.0KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net9.0         |          9.5KB |        98.5KB |   +89.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        76.0KB |   +66.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        20.5KB |   +10.5KB |    +9.0KB |                    |              +1.0KB |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       524.8KB |  +516.8KB |   +15.7KB |             +8.7KB |             +12.9KB |     +17.9KB |
| netstandard2.1 |          8.5KB |       452.7KB |  +444.2KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       524.9KB |  +516.4KB |   +15.2KB |             +8.2KB |             +12.9KB |     +17.4KB |
| net462         |          7.0KB |       528.4KB |  +521.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       528.1KB |  +521.1KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       526.8KB |  +518.3KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net472         |          8.5KB |       524.2KB |  +515.7KB |   +15.2KB |             +8.2KB |             +12.9KB |     +17.9KB |
| net48          |          8.5KB |       524.2KB |  +515.7KB |   +15.2KB |             +8.2KB |             +12.9KB |     +17.9KB |
| net481         |          8.5KB |       524.2KB |  +515.7KB |   +15.7KB |             +8.2KB |             +12.9KB |     +17.9KB |
| netcoreapp2.0  |          9.0KB |       492.3KB |  +483.3KB |   +16.2KB |             +7.7KB |             +13.4KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       459.9KB |  +450.9KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       459.9KB |  +450.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       444.0KB |  +434.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       442.5KB |  +433.0KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       388.3KB |  +378.8KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net6.0         |         10.0KB |       309.1KB |  +299.1KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       252.3KB |  +242.3KB |   +19.1KB |             +9.4KB |              +1.1KB |      +3.7KB |
| net8.0         |          9.5KB |       207.3KB |  +197.8KB |   +16.0KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |          9.5KB |       139.1KB |  +129.6KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       108.0KB |   +98.0KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        30.4KB |   +20.4KB |   +16.5KB |                    |              +1.6KB |      +4.2KB |
