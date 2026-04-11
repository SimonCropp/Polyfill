### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       299.0KB |  +291.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       251.0KB |  +242.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net461         |          8.5KB |       296.5KB |  +288.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net462         |          7.0KB |       300.0KB |  +293.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net47          |          7.0KB |       300.0KB |  +293.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       300.0KB |  +291.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net472         |          8.5KB |       298.5KB |  +290.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net48          |          8.5KB |       298.5KB |  +290.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net481         |          8.5KB |       298.5KB |  +290.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       275.0KB |  +266.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       254.0KB |  +245.0KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       254.0KB |  +245.0KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       245.0KB |  +235.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       243.5KB |  +234.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       208.0KB |  +198.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       152.0KB |  +142.0KB |   +10.0KB |             +7.0KB |           +512bytes |      +4.0KB |
| net7.0         |         10.0KB |       117.5KB |  +107.5KB |    +9.0KB |             +5.5KB |           +512bytes |      +4.0KB |
| net8.0         |          9.5KB |        89.0KB |   +79.5KB |    +9.0KB |          +512bytes |              +1.0KB |      +3.5KB |
| net9.0         |          9.5KB |        47.0KB |   +37.5KB |    +9.0KB |                    |              +1.0KB |      +3.5KB |
| net10.0        |         10.0KB |        23.5KB |   +13.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        18.5KB |    +8.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       436.4KB |  +428.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       362.9KB |  +354.4KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net461         |          8.5KB |       434.3KB |  +425.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net462         |          7.0KB |       437.8KB |  +430.8KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net47          |          7.0KB |       437.6KB |  +430.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       437.6KB |  +429.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net472         |          8.5KB |       435.0KB |  +426.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net48          |          8.5KB |       435.0KB |  +426.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net481         |          8.5KB |       435.0KB |  +426.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.0  |          9.0KB |       402.3KB |  +393.3KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       370.1KB |  +361.1KB |   +16.7KB |             +8.7KB |             +14.4KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       370.1KB |  +361.1KB |   +16.7KB |             +8.7KB |             +14.4KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       351.8KB |  +342.3KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       350.3KB |  +340.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       297.2KB |  +287.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       223.4KB |  +213.4KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.7KB |
| net7.0         |         10.0KB |       170.2KB |  +160.2KB |   +16.6KB |             +6.9KB |              +1.1KB |      +4.7KB |
| net8.0         |          9.5KB |       126.8KB |  +117.3KB |   +16.5KB |          +811bytes |              +1.6KB |      +4.2KB |
| net9.0         |          9.5KB |        67.8KB |   +58.3KB |   +16.5KB |                    |              +1.6KB |      +4.2KB |
| net10.0        |         10.0KB |        35.8KB |   +25.8KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        27.4KB |   +17.4KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
