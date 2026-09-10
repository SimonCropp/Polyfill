### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       373.5KB |  +365.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       327.0KB |  +318.5KB |    +8.5KB |             +6.5KB |              +8.5KB |     +13.5KB |
| net461         |          8.5KB |       372.5KB |  +364.0KB |    +8.5KB |             +6.0KB |              +8.5KB |     +13.0KB |
| net462         |          7.0KB |       377.5KB |  +370.5KB |    +7.0KB |             +6.5KB |              +7.5KB |     +11.5KB |
| net47          |          7.0KB |       377.0KB |  +370.0KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net471         |          8.5KB |       374.5KB |  +366.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       373.5KB |  +365.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       373.5KB |  +365.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       373.5KB |  +365.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       352.5KB |  +343.5KB |    +7.5KB |             +6.5KB |              +9.0KB |     +12.0KB |
| netcoreapp2.1  |          9.0KB |       330.5KB |  +321.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       330.5KB |  +321.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       326.5KB |  +317.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       324.5KB |  +315.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net5.0         |          9.5KB |       290.0KB |  +280.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       230.0KB |  +220.0KB |   +10.0KB |             +7.0KB |              +1.0KB |      +3.5KB |
| net7.0         |         10.0KB |       199.0KB |  +189.0KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.0KB |
| net8.0         |          9.5KB |       168.5KB |  +159.0KB |    +8.5KB |          +512bytes |              +1.0KB |      +3.5KB |
| net9.0         |          9.5KB |       116.0KB |  +106.5KB |    +8.0KB |                    |           +512bytes |      +3.0KB |
| net10.0        |         10.0KB |        93.5KB |   +83.5KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net11.0        |         10.0KB |        21.0KB |   +11.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       547.4KB |  +539.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       473.8KB |  +465.3KB |   +16.2KB |             +8.2KB |             +13.4KB |     +18.9KB |
| net461         |          8.5KB |       547.5KB |  +539.0KB |   +16.2KB |             +7.7KB |             +13.4KB |     +18.4KB |
| net462         |          7.0KB |       552.5KB |  +545.5KB |   +14.7KB |             +8.2KB |             +12.4KB |     +16.9KB |
| net47          |          7.0KB |       551.7KB |  +544.7KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net471         |          8.5KB |       548.9KB |  +540.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       546.8KB |  +538.3KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       546.8KB |  +538.3KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       546.8KB |  +538.3KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       515.9KB |  +506.9KB |   +15.2KB |             +8.2KB |             +13.9KB |     +17.4KB |
| netcoreapp2.1  |          9.0KB |       481.0KB |  +472.0KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       481.0KB |  +472.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       470.3KB |  +460.8KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       468.3KB |  +458.8KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net5.0         |          9.5KB |       416.3KB |  +406.8KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net6.0         |         10.0KB |       335.8KB |  +325.8KB |   +17.7KB |             +8.7KB |              +1.6KB |      +4.2KB |
| net7.0         |         10.0KB |       287.9KB |  +277.9KB |   +16.6KB |             +6.9KB |              +1.1KB |      +3.7KB |
| net8.0         |          9.5KB |       243.6KB |  +234.1KB |   +16.0KB |          +811bytes |              +1.6KB |      +4.2KB |
| net9.0         |          9.5KB |       167.6KB |  +158.1KB |   +15.5KB |                    |              +1.1KB |      +3.7KB |
| net10.0        |         10.0KB |       136.1KB |  +126.1KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net11.0        |         10.0KB |        30.9KB |   +20.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
