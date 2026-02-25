### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       228.5KB |  +220.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       180.0KB |  +171.5KB |   +12.5KB |            +10.0KB |             +12.5KB |     +17.0KB |
| net461         |          8.5KB |       235.0KB |  +226.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       233.5KB |  +226.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net47          |          7.0KB |       233.5KB |  +226.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       233.5KB |  +225.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       232.0KB |  +223.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net48          |          8.5KB |       232.0KB |  +223.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net481         |          8.5KB |       232.0KB |  +223.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       208.5KB |  +199.5KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       187.5KB |  +178.5KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| netcoreapp2.2  |          9.0KB |       187.5KB |  +178.5KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| netcoreapp3.0  |          9.5KB |       184.0KB |  +174.5KB |   +12.0KB |             +9.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       182.5KB |  +173.0KB |   +12.0KB |             +9.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       152.5KB |  +143.0KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| net6.0         |         10.0KB |       112.5KB |  +102.5KB |   +10.0KB |             +7.0KB |              +1.0KB |      +3.5KB |
| net7.0         |         10.0KB |        85.5KB |   +75.5KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        69.0KB |   +59.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        35.0KB |   +25.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        22.5KB |   +12.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        19.0KB |    +9.0KB |    +9.0KB |                    |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       342.5KB |  +334.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       269.8KB |  +261.3KB |   +20.2KB |            +11.7KB |             +17.4KB |     +22.4KB |
| net461         |          8.5KB |       349.4KB |  +340.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       347.9KB |  +340.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net47          |          7.0KB |       347.7KB |  +340.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       347.7KB |  +339.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       345.1KB |  +336.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net48          |          8.5KB |       345.1KB |  +336.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net481         |          8.5KB |       345.1KB |  +336.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.0  |          9.0KB |       312.7KB |  +303.7KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       281.5KB |  +272.5KB |   +19.7KB |            +11.2KB |             +16.9KB |     +22.4KB |
| netcoreapp2.2  |          9.0KB |       281.5KB |  +272.5KB |   +19.7KB |            +11.2KB |             +16.9KB |     +22.4KB |
| netcoreapp3.0  |          9.5KB |       269.4KB |  +259.9KB |   +19.7KB |            +11.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       267.9KB |  +258.4KB |   +19.7KB |            +11.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       221.6KB |  +212.1KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| net6.0         |         10.0KB |       167.2KB |  +157.2KB |   +17.7KB |             +8.7KB |              +1.6KB |      +4.2KB |
| net7.0         |         10.0KB |       124.0KB |  +114.0KB |   +17.1KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |        98.5KB |   +89.0KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        51.0KB |   +41.5KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        33.9KB |   +23.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        28.5KB |   +18.5KB |   +16.5KB |                    |              +1.6KB |      +4.7KB |
