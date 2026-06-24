### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       337.5KB |  +329.5KB |    +8.5KB |             +6.0KB |              +8.5KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       291.0KB |  +282.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       336.0KB |  +327.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       339.5KB |  +332.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       339.5KB |  +332.5KB |    +8.5KB |             +6.0KB |              +8.5KB |     +13.5KB |
| net471         |          8.5KB |       338.5KB |  +330.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       337.0KB |  +328.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       337.0KB |  +328.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       337.0KB |  +328.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       313.5KB |  +304.5KB |    +8.5KB |             +6.0KB |              +8.5KB |     +13.0KB |
| netcoreapp2.1  |          9.0KB |       293.0KB |  +284.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       293.0KB |  +284.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       285.5KB |  +276.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       284.0KB |  +274.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       247.5KB |  +238.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       188.5KB |  +178.5KB |   +10.0KB |             +7.0KB |              +1.0KB |      +3.5KB |
| net7.0         |         10.0KB |       151.0KB |  +141.0KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       122.5KB |  +113.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        79.5KB |   +70.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        57.0KB |   +47.0KB |    +9.0KB |                    |              +1.0KB |      +3.5KB |
| net11.0        |         10.0KB |        20.5KB |   +10.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       491.3KB |  +483.3KB |   +16.2KB |             +7.7KB |             +13.4KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       419.5KB |  +411.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       490.8KB |  +482.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       494.3KB |  +487.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       494.1KB |  +487.1KB |   +16.2KB |             +7.7KB |             +13.4KB |     +18.9KB |
| net471         |          8.5KB |       492.7KB |  +484.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       490.2KB |  +481.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       490.2KB |  +481.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       490.2KB |  +481.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       456.7KB |  +447.7KB |   +16.2KB |             +7.7KB |             +13.4KB |     +18.4KB |
| netcoreapp2.1  |          9.0KB |       425.2KB |  +416.2KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       425.2KB |  +416.2KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       408.9KB |  +399.4KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       407.4KB |  +397.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       352.7KB |  +343.2KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net6.0         |         10.0KB |       273.7KB |  +263.7KB |   +17.7KB |             +8.7KB |              +1.6KB |      +4.2KB |
| net7.0         |         10.0KB |       217.6KB |  +207.6KB |   +16.6KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       174.1KB |  +164.6KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |       110.7KB |  +101.2KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        79.9KB |   +69.9KB |   +16.5KB |                    |              +1.6KB |      +4.2KB |
| net11.0        |         10.0KB |        30.3KB |   +20.3KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
