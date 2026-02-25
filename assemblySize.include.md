### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       228.0KB |  +220.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       180.0KB |  +171.5KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| net461         |          8.5KB |       234.5KB |  +226.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       233.0KB |  +226.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net47          |          7.0KB |       233.0KB |  +226.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       233.0KB |  +224.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net472         |          8.5KB |       231.5KB |  +223.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net48          |          8.5KB |       231.5KB |  +223.0KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net481         |          8.5KB |       231.5KB |  +223.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       208.5KB |  +199.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       187.0KB |  +178.0KB |   +12.0KB |            +10.0KB |             +12.0KB |     +17.0KB |
| netcoreapp2.2  |          9.0KB |       187.0KB |  +178.0KB |   +12.0KB |            +10.0KB |             +12.5KB |     +17.0KB |
| netcoreapp3.0  |          9.5KB |       183.5KB |  +174.0KB |   +12.5KB |            +10.0KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       182.0KB |  +172.5KB |    +9.0KB |             +9.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       152.0KB |  +142.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       112.5KB |  +102.5KB |    +9.5KB |             +7.0KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |        85.0KB |   +75.0KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        68.5KB |   +59.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        34.5KB |   +25.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        22.5KB |   +12.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        19.0KB |    +9.0KB |    +9.0KB |                    |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       341.9KB |  +333.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       269.7KB |  +261.2KB |   +19.7KB |            +11.2KB |             +16.9KB |     +22.4KB |
| net461         |          8.5KB |       348.9KB |  +340.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       347.4KB |  +340.4KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net47          |          7.0KB |       347.1KB |  +340.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       347.1KB |  +338.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net472         |          8.5KB |       344.5KB |  +336.0KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net48          |          8.5KB |       344.5KB |  +336.0KB |   +16.7KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net481         |          8.5KB |       344.5KB |  +336.0KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| netcoreapp2.0  |          9.0KB |       312.6KB |  +303.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       280.9KB |  +271.9KB |   +19.7KB |            +11.7KB |             +16.9KB |     +22.4KB |
| netcoreapp2.2  |          9.0KB |       280.9KB |  +271.9KB |   +19.7KB |            +11.7KB |             +17.4KB |     +22.4KB |
| netcoreapp3.0  |          9.5KB |       268.8KB |  +259.3KB |   +20.2KB |            +11.7KB |             +13.9KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       267.3KB |  +257.8KB |   +16.7KB |            +11.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       221.0KB |  +211.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       167.1KB |  +157.1KB |   +17.2KB |             +8.7KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       123.4KB |  +113.4KB |   +17.1KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |        97.9KB |   +88.4KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        50.5KB |   +41.0KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        33.9KB |   +23.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        28.5KB |   +18.5KB |   +16.5KB |                    |              +1.6KB |      +4.7KB |
