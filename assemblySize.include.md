### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       384.0KB |  +376.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       340.0KB |  +331.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       383.0KB |  +374.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       386.5KB |  +379.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       386.5KB |  +379.5KB |    +8.5KB |             +6.0KB |              +8.5KB |     +13.0KB |
| net471         |          8.5KB |       385.5KB |  +377.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       384.0KB |  +375.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       384.0KB |  +375.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       384.0KB |  +375.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       362.0KB |  +353.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       343.0KB |  +334.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       343.0KB |  +334.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       341.0KB |  +331.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       339.5KB |  +330.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       305.0KB |  +295.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       246.0KB |  +236.0KB |    +9.5KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       214.5KB |  +204.5KB |    +9.5KB |             +6.0KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       185.5KB |  +176.0KB |    +8.0KB |                    |           +512bytes |      +3.0KB |
| net9.0         |          9.5KB |       117.0KB |  +107.5KB |    +8.0KB |                    |           +512bytes |      +3.0KB |
| net10.0        |         10.0KB |        93.5KB |   +83.5KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net11.0        |         10.0KB |        21.0KB |   +11.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       559.3KB |  +551.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       488.3KB |  +479.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       559.3KB |  +550.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       562.8KB |  +555.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       562.6KB |  +555.6KB |   +16.2KB |             +7.7KB |             +13.4KB |     +18.4KB |
| net471         |          8.5KB |       561.2KB |  +552.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       558.6KB |  +550.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       558.6KB |  +550.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       558.6KB |  +550.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       526.7KB |  +517.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       495.0KB |  +486.0KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       495.0KB |  +486.0KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       486.4KB |  +476.9KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       484.8KB |  +475.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       432.9KB |  +423.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       353.7KB |  +343.7KB |   +17.2KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       305.4KB |  +295.4KB |   +17.1KB |             +7.4KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       262.6KB |  +253.1KB |   +15.5KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |          9.5KB |       169.1KB |  +159.6KB |   +15.5KB |                    |              +1.1KB |      +3.7KB |
| net10.0        |         10.0KB |       136.1KB |  +126.1KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net11.0        |         10.0KB |        30.9KB |   +20.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
