### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       219.0KB |  +211.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       174.0KB |  +165.5KB |    +9.0KB |             +9.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       225.5KB |  +217.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       224.0KB |  +217.0KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| net47          |          7.0KB |       224.0KB |  +217.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       224.0KB |  +215.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       222.5KB |  +214.0KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| net48          |          8.5KB |       222.5KB |  +214.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       222.5KB |  +214.0KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       202.5KB |  +193.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       184.5KB |  +175.5KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| netcoreapp2.2  |          9.0KB |       184.5KB |  +175.5KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| netcoreapp3.0  |          9.5KB |       179.5KB |  +170.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       178.0KB |  +168.5KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       147.5KB |  +138.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       108.5KB |   +98.5KB |   +10.0KB |             +7.0KB |              +1.0KB |      +3.5KB |
| net7.0         |         10.0KB |        82.0KB |   +72.0KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        65.0KB |   +55.5KB |    +8.5KB |          +512bytes |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        31.0KB |   +21.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        18.5KB |    +8.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        16.5KB |    +6.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       331.5KB |  +323.5KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| netstandard2.1 |          8.5KB |       262.8KB |  +254.3KB |   +16.9KB |            +11.2KB |             +14.0KB |     +19.7KB |
| net461         |          8.5KB |       338.5KB |  +330.0KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net462         |          7.0KB |       337.0KB |  +330.0KB |   +16.9KB |             +8.7KB |             +14.0KB |     +19.7KB |
| net47          |          7.0KB |       336.7KB |  +329.7KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net471         |          8.5KB |       336.7KB |  +328.2KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net472         |          8.5KB |       334.2KB |  +325.7KB |   +16.9KB |             +8.7KB |             +14.0KB |     +19.7KB |
| net48          |          8.5KB |       334.2KB |  +325.7KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net481         |          8.5KB |       334.2KB |  +325.7KB |   +16.9KB |             +8.7KB |             +14.0KB |     +19.7KB |
| netcoreapp2.0  |          9.0KB |       305.8KB |  +296.8KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| netcoreapp2.1  |          9.0KB |       278.3KB |  +269.3KB |   +19.9KB |            +11.2KB |             +17.0KB |     +22.7KB |
| netcoreapp2.2  |          9.0KB |       278.3KB |  +269.3KB |   +19.9KB |            +11.2KB |             +17.0KB |     +22.7KB |
| netcoreapp3.0  |          9.5KB |       264.4KB |  +254.9KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| netcoreapp3.1  |          9.5KB |       262.9KB |  +253.4KB |   +16.9KB |             +7.7KB |             +14.0KB |     +19.2KB |
| net5.0         |          9.5KB |       216.1KB |  +206.6KB |   +16.9KB |             +8.2KB |             +14.5KB |     +19.7KB |
| net6.0         |         10.0KB |       162.4KB |  +152.4KB |   +17.9KB |             +8.7KB |              +1.6KB |      +4.2KB |
| net7.0         |         10.0KB |       119.6KB |  +109.6KB |   +17.3KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |        93.2KB |   +83.7KB |   +16.1KB |          +811bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        45.2KB |   +35.7KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        27.6KB |   +17.6KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        24.6KB |   +14.6KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
