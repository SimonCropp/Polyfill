### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       371.0KB |  +363.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       326.5KB |  +318.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       370.0KB |  +361.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.0KB |
| net462         |          7.0KB |       375.0KB |  +368.0KB |    +7.0KB |             +6.5KB |              +7.5KB |     +11.5KB |
| net47          |          7.0KB |       374.5KB |  +367.5KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net471         |          8.5KB |       372.5KB |  +364.0KB |    +8.5KB |             +6.0KB |              +8.5KB |     +13.0KB |
| net472         |          8.5KB |       371.0KB |  +362.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       371.0KB |  +362.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       371.0KB |  +362.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       350.0KB |  +341.0KB |    +7.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       330.0KB |  +321.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       330.0KB |  +321.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       326.0KB |  +316.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       324.0KB |  +314.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net5.0         |          9.5KB |       288.0KB |  +278.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       230.0KB |  +220.0KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |       196.0KB |  +186.0KB |    +9.5KB |             +6.0KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       166.0KB |  +156.5KB |    +8.5KB |          +512bytes |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |       115.5KB |  +106.0KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net10.0        |         10.0KB |        93.5KB |   +83.5KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net11.0        |         10.0KB |        21.0KB |   +11.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       544.1KB |  +536.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       473.0KB |  +464.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       544.1KB |  +535.6KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.4KB |
| net462         |          7.0KB |       549.1KB |  +542.1KB |   +14.7KB |             +8.2KB |             +12.4KB |     +16.9KB |
| net47          |          7.0KB |       548.4KB |  +541.4KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net471         |          8.5KB |       546.0KB |  +537.5KB |   +16.2KB |             +7.7KB |             +13.4KB |     +18.4KB |
| net472         |          8.5KB |       543.4KB |  +534.9KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       543.4KB |  +534.9KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       543.4KB |  +534.9KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       512.5KB |  +503.5KB |   +15.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       480.2KB |  +471.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       480.2KB |  +471.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       469.5KB |  +460.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       467.5KB |  +458.0KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net5.0         |          9.5KB |       413.4KB |  +403.9KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net6.0         |         10.0KB |       335.4KB |  +325.4KB |   +17.2KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       283.6KB |  +273.6KB |   +17.1KB |             +7.4KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       239.8KB |  +230.3KB |   +16.0KB |          +811bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |       166.8KB |  +157.3KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net10.0        |         10.0KB |       136.1KB |  +126.1KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net11.0        |         10.0KB |        30.9KB |   +20.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
