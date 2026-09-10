### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       384.0KB |  +376.0KB |    +8.5KB |             +6.0KB |              +8.5KB |     +13.0KB |
| netstandard2.1 |          8.5KB |       339.5KB |  +331.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       382.5KB |  +374.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       386.0KB |  +379.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       386.0KB |  +379.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       385.0KB |  +376.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       383.5KB |  +375.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       383.5KB |  +375.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       383.5KB |  +375.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       363.0KB |  +354.0KB |    +7.5KB |             +5.0KB |              +7.5KB |     +12.0KB |
| netcoreapp2.1  |          9.0KB |       342.5KB |  +333.5KB |    +8.5KB |             +6.0KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       342.5KB |  +333.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       340.5KB |  +331.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       339.0KB |  +329.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       304.5KB |  +295.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       245.5KB |  +235.5KB |    +9.5KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       214.0KB |  +204.0KB |    +9.5KB |             +6.0KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       185.0KB |  +175.5KB |    +8.0KB |                    |           +512bytes |      +3.0KB |
| net9.0         |          9.5KB |       117.0KB |  +107.5KB |    +8.0KB |                    |           +512bytes |      +3.0KB |
| net10.0        |         10.0KB |        93.5KB |   +83.5KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net11.0        |         10.0KB |        21.0KB |   +11.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       559.0KB |  +551.0KB |   +16.2KB |             +7.7KB |             +13.4KB |     +18.4KB |
| netstandard2.1 |          8.5KB |       487.5KB |  +479.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       558.6KB |  +550.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       562.1KB |  +555.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       561.8KB |  +554.8KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       560.5KB |  +552.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       557.9KB |  +549.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       557.9KB |  +549.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       557.9KB |  +549.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       527.5KB |  +518.5KB |   +15.2KB |             +6.7KB |             +12.4KB |     +17.4KB |
| netcoreapp2.1  |          9.0KB |       494.2KB |  +485.2KB |   +16.2KB |             +7.7KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       494.2KB |  +485.2KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       485.6KB |  +476.1KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       484.0KB |  +474.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       432.1KB |  +422.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       352.9KB |  +342.9KB |   +17.2KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       304.6KB |  +294.6KB |   +17.1KB |             +7.4KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       261.8KB |  +252.3KB |   +15.5KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |          9.5KB |       169.1KB |  +159.6KB |   +15.5KB |                    |              +1.1KB |      +3.7KB |
| net10.0        |         10.0KB |       136.1KB |  +126.1KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net11.0        |         10.0KB |        30.9KB |   +20.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
