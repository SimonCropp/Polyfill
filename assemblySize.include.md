### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       383.5KB |  +375.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       339.0KB |  +330.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       382.5KB |  +374.0KB |    +8.5KB |             +6.0KB |              +8.5KB |     +13.0KB |
| net462         |          7.0KB |       386.0KB |  +379.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.0KB |
| net47          |          7.0KB |       385.5KB |  +378.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       384.5KB |  +376.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       383.5KB |  +375.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       383.5KB |  +375.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       383.5KB |  +375.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       362.5KB |  +353.5KB |    +7.5KB |             +5.5KB |              +8.0KB |     +12.5KB |
| netcoreapp2.1  |          9.0KB |       342.0KB |  +333.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       342.0KB |  +333.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       340.5KB |  +331.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       338.5KB |  +329.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net5.0         |          9.5KB |       304.5KB |  +295.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net6.0         |         10.0KB |       245.5KB |  +235.5KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |       214.0KB |  +204.0KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       184.5KB |  +175.0KB |    +8.5KB |          +512bytes |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |       116.5KB |  +107.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        93.5KB |   +83.5KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net11.0        |         10.0KB |        21.0KB |   +11.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       558.5KB |  +550.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       486.9KB |  +478.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       558.5KB |  +550.0KB |   +16.2KB |             +7.7KB |             +13.4KB |     +18.4KB |
| net462         |          7.0KB |       562.0KB |  +555.0KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.4KB |
| net47          |          7.0KB |       561.3KB |  +554.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       559.9KB |  +551.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       557.8KB |  +549.3KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       557.8KB |  +549.3KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       557.8KB |  +549.3KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       526.9KB |  +517.9KB |   +15.2KB |             +7.2KB |             +12.9KB |     +17.9KB |
| netcoreapp2.1  |          9.0KB |       493.6KB |  +484.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       493.6KB |  +484.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       485.5KB |  +476.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       483.4KB |  +473.9KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net5.0         |          9.5KB |       432.0KB |  +422.5KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net6.0         |         10.0KB |       352.8KB |  +342.8KB |   +17.2KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       304.5KB |  +294.5KB |   +17.1KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       261.2KB |  +251.7KB |   +16.0KB |          +811bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |       168.5KB |  +159.0KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       136.1KB |  +126.1KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net11.0        |         10.0KB |        30.9KB |   +20.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
