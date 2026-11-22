### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       424.0KB |  +416.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       367.5KB |  +359.0KB |    +7.5KB |             +6.0KB |              +7.5KB |     +12.0KB |
| net461         |          8.5KB |       423.0KB |  +414.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net462         |          7.0KB |       426.5KB |  +419.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net47          |          7.0KB |       426.5KB |  +419.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       425.5KB |  +417.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net472         |          8.5KB |       423.0KB |  +414.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net48          |          8.5KB |       423.0KB |  +414.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net481         |          8.5KB |       423.0KB |  +414.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| netcoreapp2.0  |          9.0KB |       399.5KB |  +390.5KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.5KB |
| netcoreapp2.1  |          9.0KB |       375.0KB |  +366.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       375.0KB |  +366.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       367.0KB |  +357.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       367.0KB |  +357.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       330.5KB |  +321.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       268.0KB |  +258.0KB |    +9.5KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       240.0KB |  +230.0KB |    +9.5KB |             +6.0KB |              +1.0KB |      +3.5KB |
| net8.0         |          9.5KB |       215.0KB |  +205.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |       134.5KB |  +125.0KB |    +8.5KB |                    |              +1.0KB |      +3.5KB |
| net10.0        |         10.0KB |       108.5KB |   +98.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        17.5KB |    +7.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       617.5KB |  +609.5KB |   +16.1KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       527.4KB |  +518.9KB |   +15.1KB |             +7.7KB |             +12.4KB |     +17.4KB |
| net461         |          8.5KB |       617.9KB |  +609.4KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net462         |          7.0KB |       621.4KB |  +614.4KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net47          |          7.0KB |       621.4KB |  +614.4KB |   +16.6KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       619.8KB |  +611.3KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net472         |          8.5KB |       615.9KB |  +607.4KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net48          |          8.5KB |       615.9KB |  +607.4KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net481         |          8.5KB |       615.9KB |  +607.4KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| netcoreapp2.0  |          9.0KB |       581.7KB |  +572.7KB |   +17.1KB |             +8.7KB |             +14.4KB |     +19.9KB |
| netcoreapp2.1  |          9.0KB |       541.9KB |  +532.9KB |   +16.1KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       541.9KB |  +532.9KB |   +16.1KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       525.1KB |  +515.6KB |   +16.1KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       525.1KB |  +515.6KB |   +16.1KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       470.3KB |  +460.8KB |   +16.6KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       386.1KB |  +376.1KB |   +17.1KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       342.4KB |  +332.4KB |   +17.0KB |             +7.4KB |              +1.6KB |      +4.2KB |
| net8.0         |          9.5KB |       302.5KB |  +293.0KB |   +15.9KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |       192.7KB |  +183.2KB |   +15.9KB |                    |              +1.6KB |      +4.2KB |
| net10.0        |         10.0KB |       155.9KB |  +145.9KB |   +15.9KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        25.7KB |   +15.7KB |   +16.4KB |                    |              +1.1KB |      +4.2KB |
