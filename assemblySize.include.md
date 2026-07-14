### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       353.0KB |  +345.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       306.5KB |  +298.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       351.5KB |  +343.0KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net462         |          7.0KB |       355.5KB |  +348.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       355.0KB |  +348.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       354.0KB |  +345.5KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net472         |          8.5KB |       353.0KB |  +344.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       353.0KB |  +344.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       353.0KB |  +344.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       330.5KB |  +321.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       310.0KB |  +301.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       310.0KB |  +301.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       303.0KB |  +293.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       301.0KB |  +291.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       265.0KB |  +255.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       206.5KB |  +196.5KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       169.0KB |  +159.0KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       139.5KB |  +130.0KB |    +8.5KB |          +512bytes |           +512bytes |      +3.5KB |
| net9.0         |         10.0KB |        92.5KB |   +82.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        70.0KB |   +60.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        31.5KB |   +21.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       516.0KB |  +508.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       442.9KB |  +434.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       515.6KB |  +507.1KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net462         |          7.0KB |       519.6KB |  +512.6KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       518.8KB |  +511.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       517.5KB |  +509.0KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net472         |          8.5KB |       515.4KB |  +506.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       515.4KB |  +506.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       515.4KB |  +506.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       482.9KB |  +473.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       450.2KB |  +441.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       450.2KB |  +441.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       434.4KB |  +424.9KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       432.4KB |  +422.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       378.2KB |  +368.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       299.6KB |  +289.6KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       243.5KB |  +233.5KB |   +17.1KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       198.5KB |  +189.0KB |   +16.0KB |          +811bytes |              +1.1KB |      +4.2KB |
| net9.0         |         10.0KB |       130.4KB |  +120.4KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        99.5KB |   +89.5KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        46.9KB |   +36.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
