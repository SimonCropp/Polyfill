### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       423.0KB |  +415.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       366.0KB |  +357.5KB |    +7.5KB |             +6.5KB |              +8.0KB |     +12.5KB |
| net461         |          8.5KB |       422.0KB |  +413.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net462         |          7.0KB |       425.5KB |  +418.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net47          |          7.0KB |       425.5KB |  +418.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       424.5KB |  +416.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net472         |          8.5KB |       422.0KB |  +413.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net48          |          8.5KB |       422.0KB |  +413.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net481         |          8.5KB |       422.5KB |  +414.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       398.5KB |  +389.5KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.5KB |
| netcoreapp2.1  |          9.0KB |       373.5KB |  +364.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| netcoreapp2.2  |          9.0KB |       373.5KB |  +364.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| netcoreapp3.0  |          9.5KB |       363.5KB |  +354.0KB |    +7.5KB |             +5.5KB |              +8.0KB |     +12.5KB |
| netcoreapp3.1  |          9.5KB |       363.5KB |  +354.0KB |    +7.5KB |             +5.5KB |              +8.0KB |     +12.5KB |
| net5.0         |          9.5KB |       326.0KB |  +316.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       264.0KB |  +254.0KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |       236.0KB |  +226.0KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.0KB |
| net8.0         |          9.5KB |       206.0KB |  +196.5KB |   +11.5KB |          +512bytes |           +512bytes |      +6.0KB |
| net9.0         |          9.5KB |       129.5KB |  +120.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |       103.5KB |   +93.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        17.5KB |    +7.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       616.3KB |  +608.3KB |   +16.6KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       525.5KB |  +517.0KB |   +15.1KB |             +8.2KB |             +12.9KB |     +17.9KB |
| net461         |          8.5KB |       616.6KB |  +608.1KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net462         |          7.0KB |       620.1KB |  +613.1KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net47          |          7.0KB |       620.1KB |  +613.1KB |   +16.6KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       618.5KB |  +610.0KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net472         |          8.5KB |       614.7KB |  +606.2KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net48          |          8.5KB |       614.7KB |  +606.2KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net481         |          8.5KB |       615.2KB |  +606.7KB |   +16.1KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.0  |          9.0KB |       580.4KB |  +571.4KB |   +17.1KB |             +8.7KB |             +14.4KB |     +19.9KB |
| netcoreapp2.1  |          9.0KB |       540.1KB |  +531.1KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| netcoreapp2.2  |          9.0KB |       540.1KB |  +531.1KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| netcoreapp3.0  |          9.5KB |       519.6KB |  +510.1KB |   +15.1KB |             +7.2KB |             +12.9KB |     +17.9KB |
| netcoreapp3.1  |          9.5KB |       519.6KB |  +510.1KB |   +15.1KB |             +7.2KB |             +12.9KB |     +17.9KB |
| net5.0         |          9.5KB |       463.8KB |  +454.3KB |   +16.6KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       380.3KB |  +370.3KB |   +17.1KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       336.6KB |  +326.6KB |   +16.5KB |             +6.9KB |              +1.1KB |      +3.7KB |
| net8.0         |          9.5KB |       291.4KB |  +281.9KB |   +18.9KB |          +811bytes |              +1.1KB |      +6.7KB |
| net9.0         |          9.5KB |       185.7KB |  +176.2KB |   +15.9KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       148.9KB |  +138.9KB |   +15.9KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        25.7KB |   +15.7KB |   +16.4KB |                    |              +1.1KB |      +4.2KB |
