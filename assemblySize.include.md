### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       310.5KB |  +302.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       263.5KB |  +255.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       309.0KB |  +300.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net462         |          7.0KB |       312.5KB |  +305.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net47          |          7.0KB |       312.5KB |  +305.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       311.5KB |  +303.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net472         |          8.5KB |       310.0KB |  +301.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net48          |          8.5KB |       310.0KB |  +301.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net481         |          8.5KB |       310.5KB |  +302.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       286.5KB |  +277.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       266.0KB |  +257.0KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       266.0KB |  +257.0KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       257.5KB |  +248.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       255.5KB |  +246.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net5.0         |          9.5KB |       218.5KB |  +209.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net6.0         |         10.0KB |       157.0KB |  +147.0KB |   +12.5KB |            +10.0KB |              +1.0KB |      +4.0KB |
| net7.0         |         10.0KB |       122.0KB |  +112.0KB |    +9.0KB |             +5.5KB |           +512bytes |      +4.0KB |
| net8.0         |          9.5KB |        94.0KB |   +84.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        47.5KB |   +38.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        24.0KB |   +14.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        18.5KB |    +8.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       453.3KB |  +445.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       381.2KB |  +372.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       452.9KB |  +444.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net462         |          7.0KB |       456.4KB |  +449.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net47          |          7.0KB |       456.1KB |  +449.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       454.8KB |  +446.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net472         |          8.5KB |       452.2KB |  +443.7KB |   +16.7KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net48          |          8.5KB |       452.2KB |  +443.7KB |   +16.7KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net481         |          8.5KB |       452.7KB |  +444.2KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       419.5KB |  +410.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       387.9KB |  +378.9KB |   +16.7KB |             +8.7KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       387.9KB |  +378.9KB |   +16.7KB |             +8.7KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       370.1KB |  +360.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       368.1KB |  +358.6KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net5.0         |          9.5KB |       313.0KB |  +303.5KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net6.0         |         10.0KB |       231.9KB |  +221.9KB |   +20.2KB |            +11.7KB |              +1.6KB |      +4.7KB |
| net7.0         |         10.0KB |       178.3KB |  +168.3KB |   +16.6KB |             +6.9KB |              +1.1KB |      +4.7KB |
| net8.0         |          9.5KB |       135.4KB |  +125.9KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        68.6KB |   +59.1KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        36.5KB |   +26.5KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        27.4KB |   +17.4KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
