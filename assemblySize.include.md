### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       221.0KB |  +213.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       176.0KB |  +167.5KB |   +12.0KB |             +9.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       227.5KB |  +219.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       226.0KB |  +219.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net47          |          7.0KB |       226.0KB |  +219.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       226.0KB |  +217.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       224.5KB |  +216.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net48          |          8.5KB |       224.5KB |  +216.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net481         |          8.5KB |       224.5KB |  +216.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       204.0KB |  +195.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       186.5KB |  +177.5KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| netcoreapp2.2  |          9.0KB |       186.5KB |  +177.5KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| netcoreapp3.0  |          9.5KB |       181.5KB |  +172.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       180.0KB |  +170.5KB |    +8.5KB |             +6.0KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       149.5KB |  +140.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       110.0KB |  +100.0KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |        83.5KB |   +73.5KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        66.5KB |   +57.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        32.5KB |   +23.0KB |    +9.0KB |                    |              +1.0KB |      +3.5KB |
| net10.0        |         10.0KB |        20.5KB |   +10.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        16.5KB |    +6.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       334.7KB |  +326.7KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| netstandard2.1 |          8.5KB |       265.9KB |  +257.4KB |   +19.9KB |            +11.2KB |             +14.0KB |     +19.7KB |
| net461         |          8.5KB |       341.6KB |  +333.1KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net462         |          7.0KB |       340.1KB |  +333.1KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net47          |          7.0KB |       339.9KB |  +332.9KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net471         |          8.5KB |       339.9KB |  +331.4KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net472         |          8.5KB |       337.3KB |  +328.8KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net48          |          8.5KB |       337.3KB |  +328.8KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net481         |          8.5KB |       337.3KB |  +328.8KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| netcoreapp2.0  |          9.0KB |       308.5KB |  +299.5KB |   +17.4KB |             +8.7KB |             +14.5KB |     +19.7KB |
| netcoreapp2.1  |          9.0KB |       281.5KB |  +272.5KB |   +19.9KB |            +11.2KB |             +17.0KB |     +22.7KB |
| netcoreapp2.2  |          9.0KB |       281.5KB |  +272.5KB |   +19.9KB |            +11.2KB |             +17.0KB |     +22.7KB |
| netcoreapp3.0  |          9.5KB |       267.6KB |  +258.1KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| netcoreapp3.1  |          9.5KB |       266.0KB |  +256.5KB |   +16.4KB |             +7.7KB |             +14.0KB |     +19.2KB |
| net5.0         |          9.5KB |       219.2KB |  +209.7KB |   +16.9KB |             +8.2KB |             +14.5KB |     +19.7KB |
| net6.0         |         10.0KB |       164.8KB |  +154.8KB |   +17.9KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       122.0KB |  +112.0KB |   +17.3KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |        95.6KB |   +86.1KB |   +16.1KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        47.8KB |   +38.3KB |   +16.6KB |                    |              +1.6KB |      +4.2KB |
| net10.0        |         10.0KB |        31.1KB |   +21.1KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        24.6KB |   +14.6KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
