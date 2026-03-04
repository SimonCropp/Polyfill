### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       248.0KB |  +240.0KB |    +8.5KB |             +6.0KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       201.5KB |  +193.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       254.0KB |  +245.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       253.0KB |  +246.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       252.5KB |  +245.5KB |    +9.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       253.0KB |  +244.5KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       251.5KB |  +243.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       251.5KB |  +243.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       251.5KB |  +243.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       227.0KB |  +218.0KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       209.0KB |  +200.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       209.0KB |  +200.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       199.5KB |  +190.0KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       198.0KB |  +188.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       162.0KB |  +152.5KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       117.5KB |  +107.5KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |        87.0KB |   +77.0KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        70.5KB |   +61.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |         10.0KB |        36.5KB |   +26.5KB |    +9.0KB |                    |              +1.0KB |      +3.5KB |
| net10.0        |         10.0KB |        23.0KB |   +13.0KB |    +9.0KB |                    |              +1.0KB |      +4.0KB |
| net11.0        |         10.0KB |        19.0KB |    +9.0KB |    +9.5KB |                    |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       367.8KB |  +359.8KB |   +16.2KB |             +7.7KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       296.5KB |  +288.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       374.2KB |  +365.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       373.2KB |  +366.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       372.5KB |  +365.5KB |   +17.2KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       373.0KB |  +364.5KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       370.4KB |  +361.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       370.4KB |  +361.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       370.4KB |  +361.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       336.7KB |  +327.7KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       308.2KB |  +299.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       308.2KB |  +299.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       289.6KB |  +280.1KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       288.0KB |  +278.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       234.9KB |  +225.4KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net6.0         |         10.0KB |       173.8KB |  +163.8KB |   +17.2KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       126.4KB |  +116.4KB |   +17.1KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       100.9KB |   +91.4KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |         10.0KB |        53.4KB |   +43.4KB |   +16.5KB |                    |              +1.6KB |      +4.2KB |
| net10.0        |         10.0KB |        35.2KB |   +25.2KB |   +16.5KB |                    |              +1.6KB |      +4.7KB |
| net11.0        |         10.0KB |        28.5KB |   +18.5KB |   +17.0KB |                    |              +1.6KB |      +4.7KB |
