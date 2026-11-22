### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       432.5KB |  +424.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| netstandard2.1 |          8.5KB |       376.0KB |  +367.5KB |    +7.5KB |             +5.5KB |              +8.0KB |     +12.5KB |
| net461         |          8.5KB |       431.0KB |  +422.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net462         |          7.0KB |       434.5KB |  +427.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net47          |          7.0KB |       434.5KB |  +427.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       434.5KB |  +426.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net472         |          8.5KB |       432.0KB |  +423.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net48          |          8.5KB |       432.0KB |  +423.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net481         |          8.5KB |       432.0KB |  +423.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| netcoreapp2.0  |          9.0KB |       408.5KB |  +399.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| netcoreapp2.1  |          9.0KB |       383.5KB |  +374.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| netcoreapp2.2  |          9.0KB |       383.5KB |  +374.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| netcoreapp3.0  |          9.5KB |       375.0KB |  +365.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       375.0KB |  +365.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       338.5KB |  +329.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net6.0         |         10.0KB |       276.0KB |  +266.0KB |    +9.5KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       248.5KB |  +238.5KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.0KB |
| net8.0         |          9.5KB |       229.5KB |  +220.0KB |    +8.5KB |          +512bytes |           +512bytes |      +3.5KB |
| net9.0         |         10.0KB |       150.5KB |  +140.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |       124.5KB |  +114.5KB |    +8.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        17.5KB |    +7.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       631.4KB |  +623.4KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| netstandard2.1 |          8.5KB |       541.2KB |  +532.7KB |   +15.1KB |             +7.2KB |             +12.9KB |     +17.9KB |
| net461         |          8.5KB |       630.6KB |  +622.1KB |   +16.6KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net462         |          7.0KB |       634.1KB |  +627.1KB |   +16.6KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net47          |          7.0KB |       634.1KB |  +627.1KB |   +16.1KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       634.2KB |  +625.7KB |   +16.1KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net472         |          8.5KB |       630.3KB |  +621.8KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net48          |          8.5KB |       630.3KB |  +621.8KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net481         |          8.5KB |       630.3KB |  +621.8KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| netcoreapp2.0  |          9.0KB |       596.1KB |  +587.1KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| netcoreapp2.1  |          9.0KB |       555.8KB |  +546.8KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| netcoreapp2.2  |          9.0KB |       555.8KB |  +546.8KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| netcoreapp3.0  |          9.5KB |       538.1KB |  +528.6KB |   +16.1KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       538.1KB |  +528.6KB |   +16.1KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       483.4KB |  +473.9KB |   +16.1KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net6.0         |         10.0KB |       399.1KB |  +389.1KB |   +17.1KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       355.9KB |  +345.9KB |   +16.5KB |             +6.9KB |              +1.1KB |      +3.7KB |
| net8.0         |          9.5KB |       324.7KB |  +315.2KB |   +15.9KB |          +811bytes |              +1.1KB |      +4.2KB |
| net9.0         |         10.0KB |       217.0KB |  +207.0KB |   +15.9KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       180.2KB |  +170.2KB |   +15.4KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        25.7KB |   +15.7KB |   +16.4KB |                    |              +1.1KB |      +4.2KB |
