### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       410.0KB |  +402.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| netstandard2.1 |          8.5KB |       355.0KB |  +346.5KB |    +8.5KB |             +6.0KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       408.5KB |  +400.0KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net462         |          7.0KB |       412.0KB |  +405.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net47          |          7.0KB |       412.0KB |  +405.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net471         |          8.5KB |       411.0KB |  +402.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net472         |          8.5KB |       410.0KB |  +401.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net48          |          8.5KB |       410.0KB |  +401.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net481         |          8.5KB |       410.0KB |  +401.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       387.5KB |  +378.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       363.5KB |  +354.5KB |    +7.5KB |             +5.5KB |              +8.0KB |     +13.0KB |
| netcoreapp2.2  |          9.0KB |       364.0KB |  +355.0KB |    +7.5KB |             +5.0KB |              +7.5KB |     +12.5KB |
| netcoreapp3.0  |          9.5KB |       352.0KB |  +342.5KB |    +8.0KB |             +6.5KB |              +9.5KB |     +12.5KB |
| netcoreapp3.1  |          9.5KB |       352.0KB |  +342.5KB |    +8.0KB |             +6.5KB |              +9.5KB |     +12.5KB |
| net5.0         |          9.5KB |       315.5KB |  +306.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       255.0KB |  +245.0KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |       223.5KB |  +213.5KB |    +9.5KB |             +6.0KB |              +1.0KB |      +3.5KB |
| net8.0         |          9.5KB |       196.0KB |  +186.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |       126.5KB |  +117.0KB |    +8.0KB |                    |           +512bytes |      +3.0KB |
| net10.0        |         10.0KB |       100.5KB |   +90.5KB |    +8.0KB |                    |           +512bytes |      +3.0KB |
| net11.0        |         10.0KB |        17.5KB |    +7.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       597.7KB |  +589.7KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| netstandard2.1 |          8.5KB |       510.5KB |  +502.0KB |   +16.1KB |             +7.7KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       597.2KB |  +588.7KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net462         |          7.0KB |       600.7KB |  +593.7KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net47          |          7.0KB |       600.7KB |  +593.7KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net471         |          8.5KB |       599.1KB |  +590.6KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net472         |          8.5KB |       597.1KB |  +588.6KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net48          |          8.5KB |       597.1KB |  +588.6KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net481         |          8.5KB |       597.1KB |  +588.6KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp2.0  |          9.0KB |       564.7KB |  +555.7KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       526.1KB |  +517.1KB |   +15.1KB |             +7.2KB |             +12.9KB |     +18.4KB |
| netcoreapp2.2  |          9.0KB |       526.6KB |  +517.6KB |   +15.1KB |             +6.7KB |             +12.4KB |     +17.9KB |
| netcoreapp3.0  |          9.5KB |       504.1KB |  +494.6KB |   +15.6KB |             +8.2KB |             +14.4KB |     +17.9KB |
| netcoreapp3.1  |          9.5KB |       504.1KB |  +494.6KB |   +15.6KB |             +8.2KB |             +14.4KB |     +17.9KB |
| net5.0         |          9.5KB |       449.5KB |  +440.0KB |   +16.6KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       367.6KB |  +357.6KB |   +17.1KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       318.7KB |  +308.7KB |   +17.0KB |             +7.4KB |              +1.6KB |      +4.2KB |
| net8.0         |          9.5KB |       277.2KB |  +267.7KB |   +15.9KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |       181.3KB |  +171.8KB |   +15.4KB |                    |              +1.1KB |      +3.7KB |
| net10.0        |         10.0KB |       144.4KB |  +134.4KB |   +15.4KB |                    |              +1.1KB |      +3.7KB |
| net11.0        |         10.0KB |        25.7KB |   +15.7KB |   +16.4KB |                    |              +1.1KB |      +4.2KB |
