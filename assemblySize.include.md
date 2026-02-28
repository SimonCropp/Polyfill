### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       238.0KB |  +230.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       190.0KB |  +181.5KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| net461         |          8.5KB |       244.5KB |  +236.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net462         |          7.0KB |       243.5KB |  +236.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       243.0KB |  +236.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net471         |          8.5KB |       243.0KB |  +234.5KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net472         |          8.5KB |       242.0KB |  +233.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       242.0KB |  +233.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       242.0KB |  +233.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       218.5KB |  +209.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       200.5KB |  +191.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       200.5KB |  +191.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       188.0KB |  +178.5KB |   +12.5KB |            +10.0KB |             +12.5KB |     +17.0KB |
| netcoreapp3.1  |          9.5KB |       186.5KB |  +177.0KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| net5.0         |          9.5KB |       156.5KB |  +147.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       115.0KB |  +105.0KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |        86.0KB |   +76.0KB |    +9.5KB |             +6.0KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        69.5KB |   +60.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        35.5KB |   +26.0KB |    +9.0KB |                    |              +1.0KB |      +3.5KB |
| net10.0        |         10.0KB |        22.5KB |   +12.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        19.0KB |    +9.0KB |    +9.0KB |                    |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       353.3KB |  +345.3KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       281.2KB |  +272.7KB |   +19.7KB |            +11.2KB |             +16.9KB |     +22.4KB |
| net461         |          8.5KB |       360.3KB |  +351.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net462         |          7.0KB |       359.3KB |  +352.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       358.5KB |  +351.5KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net471         |          8.5KB |       358.5KB |  +350.0KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net472         |          8.5KB |       356.5KB |  +348.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       356.5KB |  +348.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       356.5KB |  +348.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       324.0KB |  +315.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       295.9KB |  +286.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       295.9KB |  +286.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       274.2KB |  +264.7KB |   +20.2KB |            +11.7KB |             +17.4KB |     +22.4KB |
| netcoreapp3.1  |          9.5KB |       272.7KB |  +263.2KB |   +19.7KB |            +11.2KB |             +16.9KB |     +22.4KB |
| net5.0         |          9.5KB |       226.7KB |  +217.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       170.0KB |  +160.0KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       124.6KB |  +114.6KB |   +17.1KB |             +7.4KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |        99.1KB |   +89.6KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        51.6KB |   +42.1KB |   +16.5KB |                    |              +1.6KB |      +4.2KB |
| net10.0        |         10.0KB |        33.9KB |   +23.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        28.5KB |   +18.5KB |   +16.5KB |                    |              +1.6KB |      +4.7KB |
