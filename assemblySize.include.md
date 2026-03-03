### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       242.0KB |  +234.0KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       196.5KB |  +188.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net461         |          8.5KB |       248.5KB |  +240.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net462         |          7.0KB |       247.5KB |  +240.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       247.0KB |  +240.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       247.0KB |  +238.5KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| net472         |          8.5KB |       245.5KB |  +237.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net48          |          8.5KB |       245.5KB |  +237.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net481         |          8.5KB |       246.0KB |  +237.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       222.5KB |  +213.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       204.0KB |  +195.0KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       204.0KB |  +195.0KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       192.0KB |  +182.5KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| netcoreapp3.1  |          9.5KB |       190.0KB |  +180.5KB |   +12.5KB |            +10.0KB |             +12.5KB |     +17.0KB |
| net5.0         |          9.5KB |       160.0KB |  +150.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       115.0KB |  +105.0KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |        86.0KB |   +76.0KB |    +9.5KB |             +6.0KB |              +1.0KB |      +3.5KB |
| net8.0         |          9.5KB |        69.5KB |   +60.0KB |    +8.5KB |          +512bytes |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        35.5KB |   +26.0KB |    +9.0KB |                    |              +1.0KB |      +4.0KB |
| net10.0        |         10.0KB |        22.5KB |   +12.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        19.0KB |    +9.0KB |    +9.5KB |                    |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       359.6KB |  +351.6KB |   +16.7KB |             +8.7KB |             +14.4KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       289.6KB |  +281.1KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net461         |          8.5KB |       366.6KB |  +358.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net462         |          7.0KB |       365.6KB |  +358.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       364.8KB |  +357.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       364.8KB |  +356.3KB |   +16.7KB |             +8.7KB |             +13.9KB |     +19.4KB |
| net472         |          8.5KB |       362.3KB |  +353.8KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net48          |          8.5KB |       362.3KB |  +353.8KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net481         |          8.5KB |       362.8KB |  +354.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       330.3KB |  +321.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       301.3KB |  +292.3KB |   +16.7KB |             +8.7KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       301.3KB |  +292.3KB |   +16.7KB |             +8.7KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       280.1KB |  +270.6KB |   +19.7KB |            +11.2KB |             +16.9KB |     +22.4KB |
| netcoreapp3.1  |          9.5KB |       278.1KB |  +268.6KB |   +20.2KB |            +11.7KB |             +17.4KB |     +22.4KB |
| net5.0         |          9.5KB |       231.9KB |  +222.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       170.1KB |  +160.1KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       124.6KB |  +114.6KB |   +17.1KB |             +7.4KB |              +1.6KB |      +4.2KB |
| net8.0         |          9.5KB |        99.1KB |   +89.6KB |   +16.0KB |          +811bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        51.6KB |   +42.1KB |   +16.5KB |                    |              +1.6KB |      +4.7KB |
| net10.0        |         10.0KB |        33.9KB |   +23.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        28.5KB |   +18.5KB |   +17.0KB |                    |              +1.6KB |      +4.7KB |
