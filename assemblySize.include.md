### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       249.0KB |  +241.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       202.0KB |  +193.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       255.5KB |  +247.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       254.5KB |  +247.5KB |    +9.0KB |             +6.0KB |              +8.5KB |     +13.5KB |
| net47          |          7.0KB |       254.0KB |  +247.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       254.0KB |  +245.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       252.5KB |  +244.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net48          |          8.5KB |       252.5KB |  +244.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       253.0KB |  +244.5KB |    +8.5KB |             +6.0KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       228.5KB |  +219.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       209.5KB |  +200.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       209.5KB |  +200.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       200.0KB |  +190.5KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       198.5KB |  +189.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       162.5KB |  +153.0KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       118.0KB |  +108.0KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |        87.5KB |   +77.5KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        71.0KB |   +61.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        37.5KB |   +28.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        23.5KB |   +13.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        19.0KB |    +9.0KB |    +9.5KB |                    |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       369.2KB |  +361.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       297.1KB |  +288.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       376.2KB |  +367.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       375.2KB |  +368.2KB |   +16.7KB |             +7.7KB |             +13.4KB |     +18.9KB |
| net47          |          7.0KB |       374.4KB |  +367.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       374.4KB |  +365.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       371.9KB |  +363.4KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net48          |          8.5KB |       371.9KB |  +363.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       372.4KB |  +363.9KB |   +16.2KB |             +7.7KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       338.6KB |  +329.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       308.8KB |  +299.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       308.8KB |  +299.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       290.2KB |  +280.7KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       288.7KB |  +279.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       235.6KB |  +226.1KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net6.0         |         10.0KB |       174.4KB |  +164.4KB |   +17.2KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       127.0KB |  +117.0KB |   +17.1KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       101.5KB |   +92.0KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        54.9KB |   +45.4KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        35.8KB |   +25.8KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        28.5KB |   +18.5KB |   +17.0KB |                    |              +1.6KB |      +4.7KB |
