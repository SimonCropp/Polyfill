### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       216.5KB |  +208.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       172.0KB |  +163.5KB |    +8.5KB |             +6.0KB |              +8.5KB |     +13.5KB |
| net461         |          8.5KB |       222.5KB |  +214.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net462         |          7.0KB |       221.5KB |  +214.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       221.5KB |  +214.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       221.5KB |  +213.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       220.0KB |  +211.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       220.0KB |  +211.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       220.0KB |  +211.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       200.0KB |  +191.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       182.0KB |  +173.0KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| netcoreapp2.2  |          9.0KB |       182.0KB |  +173.0KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| netcoreapp3.0  |          9.5KB |       177.0KB |  +167.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       175.5KB |  +166.0KB |    +9.0KB |             +6.0KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       145.5KB |  +136.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       106.5KB |   +96.5KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |        80.0KB |   +70.0KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        63.0KB |   +53.5KB |    +9.0KB |          +512bytes |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        29.0KB |   +19.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        16.5KB |    +6.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       328.0KB |  +320.0KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| netstandard2.1 |          8.5KB |       259.8KB |  +251.3KB |   +16.4KB |             +7.7KB |             +13.5KB |     +19.2KB |
| net461         |          8.5KB |       334.5KB |  +326.0KB |   +17.4KB |             +8.7KB |             +14.5KB |     +19.7KB |
| net462         |          7.0KB |       333.5KB |  +326.5KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net47          |          7.0KB |       333.2KB |  +326.2KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net471         |          8.5KB |       333.2KB |  +324.7KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net472         |          8.5KB |       330.6KB |  +322.1KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net48          |          8.5KB |       330.6KB |  +322.1KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net481         |          8.5KB |       330.6KB |  +322.1KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| netcoreapp2.0  |          9.0KB |       302.3KB |  +293.3KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| netcoreapp2.1  |          9.0KB |       274.8KB |  +265.8KB |   +19.9KB |            +11.2KB |             +17.0KB |     +22.7KB |
| netcoreapp2.2  |          9.0KB |       274.8KB |  +265.8KB |   +19.9KB |            +11.2KB |             +17.0KB |     +22.7KB |
| netcoreapp3.0  |          9.5KB |       260.9KB |  +251.4KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| netcoreapp3.1  |          9.5KB |       259.4KB |  +249.9KB |   +16.9KB |             +7.7KB |             +14.0KB |     +19.7KB |
| net5.0         |          9.5KB |       213.1KB |  +203.6KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net6.0         |         10.0KB |       159.4KB |  +149.4KB |   +17.9KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       116.6KB |  +106.6KB |   +17.3KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |        90.2KB |   +80.7KB |   +16.6KB |          +811bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        42.1KB |   +32.6KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        24.6KB |   +14.6KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
