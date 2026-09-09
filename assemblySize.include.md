### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       364.0KB |  +356.0KB |    +7.5KB |             +5.0KB |              +7.5KB |     +12.0KB |
| netstandard2.1 |          8.5KB |       318.0KB |  +309.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       362.5KB |  +354.0KB |    +7.5KB |             +5.5KB |              +8.0KB |     +12.0KB |
| net462         |          7.0KB |       366.0KB |  +359.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net47          |          7.0KB |       366.0KB |  +359.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       365.0KB |  +356.5KB |    +7.5KB |             +5.5KB |              +8.0KB |     +12.0KB |
| net472         |          8.5KB |       364.0KB |  +355.5KB |    +7.5KB |             +5.0KB |              +7.5KB |     +12.0KB |
| net48          |          8.5KB |       364.0KB |  +355.5KB |    +7.5KB |             +5.0KB |              +7.5KB |     +12.0KB |
| net481         |          8.5KB |       364.0KB |  +355.5KB |    +7.5KB |             +5.0KB |              +7.5KB |     +12.0KB |
| netcoreapp2.0  |          9.0KB |       341.5KB |  +332.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       321.5KB |  +312.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       321.5KB |  +312.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       315.5KB |  +306.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       314.0KB |  +304.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       278.0KB |  +268.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net6.0         |         10.0KB |       219.5KB |  +209.5KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |       181.0KB |  +171.0KB |   +12.0KB |             +8.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       152.0KB |  +142.5KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net9.0         |          9.5KB |       105.0KB |   +95.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        82.5KB |   +72.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        20.5KB |   +10.5KB |    +9.5KB |          +512bytes |              +1.0KB |      +4.0KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       534.0KB |  +526.0KB |   +15.2KB |             +6.7KB |             +12.4KB |     +17.4KB |
| netstandard2.1 |          8.5KB |       461.4KB |  +452.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       533.6KB |  +525.1KB |   +15.2KB |             +7.2KB |             +12.9KB |     +17.4KB |
| net462         |          7.0KB |       537.1KB |  +530.1KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net47          |          7.0KB |       536.8KB |  +529.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       535.5KB |  +527.0KB |   +15.2KB |             +7.2KB |             +12.9KB |     +17.4KB |
| net472         |          8.5KB |       533.4KB |  +524.9KB |   +15.2KB |             +6.7KB |             +12.4KB |     +17.4KB |
| net48          |          8.5KB |       533.4KB |  +524.9KB |   +15.2KB |             +6.7KB |             +12.4KB |     +17.4KB |
| net481         |          8.5KB |       533.4KB |  +524.9KB |   +15.2KB |             +6.7KB |             +12.4KB |     +17.4KB |
| netcoreapp2.0  |          9.0KB |       501.0KB |  +492.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       468.6KB |  +459.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       468.6KB |  +459.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       454.5KB |  +445.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       453.0KB |  +443.5KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       398.9KB |  +389.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net6.0         |         10.0KB |       320.4KB |  +310.4KB |   +17.2KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       263.1KB |  +253.1KB |   +19.6KB |             +9.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       218.7KB |  +209.2KB |   +16.0KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |          9.5KB |       150.6KB |  +141.1KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       119.4KB |  +109.4KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        30.4KB |   +20.4KB |   +17.0KB |          +512bytes |              +1.6KB |      +4.7KB |
