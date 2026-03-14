### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       259.5KB |  +251.5KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       212.5KB |  +204.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       266.0KB |  +257.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       265.0KB |  +258.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       264.5KB |  +257.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       264.5KB |  +256.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       263.5KB |  +255.0KB |    +8.5KB |             +6.0KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       263.0KB |  +254.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +13.5KB |
| net481         |          8.5KB |       263.5KB |  +255.0KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       239.0KB |  +230.0KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       220.5KB |  +211.5KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       220.5KB |  +211.5KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       211.0KB |  +201.5KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       209.5KB |  +200.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       173.0KB |  +163.5KB |    +9.0KB |             +6.0KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       128.0KB |  +118.0KB |   +10.0KB |             +6.5KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |        98.0KB |   +88.0KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        79.5KB |   +70.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        45.0KB |   +35.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        23.5KB |   +13.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        19.0KB |    +9.0KB |    +9.5KB |                    |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       383.6KB |  +375.6KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       311.6KB |  +303.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       390.6KB |  +382.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       389.6KB |  +382.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       388.8KB |  +381.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       388.8KB |  +380.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       386.8KB |  +378.3KB |   +16.2KB |             +7.7KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       386.3KB |  +377.8KB |   +16.7KB |             +8.2KB |             +14.4KB |     +18.9KB |
| net481         |          8.5KB |       386.8KB |  +378.3KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       353.0KB |  +344.0KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       323.8KB |  +314.8KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       323.8KB |  +314.8KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       305.1KB |  +295.6KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       303.6KB |  +294.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       249.9KB |  +240.4KB |   +16.7KB |             +7.7KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       188.2KB |  +178.2KB |   +17.7KB |             +8.2KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       141.3KB |  +131.3KB |   +17.1KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       112.8KB |  +103.3KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        64.6KB |   +55.1KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        35.8KB |   +25.8KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        28.5KB |   +18.5KB |   +17.0KB |                    |              +1.6KB |      +4.7KB |
