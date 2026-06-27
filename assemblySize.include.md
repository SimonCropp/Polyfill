### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       351.5KB |  +343.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       306.0KB |  +297.5KB |    +8.5KB |             +6.0KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       350.0KB |  +341.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net462         |          7.0KB |       353.5KB |  +346.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +13.5KB |
| net47          |          7.0KB |       353.5KB |  +346.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       352.5KB |  +344.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net472         |          8.5KB |       351.5KB |  +343.0KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       351.5KB |  +343.0KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       351.5KB |  +343.0KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       327.5KB |  +318.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       308.0KB |  +299.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       308.0KB |  +299.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       301.0KB |  +291.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       299.0KB |  +289.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       263.0KB |  +253.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       205.0KB |  +195.0KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |       167.0KB |  +157.0KB |    +9.5KB |             +5.5KB |              +1.0KB |      +3.5KB |
| net8.0         |          9.5KB |       138.5KB |  +129.0KB |    +8.5KB |          +512bytes |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        92.5KB |   +83.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        70.0KB |   +60.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        31.5KB |   +21.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       513.0KB |  +505.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       441.7KB |  +433.2KB |   +16.2KB |             +7.7KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       512.5KB |  +504.0KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net462         |          7.0KB |       516.0KB |  +509.0KB |   +16.7KB |             +8.2KB |             +14.4KB |     +18.9KB |
| net47          |          7.0KB |       515.8KB |  +508.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       514.4KB |  +505.9KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net472         |          8.5KB |       512.4KB |  +503.9KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       512.4KB |  +503.9KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       512.4KB |  +503.9KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       478.4KB |  +469.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       447.4KB |  +438.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       447.4KB |  +438.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       431.6KB |  +422.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       429.6KB |  +420.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       375.4KB |  +365.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       297.4KB |  +287.4KB |   +17.2KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       240.8KB |  +230.8KB |   +17.1KB |             +6.9KB |              +1.6KB |      +4.2KB |
| net8.0         |          9.5KB |       197.3KB |  +187.8KB |   +16.0KB |          +811bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |       130.3KB |  +120.8KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        99.5KB |   +89.5KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        46.9KB |   +36.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
