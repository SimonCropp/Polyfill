### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       330.0KB |  +322.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       283.5KB |  +275.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       328.5KB |  +320.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       332.0KB |  +325.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       332.0KB |  +325.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       331.0KB |  +322.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       330.0KB |  +321.5KB |    +8.5KB |             +6.0KB |              +8.5KB |     +13.5KB |
| net48          |          8.5KB |       330.0KB |  +321.5KB |    +8.5KB |             +6.0KB |              +8.5KB |     +13.5KB |
| net481         |          8.5KB |       330.0KB |  +321.5KB |    +8.5KB |             +6.0KB |              +8.5KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       306.0KB |  +297.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       286.0KB |  +277.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       286.0KB |  +277.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       278.5KB |  +269.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       276.5KB |  +267.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net5.0         |          9.5KB |       240.0KB |  +230.5KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       181.5KB |  +171.5KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       144.0KB |  +134.0KB |    +9.0KB |             +5.0KB |           +512bytes |      +3.0KB |
| net8.0         |          9.5KB |       115.0KB |  +105.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        69.0KB |   +59.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        45.5KB |   +35.5KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net11.0        |         10.0KB |        20.5KB |   +10.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       481.4KB |  +473.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       409.6KB |  +401.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       480.9KB |  +472.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       484.4KB |  +477.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       484.2KB |  +477.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       482.8KB |  +474.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       480.8KB |  +472.3KB |   +16.2KB |             +7.7KB |             +13.4KB |     +18.9KB |
| net48          |          8.5KB |       480.8KB |  +472.3KB |   +16.2KB |             +7.7KB |             +13.4KB |     +18.9KB |
| net481         |          8.5KB |       480.8KB |  +472.3KB |   +16.2KB |             +7.7KB |             +13.4KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       447.0KB |  +438.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       415.9KB |  +406.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       415.9KB |  +406.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       399.6KB |  +390.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       397.6KB |  +388.1KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net5.0         |          9.5KB |       342.9KB |  +333.4KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net6.0         |         10.0KB |       264.4KB |  +254.4KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       208.3KB |  +198.3KB |   +16.6KB |             +6.4KB |              +1.1KB |      +3.7KB |
| net8.0         |          9.5KB |       164.0KB |  +154.5KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        96.5KB |   +87.0KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        64.1KB |   +54.1KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net11.0        |         10.0KB |        30.3KB |   +20.3KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
