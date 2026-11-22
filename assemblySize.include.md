### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       429.5KB |  +421.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| netstandard2.1 |          8.5KB |       373.0KB |  +364.5KB |    +7.5KB |             +5.5KB |              +8.0KB |     +12.5KB |
| net461         |          8.5KB |       428.0KB |  +419.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net462         |          7.0KB |       431.5KB |  +424.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net47          |          7.0KB |       431.5KB |  +424.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       431.5KB |  +423.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net472         |          8.5KB |       429.0KB |  +420.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net48          |          8.5KB |       429.0KB |  +420.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net481         |          8.5KB |       429.0KB |  +420.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| netcoreapp2.0  |          9.0KB |       405.5KB |  +396.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| netcoreapp2.1  |          9.0KB |       380.5KB |  +371.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| netcoreapp2.2  |          9.0KB |       381.0KB |  +372.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       372.5KB |  +363.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       372.5KB |  +363.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net5.0         |          9.5KB |       336.0KB |  +326.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       274.0KB |  +264.0KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |       246.0KB |  +236.0KB |    +9.5KB |             +6.0KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       225.5KB |  +216.0KB |    +8.5KB |          +512bytes |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |       147.5KB |  +138.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |       121.5KB |  +111.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        17.5KB |    +7.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       626.9KB |  +618.9KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| netstandard2.1 |          8.5KB |       536.7KB |  +528.2KB |   +15.1KB |             +7.2KB |             +12.9KB |     +17.9KB |
| net461         |          8.5KB |       626.1KB |  +617.6KB |   +16.6KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net462         |          7.0KB |       629.6KB |  +622.6KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net47          |          7.0KB |       629.6KB |  +622.6KB |   +16.1KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       629.6KB |  +621.1KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net472         |          8.5KB |       625.8KB |  +617.3KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net48          |          8.5KB |       625.8KB |  +617.3KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net481         |          8.5KB |       625.8KB |  +617.3KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| netcoreapp2.0  |          9.0KB |       591.5KB |  +582.5KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| netcoreapp2.1  |          9.0KB |       551.2KB |  +542.2KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| netcoreapp2.2  |          9.0KB |       551.7KB |  +542.7KB |   +16.1KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       534.4KB |  +524.9KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       534.4KB |  +524.9KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net5.0         |          9.5KB |       479.7KB |  +470.2KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net6.0         |         10.0KB |       395.9KB |  +385.9KB |   +17.1KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       352.2KB |  +342.2KB |   +17.0KB |             +7.4KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       319.4KB |  +309.9KB |   +15.9KB |          +811bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |       212.7KB |  +203.2KB |   +15.9KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       175.9KB |  +165.9KB |   +15.9KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        25.7KB |   +15.7KB |   +16.4KB |                    |              +1.1KB |      +4.2KB |
