### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       307.5KB |  +299.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       260.5KB |  +252.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       306.0KB |  +297.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net462         |          7.0KB |       309.5KB |  +302.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net47          |          7.0KB |       309.5KB |  +302.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       308.5KB |  +300.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net472         |          8.5KB |       307.0KB |  +298.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net48          |          8.5KB |       307.0KB |  +298.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net481         |          8.5KB |       307.0KB |  +298.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       283.5KB |  +274.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       263.0KB |  +254.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       263.0KB |  +254.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       254.5KB |  +245.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       252.5KB |  +243.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       215.5KB |  +206.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net6.0         |         10.0KB |       154.5KB |  +144.5KB |    +9.5KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       119.5KB |  +109.5KB |    +9.0KB |             +5.0KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        91.0KB |   +81.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        49.0KB |   +39.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        25.5KB |   +15.5KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net11.0        |         10.0KB |        20.0KB |   +10.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       448.1KB |  +440.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       375.9KB |  +367.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       447.6KB |  +439.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net462         |          7.0KB |       451.1KB |  +444.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net47          |          7.0KB |       450.9KB |  +443.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       449.5KB |  +441.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net472         |          8.5KB |       446.9KB |  +438.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net48          |          8.5KB |       446.9KB |  +438.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net481         |          8.5KB |       446.9KB |  +438.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.0  |          9.0KB |       414.2KB |  +405.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       382.6KB |  +373.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       382.6KB |  +373.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       364.8KB |  +355.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       362.7KB |  +353.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       307.6KB |  +298.1KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net6.0         |         10.0KB |       227.0KB |  +217.0KB |   +17.2KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       173.2KB |  +163.2KB |   +16.6KB |             +6.4KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       129.8KB |  +120.3KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        70.9KB |   +61.4KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        38.8KB |   +28.8KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net11.0        |         10.0KB |        29.7KB |   +19.7KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
