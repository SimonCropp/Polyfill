### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       423.0KB |  +415.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| netstandard2.1 |          8.5KB |       366.5KB |  +358.0KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.5KB |
| net461         |          8.5KB |       422.0KB |  +413.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net462         |          7.0KB |       425.5KB |  +418.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net47          |          7.0KB |       425.5KB |  +418.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net471         |          8.5KB |       425.0KB |  +416.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net472         |          8.5KB |       422.5KB |  +414.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net48          |          8.5KB |       422.5KB |  +414.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net481         |          8.5KB |       422.5KB |  +414.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       399.0KB |  +390.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| netcoreapp2.1  |          9.0KB |       374.0KB |  +365.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       374.0KB |  +365.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       364.0KB |  +354.5KB |    +7.5KB |             +5.0KB |              +7.5KB |     +12.5KB |
| netcoreapp3.1  |          9.5KB |       364.0KB |  +354.5KB |    +7.5KB |             +5.0KB |              +8.0KB |     +12.5KB |
| net5.0         |          9.5KB |       326.5KB |  +317.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net6.0         |         10.0KB |       264.0KB |  +254.0KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |       236.0KB |  +226.0KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.0KB |
| net8.0         |          9.5KB |       206.0KB |  +196.5KB |   +11.5KB |          +512bytes |           +512bytes |      +6.0KB |
| net9.0         |          9.5KB |       129.5KB |  +120.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |       103.5KB |   +93.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        17.5KB |    +7.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       616.4KB |  +608.4KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| netstandard2.1 |          8.5KB |       526.2KB |  +517.7KB |   +15.1KB |             +8.2KB |             +12.4KB |     +17.9KB |
| net461         |          8.5KB |       616.8KB |  +608.3KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net462         |          7.0KB |       620.3KB |  +613.3KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net47          |          7.0KB |       620.3KB |  +613.3KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net471         |          8.5KB |       619.2KB |  +610.7KB |   +16.1KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net472         |          8.5KB |       615.3KB |  +606.8KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net48          |          8.5KB |       615.3KB |  +606.8KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net481         |          8.5KB |       615.3KB |  +606.8KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp2.0  |          9.0KB |       581.1KB |  +572.1KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| netcoreapp2.1  |          9.0KB |       540.7KB |  +531.7KB |   +16.1KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       540.7KB |  +531.7KB |   +16.1KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       520.2KB |  +510.7KB |   +15.1KB |             +6.7KB |             +12.4KB |     +17.9KB |
| netcoreapp3.1  |          9.5KB |       520.2KB |  +510.7KB |   +15.1KB |             +6.7KB |             +12.9KB |     +17.9KB |
| net5.0         |          9.5KB |       464.5KB |  +455.0KB |   +16.1KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net6.0         |         10.0KB |       380.3KB |  +370.3KB |   +17.1KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       336.6KB |  +326.6KB |   +16.5KB |             +6.9KB |              +1.1KB |      +3.7KB |
| net8.0         |          9.5KB |       291.4KB |  +281.9KB |   +18.9KB |          +811bytes |              +1.1KB |      +6.7KB |
| net9.0         |          9.5KB |       185.7KB |  +176.2KB |   +15.9KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       148.9KB |  +138.9KB |   +15.9KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        25.7KB |   +15.7KB |   +16.4KB |                    |              +1.1KB |      +4.2KB |
