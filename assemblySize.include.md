### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       235.0KB |  +227.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       186.5KB |  +178.0KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| net461         |          8.5KB |       241.0KB |  +232.5KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net462         |          7.0KB |       240.0KB |  +233.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net47          |          7.0KB |       240.0KB |  +233.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       240.0KB |  +231.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       238.5KB |  +230.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net48          |          8.5KB |       238.5KB |  +230.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       238.5KB |  +230.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       215.0KB |  +206.0KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       197.0KB |  +188.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       197.0KB |  +188.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       185.0KB |  +175.5KB |   +12.0KB |             +9.5KB |             +12.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       183.0KB |  +173.5KB |   +12.0KB |             +9.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       153.0KB |  +143.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       113.5KB |  +103.5KB |    +9.5KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |        86.0KB |   +76.0KB |    +9.5KB |             +6.0KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        69.5KB |   +60.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        35.5KB |   +26.0KB |    +9.0KB |                    |              +1.0KB |      +3.5KB |
| net10.0        |         10.0KB |        22.5KB |   +12.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        19.0KB |    +9.0KB |    +9.0KB |                    |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       349.6KB |  +341.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       276.9KB |  +268.4KB |   +19.7KB |            +11.2KB |             +16.9KB |     +22.4KB |
| net461         |          8.5KB |       356.0KB |  +347.5KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net462         |          7.0KB |       355.0KB |  +348.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net47          |          7.0KB |       354.8KB |  +347.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       354.8KB |  +346.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       352.2KB |  +343.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net48          |          8.5KB |       352.2KB |  +343.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       352.2KB |  +343.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.0  |          9.0KB |       319.7KB |  +310.7KB |   +16.7KB |             +8.7KB |             +13.9KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       291.6KB |  +282.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       291.6KB |  +282.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       270.5KB |  +261.0KB |   +19.7KB |            +11.2KB |             +16.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       268.4KB |  +258.9KB |   +19.7KB |            +11.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       222.2KB |  +212.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       168.3KB |  +158.3KB |   +17.2KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       124.6KB |  +114.6KB |   +17.1KB |             +7.4KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |        99.1KB |   +89.6KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        51.6KB |   +42.1KB |   +16.5KB |                    |              +1.6KB |      +4.2KB |
| net10.0        |         10.0KB |        33.9KB |   +23.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        28.5KB |   +18.5KB |   +16.5KB |                    |              +1.6KB |      +4.7KB |
