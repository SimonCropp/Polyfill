### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       358.5KB |  +350.5KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| netstandard2.1 |          8.5KB |       312.0KB |  +303.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       357.0KB |  +348.5KB |    +7.5KB |             +6.5KB |              +8.0KB |     +12.5KB |
| net462         |          7.0KB |       360.5KB |  +353.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       360.5KB |  +353.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       359.5KB |  +351.0KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net472         |          8.5KB |       358.0KB |  +349.5KB |    +8.0KB |             +6.5KB |              +8.0KB |     +12.5KB |
| net48          |          8.5KB |       358.0KB |  +349.5KB |    +8.0KB |             +6.5KB |              +8.0KB |     +12.5KB |
| net481         |          8.5KB |       358.0KB |  +349.5KB |    +8.0KB |             +6.5KB |              +8.0KB |     +12.5KB |
| netcoreapp2.0  |          9.0KB |       336.0KB |  +327.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       316.0KB |  +307.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       316.0KB |  +307.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       308.5KB |  +299.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       306.5KB |  +297.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       270.5KB |  +261.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       212.0KB |  +202.0KB |   +10.0KB |             +7.0KB |              +1.0KB |      +3.5KB |
| net7.0         |         10.0KB |       174.5KB |  +164.5KB |   +12.0KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       145.0KB |  +135.5KB |    +8.5KB |          +512bytes |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        98.0KB |   +88.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        76.0KB |   +66.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        20.5KB |   +10.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       524.4KB |  +516.4KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| netstandard2.1 |          8.5KB |       450.9KB |  +442.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       524.0KB |  +515.5KB |   +15.2KB |             +8.2KB |             +12.9KB |     +17.9KB |
| net462         |          7.0KB |       527.5KB |  +520.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       527.2KB |  +520.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       525.8KB |  +517.3KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net472         |          8.5KB |       523.3KB |  +514.8KB |   +15.7KB |             +8.2KB |             +12.9KB |     +17.9KB |
| net48          |          8.5KB |       523.3KB |  +514.8KB |   +15.7KB |             +8.2KB |             +12.9KB |     +17.9KB |
| net481         |          8.5KB |       523.3KB |  +514.8KB |   +15.7KB |             +8.2KB |             +12.9KB |     +17.9KB |
| netcoreapp2.0  |          9.0KB |       491.3KB |  +482.3KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       459.0KB |  +450.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       459.0KB |  +450.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       442.3KB |  +432.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       440.3KB |  +430.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       386.1KB |  +376.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       307.5KB |  +297.5KB |   +17.7KB |             +8.7KB |              +1.6KB |      +4.2KB |
| net7.0         |         10.0KB |       251.4KB |  +241.4KB |   +19.6KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       206.4KB |  +196.9KB |   +16.0KB |          +811bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |       138.3KB |  +128.8KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       108.0KB |   +98.0KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        30.3KB |   +20.3KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
