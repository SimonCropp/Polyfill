### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       387.0KB |  +379.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       343.0KB |  +334.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       386.0KB |  +377.5KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       389.5KB |  +382.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       389.0KB |  +382.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       388.0KB |  +379.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net472         |          8.5KB |       387.0KB |  +378.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       387.0KB |  +378.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       387.0KB |  +378.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       365.0KB |  +356.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       346.0KB |  +337.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       346.0KB |  +337.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       344.0KB |  +334.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       342.5KB |  +333.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       308.0KB |  +298.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       249.0KB |  +239.0KB |    +9.5KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       217.5KB |  +207.5KB |    +9.5KB |             +6.0KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       188.0KB |  +178.5KB |    +8.5KB |          +512bytes |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |       117.0KB |  +107.5KB |    +8.0KB |                    |           +512bytes |      +3.0KB |
| net10.0        |         10.0KB |        93.5KB |   +83.5KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net11.0        |         10.0KB |        21.0KB |   +11.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       563.3KB |  +555.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       492.3KB |  +483.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       563.3KB |  +554.8KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       566.8KB |  +559.8KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       566.1KB |  +559.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       564.7KB |  +556.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net472         |          8.5KB |       562.6KB |  +554.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       562.6KB |  +554.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       562.6KB |  +554.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       530.7KB |  +521.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       499.0KB |  +490.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       499.0KB |  +490.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       490.3KB |  +480.8KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       488.8KB |  +479.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       436.9KB |  +427.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       357.7KB |  +347.7KB |   +17.2KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       309.3KB |  +299.3KB |   +17.1KB |             +7.4KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       266.1KB |  +256.6KB |   +16.0KB |          +811bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |       169.1KB |  +159.6KB |   +15.5KB |                    |              +1.1KB |      +3.7KB |
| net10.0        |         10.0KB |       136.1KB |  +126.1KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net11.0        |         10.0KB |        30.9KB |   +20.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
