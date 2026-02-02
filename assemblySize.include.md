### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       216.0KB |  +208.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       172.0KB |  +163.5KB |    +8.5KB |             +6.0KB |              +8.5KB |     +13.5KB |
| net461         |          8.5KB |       222.5KB |  +214.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       221.0KB |  +214.0KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| net47          |          7.0KB |       221.0KB |  +214.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       221.0KB |  +212.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net472         |          8.5KB |       219.5KB |  +211.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net48          |          8.5KB |       219.5KB |  +211.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net481         |          8.5KB |       219.5KB |  +211.0KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       199.5KB |  +190.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       182.0KB |  +173.0KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| netcoreapp2.2  |          9.0KB |       182.0KB |  +173.0KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| netcoreapp3.0  |          9.5KB |       177.5KB |  +168.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       176.0KB |  +166.5KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       146.0KB |  +136.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       107.5KB |   +97.5KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |        81.0KB |   +71.0KB |    +9.5KB |             +6.0KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        64.5KB |   +55.0KB |    +8.5KB |             +1.0KB |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        30.5KB |   +21.0KB |    +9.0KB |             +1.5KB |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        18.5KB |    +8.5KB |    +9.0KB |             +1.5KB |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       327.5KB |  +319.5KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| netstandard2.1 |          8.5KB |       259.9KB |  +251.4KB |   +16.4KB |             +7.7KB |             +13.5KB |     +19.2KB |
| net461         |          8.5KB |       334.4KB |  +325.9KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net462         |          7.0KB |       332.9KB |  +325.9KB |   +16.9KB |             +8.7KB |             +14.0KB |     +19.7KB |
| net47          |          7.0KB |       332.7KB |  +325.7KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net471         |          8.5KB |       332.7KB |  +324.2KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net472         |          8.5KB |       330.1KB |  +321.6KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net48          |          8.5KB |       330.1KB |  +321.6KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net481         |          8.5KB |       330.1KB |  +321.6KB |   +16.9KB |             +8.7KB |             +14.0KB |     +19.7KB |
| netcoreapp2.0  |          9.0KB |       301.8KB |  +292.8KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| netcoreapp2.1  |          9.0KB |       275.0KB |  +266.0KB |   +19.9KB |            +11.2KB |             +17.0KB |     +22.7KB |
| netcoreapp2.2  |          9.0KB |       275.0KB |  +266.0KB |   +19.9KB |            +11.2KB |             +17.0KB |     +22.7KB |
| netcoreapp3.0  |          9.5KB |       261.9KB |  +252.4KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| netcoreapp3.1  |          9.5KB |       260.4KB |  +250.9KB |   +16.9KB |             +7.7KB |             +14.0KB |     +19.2KB |
| net5.0         |          9.5KB |       214.1KB |  +204.6KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net6.0         |         10.0KB |       160.9KB |  +150.9KB |   +17.4KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       118.6KB |  +108.6KB |   +17.3KB |             +7.6KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |        92.6KB |   +83.1KB |   +16.1KB |             +1.9KB |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        44.9KB |   +35.4KB |   +16.6KB |             +2.3KB |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        28.2KB |   +18.2KB |   +16.6KB |             +2.3KB |              +1.1KB |      +4.2KB |
