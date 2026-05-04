### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       306.0KB |  +298.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       258.0KB |  +249.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       303.5KB |  +295.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net462         |          7.0KB |       307.0KB |  +300.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net47          |          7.0KB |       307.0KB |  +300.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       306.5KB |  +298.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net472         |          8.5KB |       305.5KB |  +297.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       305.5KB |  +297.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       305.5KB |  +297.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       281.5KB |  +272.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       261.0KB |  +252.0KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       261.0KB |  +252.0KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       252.0KB |  +242.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       250.5KB |  +241.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       213.5KB |  +204.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       152.0KB |  +142.0KB |   +10.0KB |             +7.0KB |           +512bytes |      +4.0KB |
| net7.0         |         10.0KB |       117.5KB |  +107.5KB |    +9.0KB |             +5.5KB |           +512bytes |      +4.0KB |
| net8.0         |          9.5KB |        89.0KB |   +79.5KB |    +9.0KB |          +512bytes |              +1.0KB |      +3.5KB |
| net9.0         |          9.5KB |        47.0KB |   +37.5KB |    +9.0KB |                    |              +1.0KB |      +3.5KB |
| net10.0        |         10.0KB |        23.5KB |   +13.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        18.5KB |    +8.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       445.5KB |  +437.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       372.0KB |  +363.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       443.8KB |  +435.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net462         |          7.0KB |       447.3KB |  +440.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net47          |          7.0KB |       447.0KB |  +440.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       446.2KB |  +437.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net472         |          8.5KB |       444.1KB |  +435.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       444.1KB |  +435.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       444.1KB |  +435.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       410.9KB |  +401.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       379.2KB |  +370.2KB |   +16.7KB |             +8.7KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       379.2KB |  +370.2KB |   +16.7KB |             +8.7KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       360.9KB |  +351.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       359.4KB |  +349.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       304.3KB |  +294.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       223.4KB |  +213.4KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.7KB |
| net7.0         |         10.0KB |       170.2KB |  +160.2KB |   +16.6KB |             +6.9KB |              +1.1KB |      +4.7KB |
| net8.0         |          9.5KB |       126.8KB |  +117.3KB |   +16.5KB |          +811bytes |              +1.6KB |      +4.2KB |
| net9.0         |          9.5KB |        67.8KB |   +58.3KB |   +16.5KB |                    |              +1.6KB |      +4.2KB |
| net10.0        |         10.0KB |        35.8KB |   +25.8KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        27.4KB |   +17.4KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
