### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       369.0KB |  +361.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       324.5KB |  +316.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       368.0KB |  +359.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       373.0KB |  +366.0KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net47          |          7.0KB |       372.5KB |  +365.5KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net471         |          8.5KB |       370.5KB |  +362.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.0KB |
| net472         |          8.5KB |       369.0KB |  +360.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       369.0KB |  +360.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       369.0KB |  +360.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       348.0KB |  +339.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       328.0KB |  +319.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       328.0KB |  +319.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       324.0KB |  +314.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       322.5KB |  +313.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       286.5KB |  +277.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       228.5KB |  +218.5KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       193.5KB |  +183.5KB |    +9.5KB |             +6.0KB |              +1.0KB |      +3.5KB |
| net8.0         |          9.5KB |       164.0KB |  +154.5KB |    +8.0KB |                    |           +512bytes |      +3.0KB |
| net9.0         |          9.5KB |       114.0KB |  +104.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        92.0KB |   +82.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        21.0KB |   +11.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       541.1KB |  +533.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       470.0KB |  +461.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       541.1KB |  +532.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       546.1KB |  +539.1KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net47          |          7.0KB |       545.4KB |  +538.4KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net471         |          8.5KB |       543.0KB |  +534.5KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.4KB |
| net472         |          8.5KB |       540.4KB |  +531.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       540.4KB |  +531.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       540.4KB |  +531.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       509.5KB |  +500.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       477.2KB |  +468.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       477.2KB |  +468.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       466.5KB |  +457.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       464.9KB |  +455.4KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       411.1KB |  +401.6KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net6.0         |         10.0KB |       333.2KB |  +323.2KB |   +17.2KB |             +8.2KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       279.9KB |  +269.9KB |   +17.1KB |             +7.4KB |              +1.6KB |      +4.2KB |
| net8.0         |          9.5KB |       236.5KB |  +227.0KB |   +15.5KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |          9.5KB |       164.6KB |  +155.1KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       133.9KB |  +123.9KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        30.9KB |   +20.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
