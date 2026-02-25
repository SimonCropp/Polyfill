### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       228.0KB |  +220.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       180.0KB |  +171.5KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| net461         |          8.5KB |       234.5KB |  +226.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net462         |          7.0KB |       233.0KB |  +226.0KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
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
| netstandard2.0 |          8.0KB |       344.3KB |  +336.3KB |   +16.7KB |             +8.2KB |             +14.0KB |     +19.7KB |
| netstandard2.1 |          8.5KB |       271.4KB |  +262.9KB |   +19.7KB |            +11.2KB |             +17.0KB |     +22.7KB |
| net461         |          8.5KB |       351.3KB |  +342.8KB |   +16.7KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net462         |          7.0KB |       349.8KB |  +342.8KB |   +16.7KB |             +8.7KB |             +14.5KB |     +19.7KB |
| net47          |          7.0KB |       349.5KB |  +342.5KB |   +16.7KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net471         |          8.5KB |       349.5KB |  +341.0KB |   +16.7KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net472         |          8.5KB |       346.9KB |  +338.4KB |   +17.2KB |             +8.7KB |             +14.5KB |     +19.7KB |
| net48          |          8.5KB |       346.9KB |  +338.4KB |   +16.7KB |             +8.7KB |             +14.5KB |     +19.7KB |
| net481         |          8.5KB |       346.9KB |  +338.4KB |   +17.2KB |             +8.7KB |             +14.5KB |     +19.7KB |
| netcoreapp2.0  |          9.0KB |       314.8KB |  +305.8KB |   +16.7KB |             +8.2KB |             +14.0KB |     +19.2KB |
| netcoreapp2.1  |          9.0KB |       282.9KB |  +273.9KB |   +19.7KB |            +11.7KB |             +17.0KB |     +22.7KB |
| netcoreapp2.2  |          9.0KB |       282.9KB |  +273.9KB |   +19.7KB |            +11.7KB |             +17.5KB |     +22.7KB |
| netcoreapp3.0  |          9.5KB |       270.5KB |  +261.0KB |   +20.2KB |            +11.7KB |             +14.0KB |     +19.7KB |
| netcoreapp3.1  |          9.5KB |       268.9KB |  +259.4KB |   +16.7KB |            +11.2KB |             +14.0KB |     +19.7KB |
| net5.0         |          9.5KB |       222.7KB |  +213.2KB |   +16.7KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net6.0         |         10.0KB |       168.3KB |  +158.3KB |   +17.2KB |             +8.7KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       124.3KB |  +114.3KB |   +17.1KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |        98.7KB |   +89.2KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        50.9KB |   +41.4KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        34.1KB |   +24.1KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        28.7KB |   +18.7KB |   +16.5KB |                    |              +1.6KB |      +4.7KB |
