### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       359.0KB |  +351.0KB |    +7.5KB |             +6.5KB |              +8.0KB |     +12.5KB |
| netstandard2.1 |          8.5KB |       313.0KB |  +304.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       358.0KB |  +349.5KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net462         |          7.0KB |       361.5KB |  +354.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       361.0KB |  +354.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net471         |          8.5KB |       360.5KB |  +352.0KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net472         |          8.5KB |       359.0KB |  +350.5KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net48          |          8.5KB |       359.0KB |  +350.5KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net481         |          8.5KB |       359.0KB |  +350.5KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| netcoreapp2.0  |          9.0KB |       336.5KB |  +327.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       316.5KB |  +307.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       316.5KB |  +307.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       310.0KB |  +300.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       308.0KB |  +298.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       272.0KB |  +262.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       213.5KB |  +203.5KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |       175.5KB |  +165.5KB |   +11.5KB |             +8.0KB |           +512bytes |      +3.0KB |
| net8.0         |          9.5KB |       145.5KB |  +136.0KB |    +8.5KB |          +512bytes |              +1.0KB |      +3.5KB |
| net9.0         |          9.5KB |        99.0KB |   +89.5KB |    +8.0KB |                    |           +512bytes |      +3.0KB |
| net10.0        |         10.0KB |        76.5KB |   +66.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        20.5KB |   +10.5KB |    +9.0KB |                    |              +1.0KB |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       525.8KB |  +517.8KB |   +15.2KB |             +8.2KB |             +12.9KB |     +17.9KB |
| netstandard2.1 |          8.5KB |       453.2KB |  +444.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       525.9KB |  +517.4KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net462         |          7.0KB |       529.4KB |  +522.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       528.6KB |  +521.6KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net471         |          8.5KB |       527.7KB |  +519.2KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net472         |          8.5KB |       525.2KB |  +516.7KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net48          |          8.5KB |       525.2KB |  +516.7KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net481         |          8.5KB |       525.2KB |  +516.7KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| netcoreapp2.0  |          9.0KB |       492.7KB |  +483.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       460.4KB |  +451.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       460.4KB |  +451.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       445.0KB |  +435.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       443.0KB |  +433.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       388.8KB |  +379.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       310.1KB |  +300.1KB |   +17.2KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       253.2KB |  +243.2KB |   +19.1KB |             +9.4KB |              +1.1KB |      +3.7KB |
| net8.0         |          9.5KB |       207.7KB |  +198.2KB |   +16.0KB |          +811bytes |              +1.6KB |      +4.2KB |
| net9.0         |          9.5KB |       140.1KB |  +130.6KB |   +15.5KB |                    |              +1.1KB |      +3.7KB |
| net10.0        |         10.0KB |       108.9KB |   +98.9KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        30.4KB |   +20.4KB |   +16.5KB |                    |              +1.6KB |      +4.2KB |
