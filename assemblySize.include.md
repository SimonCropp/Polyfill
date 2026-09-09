### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       358.5KB |  +350.5KB |    +8.0KB |             +6.5KB |              +8.0KB |     +12.5KB |
| netstandard2.1 |          8.5KB |       312.5KB |  +304.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       357.5KB |  +349.0KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net462         |          7.0KB |       361.0KB |  +354.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       360.5KB |  +353.5KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net471         |          8.5KB |       360.0KB |  +351.5KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net472         |          8.5KB |       358.5KB |  +350.0KB |    +7.5KB |             +6.5KB |              +8.0KB |     +12.0KB |
| net48          |          8.5KB |       358.5KB |  +350.0KB |    +7.5KB |             +6.5KB |              +8.0KB |     +12.0KB |
| net481         |          8.5KB |       358.5KB |  +350.0KB |    +7.5KB |             +6.5KB |              +8.0KB |     +12.0KB |
| netcoreapp2.0  |          9.0KB |       336.0KB |  +327.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       316.0KB |  +307.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       316.0KB |  +307.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       309.5KB |  +300.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       307.5KB |  +298.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       271.5KB |  +262.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       213.0KB |  +203.0KB |    +9.5KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       175.0KB |  +165.0KB |   +11.5KB |             +8.0KB |           +512bytes |      +3.0KB |
| net8.0         |          9.5KB |       145.5KB |  +136.0KB |    +8.0KB |                    |           +512bytes |      +3.0KB |
| net9.0         |          9.5KB |        98.5KB |   +89.0KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net10.0        |         10.0KB |        76.0KB |   +66.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        21.0KB |   +11.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       525.8KB |  +517.8KB |   +15.7KB |             +8.2KB |             +12.9KB |     +17.9KB |
| netstandard2.1 |          8.5KB |       453.1KB |  +444.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       525.8KB |  +517.3KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net462         |          7.0KB |       529.3KB |  +522.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       528.6KB |  +521.6KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net471         |          8.5KB |       527.7KB |  +519.2KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net472         |          8.5KB |       525.1KB |  +516.6KB |   +15.2KB |             +8.2KB |             +12.9KB |     +17.4KB |
| net48          |          8.5KB |       525.1KB |  +516.6KB |   +15.2KB |             +8.2KB |             +12.9KB |     +17.4KB |
| net481         |          8.5KB |       525.1KB |  +516.6KB |   +15.2KB |             +8.2KB |             +12.9KB |     +17.4KB |
| netcoreapp2.0  |          9.0KB |       492.7KB |  +483.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       460.4KB |  +451.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       460.4KB |  +451.4KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       445.0KB |  +435.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       442.9KB |  +433.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       388.8KB |  +379.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       310.1KB |  +300.1KB |   +17.2KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       253.2KB |  +243.2KB |   +19.1KB |             +9.4KB |              +1.1KB |      +3.7KB |
| net8.0         |          9.5KB |       208.2KB |  +198.7KB |   +15.5KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |          9.5KB |       140.1KB |  +130.6KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net10.0        |         10.0KB |       108.9KB |   +98.9KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        30.9KB |   +20.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
