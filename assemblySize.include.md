### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       273.5KB |  +265.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       225.5KB |  +217.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       281.5KB |  +273.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       280.5KB |  +273.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       280.0KB |  +273.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       280.0KB |  +271.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       279.0KB |  +270.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       279.0KB |  +270.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       279.0KB |  +270.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       255.0KB |  +246.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       234.0KB |  +225.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.5KB |
| netcoreapp2.2  |          9.0KB |       234.5KB |  +225.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       225.5KB |  +216.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       223.5KB |  +214.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net5.0         |          9.5KB |       189.0KB |  +179.5KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       141.5KB |  +131.5KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       108.5KB |   +98.5KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        88.5KB |   +79.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        46.5KB |   +37.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        23.5KB |   +13.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        18.5KB |    +8.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       404.3KB |  +396.3KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       331.0KB |  +322.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       412.8KB |  +404.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       411.8KB |  +404.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       411.0KB |  +404.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       411.0KB |  +402.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       409.0KB |  +400.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       409.0KB |  +400.5KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       409.0KB |  +400.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       375.7KB |  +366.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       343.7KB |  +334.7KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.9KB |
| netcoreapp2.2  |          9.0KB |       344.2KB |  +335.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       326.1KB |  +316.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       324.0KB |  +314.5KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net5.0         |          9.5KB |       272.2KB |  +262.7KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net6.0         |         10.0KB |       208.2KB |  +198.2KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       156.9KB |  +146.9KB |   +17.1KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       125.9KB |  +116.4KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        66.9KB |   +57.4KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        35.8KB |   +25.8KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        27.4KB |   +17.4KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
