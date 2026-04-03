### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       259.5KB |  +251.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       212.5KB |  +204.0KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net461         |          8.5KB |       268.0KB |  +259.5KB |    +9.0KB |             +6.0KB |              +8.5KB |     +13.5KB |
| net462         |          7.0KB |       266.5KB |  +259.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       266.5KB |  +259.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       266.5KB |  +258.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       265.0KB |  +256.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       265.0KB |  +256.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       265.0KB |  +256.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       241.0KB |  +232.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       221.5KB |  +212.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       221.5KB |  +212.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       212.5KB |  +203.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       210.5KB |  +201.0KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net5.0         |          9.5KB |       173.5KB |  +164.0KB |    +9.0KB |             +6.0KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       128.5KB |  +118.5KB |   +10.0KB |             +6.5KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |        98.5KB |   +88.5KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        80.0KB |   +70.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        45.0KB |   +35.5KB |    +9.0KB |                    |              +1.0KB |      +3.5KB |
| net10.0        |         10.0KB |        23.5KB |   +13.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        18.0KB |    +8.0KB |    +9.5KB |                    |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       384.4KB |  +376.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       312.2KB |  +303.7KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net461         |          8.5KB |       393.3KB |  +384.8KB |   +16.7KB |             +7.7KB |             +13.4KB |     +18.9KB |
| net462         |          7.0KB |       391.8KB |  +384.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       391.6KB |  +384.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       391.6KB |  +383.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       389.0KB |  +380.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       389.0KB |  +380.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       389.0KB |  +380.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       355.7KB |  +346.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       325.4KB |  +316.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       325.4KB |  +316.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       307.2KB |  +297.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       305.2KB |  +295.7KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net5.0         |          9.5KB |       250.9KB |  +241.4KB |   +16.7KB |             +7.7KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       189.4KB |  +179.4KB |   +17.7KB |             +8.2KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       142.4KB |  +132.4KB |   +17.1KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       114.0KB |  +104.5KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        65.0KB |   +55.5KB |   +16.5KB |                    |              +1.6KB |      +4.2KB |
| net10.0        |         10.0KB |        35.7KB |   +25.7KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        26.9KB |   +16.9KB |   +17.0KB |                    |              +1.6KB |      +4.7KB |
