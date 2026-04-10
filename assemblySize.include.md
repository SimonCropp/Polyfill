### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       274.0KB |  +266.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       226.5KB |  +218.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       282.5KB |  +274.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       281.5KB |  +274.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       281.0KB |  +274.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       281.0KB |  +272.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       279.5KB |  +271.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       279.5KB |  +271.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       279.5KB |  +271.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       256.0KB |  +247.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       235.0KB |  +226.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       235.0KB |  +226.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       226.5KB |  +217.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       224.5KB |  +215.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       190.0KB |  +180.5KB |    +9.5KB |             +7.0KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       141.5KB |  +131.5KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       109.0KB |   +99.0KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        88.5KB |   +79.0KB |    +8.5KB |          +512bytes |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        46.5KB |   +37.0KB |    +9.0KB |                    |              +1.0KB |      +3.5KB |
| net10.0        |         10.0KB |        23.5KB |   +13.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        18.5KB |    +8.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       405.0KB |  +397.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       332.2KB |  +323.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       414.0KB |  +405.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       413.0KB |  +406.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       412.2KB |  +405.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       412.2KB |  +403.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       409.7KB |  +401.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       409.7KB |  +401.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       409.7KB |  +401.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       376.9KB |  +367.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       344.9KB |  +335.9KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       344.9KB |  +335.9KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       327.3KB |  +317.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       325.2KB |  +315.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       273.4KB |  +263.9KB |   +17.2KB |             +8.7KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       208.2KB |  +198.2KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       157.5KB |  +147.5KB |   +16.6KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       126.0KB |  +116.5KB |   +16.0KB |          +811bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        67.0KB |   +57.5KB |   +16.5KB |                    |              +1.6KB |      +4.2KB |
| net10.0        |         10.0KB |        35.8KB |   +25.8KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        27.4KB |   +17.4KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
