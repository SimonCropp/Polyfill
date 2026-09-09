### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       363.5KB |  +355.5KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| netstandard2.1 |          8.5KB |       317.5KB |  +309.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       362.0KB |  +353.5KB |    +7.5KB |             +5.5KB |              +8.0KB |     +12.0KB |
| net462         |          7.0KB |       365.5KB |  +358.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net47          |          7.0KB |       365.5KB |  +358.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       364.5KB |  +356.0KB |    +7.5KB |             +5.0KB |              +7.5KB |     +12.0KB |
| net472         |          8.5KB |       363.0KB |  +354.5KB |    +8.0KB |             +7.0KB |              +8.0KB |     +12.5KB |
| net48          |          8.5KB |       363.0KB |  +354.5KB |    +8.0KB |             +5.5KB |              +8.0KB |     +12.5KB |
| net481         |          8.5KB |       363.5KB |  +355.0KB |    +7.5KB |             +5.0KB |              +7.5KB |     +12.0KB |
| netcoreapp2.0  |          9.0KB |       341.0KB |  +332.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       321.0KB |  +312.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       321.0KB |  +312.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       315.0KB |  +305.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       313.0KB |  +303.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       277.5KB |  +268.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net6.0         |         10.0KB |       218.5KB |  +208.5KB |   +10.0KB |             +7.0KB |              +1.0KB |      +3.5KB |
| net7.0         |         10.0KB |       180.5KB |  +170.5KB |   +12.0KB |             +8.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       151.0KB |  +141.5KB |    +8.5KB |          +512bytes |              +1.0KB |      +3.5KB |
| net9.0         |          9.5KB |       104.5KB |   +95.0KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net10.0        |         10.0KB |        82.0KB |   +72.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        20.5KB |   +10.5KB |    +9.5KB |                    |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       533.1KB |  +525.1KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| netstandard2.1 |          8.5KB |       460.5KB |  +452.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       532.7KB |  +524.2KB |   +15.2KB |             +7.2KB |             +12.9KB |     +17.4KB |
| net462         |          7.0KB |       536.2KB |  +529.2KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net47          |          7.0KB |       535.9KB |  +528.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       534.6KB |  +526.1KB |   +15.2KB |             +6.7KB |             +12.4KB |     +17.4KB |
| net472         |          8.5KB |       532.0KB |  +523.5KB |   +15.7KB |             +8.7KB |             +12.9KB |     +17.9KB |
| net48          |          8.5KB |       532.0KB |  +523.5KB |   +15.7KB |             +7.2KB |             +12.9KB |     +17.9KB |
| net481         |          8.5KB |       532.5KB |  +524.0KB |   +15.2KB |             +6.7KB |             +12.4KB |     +17.4KB |
| netcoreapp2.0  |          9.0KB |       500.1KB |  +491.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       467.7KB |  +458.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       467.7KB |  +458.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       453.6KB |  +444.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       451.6KB |  +442.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       398.0KB |  +388.5KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net6.0         |         10.0KB |       319.0KB |  +309.0KB |   +17.7KB |             +8.7KB |              +1.6KB |      +4.2KB |
| net7.0         |         10.0KB |       262.2KB |  +252.2KB |   +19.6KB |             +9.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       217.3KB |  +207.8KB |   +16.0KB |          +811bytes |              +1.6KB |      +4.2KB |
| net9.0         |          9.5KB |       149.7KB |  +140.2KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net10.0        |         10.0KB |       118.5KB |  +108.5KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        30.4KB |   +20.4KB |   +17.0KB |                    |              +1.6KB |      +4.7KB |
