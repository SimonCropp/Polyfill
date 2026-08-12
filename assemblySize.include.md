### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       357.0KB |  +349.0KB |    +8.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       311.0KB |  +302.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       355.5KB |  +347.0KB |    +8.0KB |             +7.0KB |              +8.0KB |     +12.5KB |
| net462         |          7.0KB |       359.5KB |  +352.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       359.0KB |  +352.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       358.0KB |  +349.5KB |    +8.0KB |             +7.0KB |              +8.0KB |     +12.5KB |
| net472         |          8.5KB |       357.0KB |  +348.5KB |    +7.5KB |             +6.5KB |              +9.0KB |     +12.0KB |
| net48          |          8.5KB |       357.0KB |  +348.5KB |    +7.5KB |             +6.5KB |              +9.0KB |     +12.0KB |
| net481         |          8.5KB |       357.0KB |  +348.5KB |    +7.5KB |             +6.5KB |              +9.0KB |     +12.5KB |
| netcoreapp2.0  |          9.0KB |       334.5KB |  +325.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       314.5KB |  +305.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       314.5KB |  +305.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       307.5KB |  +298.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       305.5KB |  +296.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       269.5KB |  +260.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       211.0KB |  +201.0KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       173.5KB |  +163.5KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       144.0KB |  +134.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        97.0KB |   +87.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        75.0KB |   +65.0KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net11.0        |         10.0KB |        20.5KB |   +10.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       522.0KB |  +514.0KB |   +15.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       449.3KB |  +440.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       521.5KB |  +513.0KB |   +15.7KB |             +8.7KB |             +12.9KB |     +17.9KB |
| net462         |          7.0KB |       525.5KB |  +518.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       524.8KB |  +517.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       523.4KB |  +514.9KB |   +15.7KB |             +8.7KB |             +12.9KB |     +17.9KB |
| net472         |          8.5KB |       521.3KB |  +512.8KB |   +15.2KB |             +8.2KB |             +13.9KB |     +17.4KB |
| net48          |          8.5KB |       521.3KB |  +512.8KB |   +15.2KB |             +8.2KB |             +13.9KB |     +17.4KB |
| net481         |          8.5KB |       521.3KB |  +512.8KB |   +15.2KB |             +8.2KB |             +13.9KB |     +17.9KB |
| netcoreapp2.0  |          9.0KB |       488.9KB |  +479.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       456.5KB |  +447.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       456.5KB |  +447.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       440.8KB |  +431.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       438.7KB |  +429.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       384.6KB |  +375.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       305.9KB |  +295.9KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       249.8KB |  +239.8KB |   +16.6KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       204.8KB |  +195.3KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |       136.7KB |  +127.2KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       106.4KB |   +96.4KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net11.0        |         10.0KB |        30.3KB |   +20.3KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
