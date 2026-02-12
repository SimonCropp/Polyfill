### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       217.5KB |  +209.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       172.5KB |  +164.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       224.0KB |  +215.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       222.5KB |  +215.5KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net47          |          7.0KB |       222.5KB |  +215.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       222.5KB |  +214.0KB |    +9.0KB |             +7.0KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       221.0KB |  +212.5KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net48          |          8.5KB |       221.0KB |  +212.5KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| net481         |          8.5KB |       221.0KB |  +212.5KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       201.0KB |  +192.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       183.0KB |  +174.0KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| netcoreapp2.2  |          9.0KB |       183.0KB |  +174.0KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| netcoreapp3.0  |          9.5KB |       178.0KB |  +168.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       176.5KB |  +167.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       146.0KB |  +136.5KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       107.5KB |   +97.5KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |        80.5KB |   +70.5KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        63.5KB |   +54.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |         10.0KB |        29.0KB |   +19.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        16.5KB |    +6.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        16.5KB |    +6.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       329.0KB |  +321.0KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| netstandard2.1 |          8.5KB |       260.3KB |  +251.8KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net461         |          8.5KB |       336.0KB |  +327.5KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net462         |          7.0KB |       334.5KB |  +327.5KB |   +17.4KB |             +8.7KB |             +14.5KB |     +19.7KB |
| net47          |          7.0KB |       334.2KB |  +327.2KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net471         |          8.5KB |       334.2KB |  +325.7KB |   +16.9KB |             +8.7KB |             +14.0KB |     +19.2KB |
| net472         |          8.5KB |       331.6KB |  +323.1KB |   +17.4KB |             +8.7KB |             +14.5KB |     +19.7KB |
| net48          |          8.5KB |       331.6KB |  +323.1KB |   +16.9KB |             +8.7KB |             +14.0KB |     +19.7KB |
| net481         |          8.5KB |       331.6KB |  +323.1KB |   +17.4KB |             +8.7KB |             +14.5KB |     +19.7KB |
| netcoreapp2.0  |          9.0KB |       303.3KB |  +294.3KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| netcoreapp2.1  |          9.0KB |       275.8KB |  +266.8KB |   +19.9KB |            +11.2KB |             +17.0KB |     +22.7KB |
| netcoreapp2.2  |          9.0KB |       275.8KB |  +266.8KB |   +19.9KB |            +11.2KB |             +17.0KB |     +22.7KB |
| netcoreapp3.0  |          9.5KB |       261.9KB |  +252.4KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| netcoreapp3.1  |          9.5KB |       260.4KB |  +250.9KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.2KB |
| net5.0         |          9.5KB |       213.6KB |  +204.1KB |   +17.4KB |             +8.2KB |             +14.5KB |     +19.7KB |
| net6.0         |         10.0KB |       160.4KB |  +150.4KB |   +17.4KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       117.1KB |  +107.1KB |   +17.3KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |        90.7KB |   +81.2KB |   +16.1KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |         10.0KB |        42.1KB |   +32.1KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        24.6KB |   +14.6KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        24.6KB |   +14.6KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
