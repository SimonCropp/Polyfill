### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       355.0KB |  +347.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       308.5KB |  +300.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       353.5KB |  +345.0KB |    +8.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net462         |          7.0KB |       357.5KB |  +350.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       357.0KB |  +350.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       356.0KB |  +347.5KB |    +8.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net472         |          8.5KB |       355.0KB |  +346.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       355.0KB |  +346.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       355.0KB |  +346.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       332.5KB |  +323.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       312.0KB |  +303.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       312.0KB |  +303.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       305.0KB |  +295.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       303.5KB |  +294.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       267.0KB |  +257.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       209.0KB |  +199.0KB |    +9.5KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       171.5KB |  +161.5KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.0KB |
| net8.0         |          9.5KB |       142.0KB |  +132.5KB |    +8.0KB |                    |           +512bytes |      +3.0KB |
| net9.0         |          9.5KB |        94.5KB |   +85.0KB |    +9.0KB |                    |              +1.0KB |      +3.5KB |
| net10.0        |         10.0KB |        72.5KB |   +62.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        20.5KB |   +10.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       519.1KB |  +511.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       445.9KB |  +437.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       518.6KB |  +510.1KB |   +15.7KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net462         |          7.0KB |       522.6KB |  +515.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       521.9KB |  +514.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       520.5KB |  +512.0KB |   +15.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net472         |          8.5KB |       518.4KB |  +509.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       518.4KB |  +509.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       518.4KB |  +509.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       486.0KB |  +477.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       453.1KB |  +444.1KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       453.1KB |  +444.1KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       437.3KB |  +427.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       435.8KB |  +426.3KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       381.1KB |  +371.6KB |   +16.7KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net6.0         |         10.0KB |       303.0KB |  +293.0KB |   +17.2KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       246.9KB |  +236.9KB |   +16.6KB |             +6.9KB |              +1.1KB |      +3.7KB |
| net8.0         |          9.5KB |       201.9KB |  +192.4KB |   +15.5KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |          9.5KB |       133.2KB |  +123.7KB |   +16.5KB |                    |              +1.6KB |      +4.2KB |
| net10.0        |         10.0KB |       102.9KB |   +92.9KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        30.3KB |   +20.3KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
