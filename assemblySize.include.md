### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       246.0KB |  +238.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       200.5KB |  +192.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       252.5KB |  +244.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       251.0KB |  +244.0KB |    +9.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net47          |          7.0KB |       251.0KB |  +244.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       251.0KB |  +242.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       249.5KB |  +241.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +13.5KB |
| net48          |          8.5KB |       249.5KB |  +241.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       249.5KB |  +241.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       226.5KB |  +217.5KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       208.0KB |  +199.0KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       208.0KB |  +199.0KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       199.0KB |  +189.5KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       197.0KB |  +187.5KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net5.0         |          9.5KB |       161.5KB |  +152.0KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| net6.0         |         10.0KB |       116.5KB |  +106.5KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |        86.0KB |   +76.0KB |    +9.5KB |             +6.0KB |              +1.0KB |      +3.5KB |
| net8.0         |          9.5KB |        69.5KB |   +60.0KB |    +8.5KB |          +512bytes |           +512bytes |      +3.5KB |
| net9.0         |         10.0KB |        35.5KB |   +25.5KB |    +9.0KB |                    |              +1.0KB |      +4.0KB |
| net10.0        |         10.0KB |        22.5KB |   +12.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        19.0KB |    +9.0KB |    +9.5KB |                    |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       364.9KB |  +356.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       294.8KB |  +286.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       371.8KB |  +363.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       370.3KB |  +363.3KB |   +17.2KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net47          |          7.0KB |       370.1KB |  +363.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       370.1KB |  +361.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       367.5KB |  +359.0KB |   +16.7KB |             +8.2KB |             +14.4KB |     +18.9KB |
| net48          |          8.5KB |       367.5KB |  +359.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       367.5KB |  +359.0KB |   +16.7KB |             +8.2KB |             +14.4KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       335.5KB |  +326.5KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       306.5KB |  +297.5KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       306.5KB |  +297.5KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       288.4KB |  +278.9KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       286.4KB |  +276.9KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net5.0         |          9.5KB |       233.8KB |  +224.3KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| net6.0         |         10.0KB |       171.9KB |  +161.9KB |   +17.2KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       124.6KB |  +114.6KB |   +17.1KB |             +7.4KB |              +1.6KB |      +4.2KB |
| net8.0         |          9.5KB |        99.1KB |   +89.6KB |   +16.0KB |          +811bytes |              +1.1KB |      +4.2KB |
| net9.0         |         10.0KB |        51.6KB |   +41.6KB |   +16.5KB |                    |              +1.6KB |      +4.7KB |
| net10.0        |         10.0KB |        33.9KB |   +23.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        28.5KB |   +18.5KB |   +17.0KB |                    |              +1.6KB |      +4.7KB |
