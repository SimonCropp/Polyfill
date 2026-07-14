### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       354.0KB |  +346.0KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       307.5KB |  +299.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       353.0KB |  +344.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       356.5KB |  +349.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       356.5KB |  +349.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       355.5KB |  +347.0KB |    +7.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       354.0KB |  +345.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net48          |          8.5KB |       354.0KB |  +345.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       354.0KB |  +345.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       331.5KB |  +322.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       311.5KB |  +302.5KB |    +8.5KB |             +6.0KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       311.5KB |  +302.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       304.0KB |  +294.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       302.5KB |  +293.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       266.0KB |  +256.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       208.0KB |  +198.0KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |       170.5KB |  +160.5KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.0KB |
| net8.0         |          9.5KB |       141.0KB |  +131.5KB |    +8.0KB |                    |           +512bytes |      +3.0KB |
| net9.0         |         10.0KB |        93.5KB |   +83.5KB |    +8.5KB |                    |              +1.0KB |      +3.5KB |
| net10.0        |         10.0KB |        71.5KB |   +61.5KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net11.0        |         10.0KB |        20.5KB |   +10.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       517.5KB |  +509.5KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       444.3KB |  +435.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       517.6KB |  +509.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       521.1KB |  +514.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       520.8KB |  +513.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       519.5KB |  +511.0KB |   +15.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       516.9KB |  +508.4KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net48          |          8.5KB |       516.9KB |  +508.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       516.9KB |  +508.4KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp2.0  |          9.0KB |       484.4KB |  +475.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       452.0KB |  +443.0KB |   +16.2KB |             +7.7KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       452.0KB |  +443.0KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       435.7KB |  +426.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       434.2KB |  +424.7KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       379.5KB |  +370.0KB |   +16.7KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net6.0         |         10.0KB |       301.4KB |  +291.4KB |   +17.2KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       245.3KB |  +235.3KB |   +16.6KB |             +6.9KB |              +1.1KB |      +3.7KB |
| net8.0         |          9.5KB |       200.3KB |  +190.8KB |   +15.5KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |         10.0KB |       131.7KB |  +121.7KB |   +16.0KB |                    |              +1.6KB |      +4.2KB |
| net10.0        |         10.0KB |       101.3KB |   +91.3KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net11.0        |         10.0KB |        30.3KB |   +20.3KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
