### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       433.0KB |  +425.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       376.5KB |  +368.0KB |    +7.5KB |             +5.0KB |              +7.5KB |     +12.0KB |
| net461         |          8.5KB |       431.0KB |  +422.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net462         |          7.0KB |       434.5KB |  +427.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net47          |          7.0KB |       434.5KB |  +427.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net471         |          8.5KB |       434.5KB |  +426.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net472         |          8.5KB |       432.0KB |  +423.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net48          |          8.5KB |       432.0KB |  +423.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net481         |          8.5KB |       432.5KB |  +424.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       409.0KB |  +400.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       384.0KB |  +375.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       384.0KB |  +375.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       375.0KB |  +365.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       375.0KB |  +365.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       338.5KB |  +329.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       276.0KB |  +266.0KB |    +9.5KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       248.5KB |  +238.5KB |    +9.0KB |             +6.0KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       229.5KB |  +220.0KB |    +8.5KB |          +512bytes |              +1.0KB |      +3.5KB |
| net9.0         |         10.0KB |       150.5KB |  +140.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |       124.5KB |  +114.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        18.0KB |    +8.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       632.0KB |  +624.0KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       541.8KB |  +533.3KB |   +15.1KB |             +6.7KB |             +12.4KB |     +17.4KB |
| net461         |          8.5KB |       630.7KB |  +622.2KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net462         |          7.0KB |       634.2KB |  +627.2KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net47          |          7.0KB |       634.2KB |  +627.2KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net471         |          8.5KB |       634.3KB |  +625.8KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net472         |          8.5KB |       630.4KB |  +621.9KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net48          |          8.5KB |       630.4KB |  +621.9KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net481         |          8.5KB |       630.9KB |  +622.4KB |   +16.1KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.0  |          9.0KB |       596.7KB |  +587.7KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       556.4KB |  +547.4KB |   +16.1KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       556.4KB |  +547.4KB |   +16.1KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       538.2KB |  +528.7KB |   +16.1KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       538.2KB |  +528.7KB |   +16.1KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       483.5KB |  +474.0KB |   +16.6KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       399.2KB |  +389.2KB |   +17.1KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       356.0KB |  +346.0KB |   +16.5KB |             +7.4KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       324.8KB |  +315.3KB |   +15.9KB |          +811bytes |              +1.6KB |      +4.2KB |
| net9.0         |         10.0KB |       217.1KB |  +207.1KB |   +15.9KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       180.4KB |  +170.4KB |   +15.9KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        26.5KB |   +16.5KB |   +16.4KB |                    |              +1.1KB |      +4.2KB |
