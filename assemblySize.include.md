### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       361.5KB |  +353.5KB |    +7.5KB |             +7.0KB |              +8.0KB |     +12.5KB |
| netstandard2.1 |          8.5KB |       315.5KB |  +307.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       360.5KB |  +352.0KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net462         |          7.0KB |       364.0KB |  +357.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       363.5KB |  +356.5KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net471         |          8.5KB |       363.0KB |  +354.5KB |    +7.5KB |             +5.0KB |              +7.5KB |     +12.0KB |
| net472         |          8.5KB |       361.5KB |  +353.0KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net48          |          8.5KB |       361.5KB |  +353.0KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net481         |          8.5KB |       361.5KB |  +353.0KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| netcoreapp2.0  |          9.0KB |       339.0KB |  +330.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       319.0KB |  +310.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       319.0KB |  +310.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       312.5KB |  +303.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       310.5KB |  +301.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       274.5KB |  +265.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       216.0KB |  +206.0KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       178.0KB |  +168.0KB |   +12.0KB |             +8.0KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       148.5KB |  +139.0KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net9.0         |          9.5KB |       101.5KB |   +92.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        79.0KB |   +69.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        20.5KB |   +10.5KB |    +9.5KB |          +512bytes |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       530.1KB |  +522.1KB |   +15.2KB |             +8.7KB |             +12.9KB |     +17.9KB |
| netstandard2.1 |          8.5KB |       457.5KB |  +449.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       530.1KB |  +521.6KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net462         |          7.0KB |       533.6KB |  +526.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       532.9KB |  +525.9KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net471         |          8.5KB |       532.0KB |  +523.5KB |   +15.2KB |             +6.7KB |             +12.4KB |     +17.4KB |
| net472         |          8.5KB |       529.5KB |  +521.0KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net48          |          8.5KB |       529.5KB |  +521.0KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net481         |          8.5KB |       529.5KB |  +521.0KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| netcoreapp2.0  |          9.0KB |       497.0KB |  +488.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       464.7KB |  +455.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       464.7KB |  +455.7KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       449.3KB |  +439.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       447.3KB |  +437.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       393.2KB |  +383.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       314.5KB |  +304.5KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       257.6KB |  +247.6KB |   +19.6KB |             +9.4KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       212.7KB |  +203.2KB |   +16.0KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |          9.5KB |       144.5KB |  +135.0KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       113.4KB |  +103.4KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        30.4KB |   +20.4KB |   +17.0KB |          +512bytes |              +1.6KB |      +4.7KB |
