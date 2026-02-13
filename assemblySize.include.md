### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       219.5KB |  +211.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       174.5KB |  +166.0KB |    +9.0KB |             +9.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       225.5KB |  +217.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net462         |          7.0KB |       224.5KB |  +217.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       224.5KB |  +217.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       224.5KB |  +216.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       223.0KB |  +214.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       223.0KB |  +214.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       223.0KB |  +214.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       202.5KB |  +193.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       184.5KB |  +175.5KB |   +12.0KB |            +10.0KB |             +12.5KB |     +17.0KB |
| netcoreapp2.2  |          9.0KB |       184.5KB |  +175.5KB |   +12.5KB |            +10.0KB |             +12.5KB |     +17.0KB |
| netcoreapp3.0  |          9.5KB |       180.0KB |  +170.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       178.0KB |  +168.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net5.0         |          9.5KB |       148.0KB |  +138.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       109.0KB |   +99.0KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |        82.0KB |   +72.0KB |    +9.5KB |             +6.0KB |              +1.0KB |      +3.5KB |
| net8.0         |          9.5KB |        65.5KB |   +56.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        31.5KB |   +22.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        19.0KB |    +9.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        16.5KB |    +6.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       332.2KB |  +324.2KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| netstandard2.1 |          8.5KB |       263.5KB |  +255.0KB |   +16.9KB |            +11.2KB |             +14.0KB |     +19.7KB |
| net461         |          8.5KB |       338.7KB |  +330.2KB |   +17.4KB |             +8.7KB |             +14.5KB |     +19.7KB |
| net462         |          7.0KB |       337.7KB |  +330.7KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net47          |          7.0KB |       337.4KB |  +330.4KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net471         |          8.5KB |       337.4KB |  +328.9KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net472         |          8.5KB |       334.9KB |  +326.4KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net48          |          8.5KB |       334.9KB |  +326.4KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net481         |          8.5KB |       334.9KB |  +326.4KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| netcoreapp2.0  |          9.0KB |       306.0KB |  +297.0KB |   +16.9KB |             +8.7KB |             +14.5KB |     +19.7KB |
| netcoreapp2.1  |          9.0KB |       278.6KB |  +269.6KB |   +19.9KB |            +11.7KB |             +17.5KB |     +22.7KB |
| netcoreapp2.2  |          9.0KB |       278.6KB |  +269.6KB |   +20.4KB |            +11.7KB |             +17.5KB |     +22.7KB |
| netcoreapp3.0  |          9.5KB |       265.1KB |  +255.6KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| netcoreapp3.1  |          9.5KB |       263.1KB |  +253.6KB |   +16.9KB |             +8.2KB |             +14.5KB |     +19.7KB |
| net5.0         |          9.5KB |       216.8KB |  +207.3KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net6.0         |         10.0KB |       163.1KB |  +153.1KB |   +17.9KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       119.8KB |  +109.8KB |   +17.3KB |             +7.4KB |              +1.6KB |      +4.2KB |
| net8.0         |          9.5KB |        93.9KB |   +84.4KB |   +16.1KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        46.1KB |   +36.6KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        28.6KB |   +18.6KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        24.6KB |   +14.6KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
