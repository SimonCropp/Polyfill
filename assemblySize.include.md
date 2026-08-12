### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       358.5KB |  +350.5KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| netstandard2.1 |          8.5KB |       312.5KB |  +304.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       357.0KB |  +348.5KB |    +7.5KB |             +6.5KB |              +8.0KB |     +12.5KB |
| net462         |          7.0KB |       360.5KB |  +353.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       360.5KB |  +353.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       359.5KB |  +351.0KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net472         |          8.5KB |       358.0KB |  +349.5KB |    +8.0KB |             +6.5KB |              +8.0KB |     +12.5KB |
| net48          |          8.5KB |       358.0KB |  +349.5KB |    +8.0KB |             +6.5KB |              +8.0KB |     +12.5KB |
| net481         |          8.5KB |       358.0KB |  +349.5KB |    +8.0KB |             +6.5KB |              +8.0KB |     +12.5KB |
| netcoreapp2.0  |          9.0KB |       336.0KB |  +327.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       316.0KB |  +307.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       316.0KB |  +307.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       308.5KB |  +299.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       307.0KB |  +297.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       271.0KB |  +261.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net6.0         |         10.0KB |       212.5KB |  +202.5KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       175.0KB |  +165.0KB |   +11.5KB |             +8.0KB |           +512bytes |      +3.0KB |
| net8.0         |          9.5KB |       145.5KB |  +136.0KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net9.0         |          9.5KB |        98.5KB |   +89.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        76.0KB |   +66.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        20.5KB |   +10.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       524.4KB |  +516.4KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| netstandard2.1 |          8.5KB |       451.8KB |  +443.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       524.0KB |  +515.5KB |   +15.2KB |             +8.2KB |             +12.9KB |     +17.9KB |
| net462         |          7.0KB |       527.5KB |  +520.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       527.2KB |  +520.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       525.8KB |  +517.3KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net472         |          8.5KB |       523.3KB |  +514.8KB |   +15.7KB |             +8.2KB |             +12.9KB |     +17.9KB |
| net48          |          8.5KB |       523.3KB |  +514.8KB |   +15.7KB |             +8.2KB |             +12.9KB |     +17.9KB |
| net481         |          8.5KB |       523.3KB |  +514.8KB |   +15.7KB |             +8.2KB |             +12.9KB |     +17.9KB |
| netcoreapp2.0  |          9.0KB |       491.3KB |  +482.3KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       459.0KB |  +450.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       459.0KB |  +450.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       442.7KB |  +433.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       441.2KB |  +431.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       387.0KB |  +377.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net6.0         |         10.0KB |       308.4KB |  +298.4KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       252.3KB |  +242.3KB |   +19.1KB |             +9.4KB |              +1.1KB |      +3.7KB |
| net8.0         |          9.5KB |       207.3KB |  +197.8KB |   +16.0KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |          9.5KB |       139.1KB |  +129.6KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       108.0KB |   +98.0KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        30.3KB |   +20.3KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
