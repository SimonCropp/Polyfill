### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       229.0KB |  +221.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       181.0KB |  +172.5KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| net461         |          8.5KB |       235.5KB |  +227.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net462         |          7.0KB |       234.5KB |  +227.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       234.0KB |  +227.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net471         |          8.5KB |       234.0KB |  +225.5KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net472         |          8.5KB |       233.0KB |  +224.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       233.0KB |  +224.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       233.0KB |  +224.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       209.5KB |  +200.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       191.0KB |  +182.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.5KB |
| netcoreapp2.2  |          9.0KB |       191.5KB |  +182.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       185.0KB |  +175.5KB |   +12.0KB |             +9.5KB |             +12.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       183.0KB |  +173.5KB |   +12.0KB |             +9.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       153.0KB |  +143.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       113.5KB |  +103.5KB |    +9.5KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |        86.0KB |   +76.0KB |    +9.5KB |             +6.0KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        69.5KB |   +60.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |         10.0KB |        35.5KB |   +25.5KB |    +9.0KB |                    |              +1.0KB |      +3.5KB |
| net10.0        |         10.0KB |        22.5KB |   +12.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        19.0KB |    +9.0KB |    +9.0KB |                    |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       343.0KB |  +335.0KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       270.9KB |  +262.4KB |   +19.7KB |            +11.2KB |             +16.9KB |     +22.4KB |
| net461         |          8.5KB |       350.0KB |  +341.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net462         |          7.0KB |       349.0KB |  +342.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       348.2KB |  +341.2KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net471         |          8.5KB |       348.2KB |  +339.7KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net472         |          8.5KB |       346.2KB |  +337.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       346.2KB |  +337.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       346.2KB |  +337.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       313.7KB |  +304.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       285.1KB |  +276.1KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.9KB |
| netcoreapp2.2  |          9.0KB |       285.6KB |  +276.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       270.4KB |  +260.9KB |   +19.7KB |            +11.2KB |             +16.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       268.4KB |  +258.9KB |   +19.7KB |            +11.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       222.2KB |  +212.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       168.2KB |  +158.2KB |   +17.2KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       124.5KB |  +114.5KB |   +17.1KB |             +7.4KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |        99.0KB |   +89.5KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |         10.0KB |        51.6KB |   +41.6KB |   +16.5KB |                    |              +1.6KB |      +4.2KB |
| net10.0        |         10.0KB |        33.9KB |   +23.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        28.5KB |   +18.5KB |   +16.5KB |                    |              +1.6KB |      +4.7KB |
