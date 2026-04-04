### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       265.5KB |  +257.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       218.0KB |  +209.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       274.0KB |  +265.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       273.0KB |  +266.0KB |    +9.0KB |             +6.0KB |              +8.5KB |     +13.5KB |
| net47          |          7.0KB |       272.5KB |  +265.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       272.5KB |  +264.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       271.5KB |  +263.0KB |    +8.5KB |             +6.0KB |              +9.0KB |     +13.0KB |
| net48          |          8.5KB |       271.0KB |  +262.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       271.5KB |  +263.0KB |    +8.5KB |             +6.0KB |              +9.0KB |     +13.0KB |
| netcoreapp2.0  |          9.0KB |       247.5KB |  +238.5KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       227.0KB |  +218.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       227.0KB |  +218.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       217.5KB |  +208.0KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       216.0KB |  +206.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       178.5KB |  +169.0KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| net6.0         |         10.0KB |       134.0KB |  +124.0KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |       103.0KB |   +93.0KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        82.5KB |   +73.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        46.5KB |   +37.0KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net10.0        |         10.0KB |        23.5KB |   +13.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        18.0KB |    +8.0KB |    +9.5KB |                    |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       392.9KB |  +384.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       320.0KB |  +311.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       401.8KB |  +393.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       400.8KB |  +393.8KB |   +16.7KB |             +7.7KB |             +13.4KB |     +18.9KB |
| net47          |          7.0KB |       400.1KB |  +393.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       400.1KB |  +391.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       398.0KB |  +389.5KB |   +16.2KB |             +7.7KB |             +13.9KB |     +18.4KB |
| net48          |          8.5KB |       397.5KB |  +389.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       398.0KB |  +389.5KB |   +16.2KB |             +7.7KB |             +13.9KB |     +18.4KB |
| netcoreapp2.0  |          9.0KB |       364.8KB |  +355.8KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       333.2KB |  +324.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       333.2KB |  +324.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       314.5KB |  +305.0KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       313.0KB |  +303.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       258.2KB |  +248.7KB |   +19.7KB |            +11.2KB |             +16.9KB |     +22.4KB |
| net6.0         |         10.0KB |       197.2KB |  +187.2KB |   +17.2KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       148.9KB |  +138.9KB |   +16.6KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       117.4KB |  +107.9KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        66.9KB |   +57.4KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net10.0        |         10.0KB |        35.7KB |   +25.7KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        26.9KB |   +16.9KB |   +17.0KB |                    |              +1.6KB |      +4.7KB |
