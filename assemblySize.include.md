### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       406.5KB |  +398.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| netstandard2.1 |          8.5KB |       351.5KB |  +343.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       405.0KB |  +396.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net462         |          7.0KB |       408.5KB |  +401.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net47          |          7.0KB |       408.5KB |  +401.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net471         |          8.5KB |       407.5KB |  +399.0KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net472         |          8.5KB |       406.5KB |  +398.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net48          |          8.5KB |       406.5KB |  +398.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net481         |          8.5KB |       406.5KB |  +398.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       384.0KB |  +375.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       361.5KB |  +352.5KB |    +7.5KB |             +5.5KB |              +8.0KB |     +13.0KB |
| netcoreapp2.2  |          9.0KB |       361.5KB |  +352.5KB |    +7.5KB |             +5.5KB |              +8.0KB |     +13.0KB |
| netcoreapp3.0  |          9.5KB |       350.0KB |  +340.5KB |    +8.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       350.0KB |  +340.5KB |    +8.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net5.0         |          9.5KB |       315.5KB |  +306.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net6.0         |         10.0KB |       255.0KB |  +245.0KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |       224.5KB |  +214.5KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       195.5KB |  +186.0KB |    +8.5KB |          +512bytes |              +1.0KB |      +3.5KB |
| net9.0         |          9.5KB |       126.0KB |  +116.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |       100.0KB |   +90.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        17.5KB |    +7.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       593.8KB |  +585.8KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| netstandard2.1 |          8.5KB |       506.6KB |  +498.1KB |   +16.6KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       593.3KB |  +584.8KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net462         |          7.0KB |       596.8KB |  +589.8KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net47          |          7.0KB |       596.8KB |  +589.8KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net471         |          8.5KB |       595.2KB |  +586.7KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net472         |          8.5KB |       593.2KB |  +584.7KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net48          |          8.5KB |       593.2KB |  +584.7KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net481         |          8.5KB |       593.2KB |  +584.7KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp2.0  |          9.0KB |       560.7KB |  +551.7KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       523.9KB |  +514.9KB |   +15.1KB |             +7.2KB |             +12.9KB |     +18.4KB |
| netcoreapp2.2  |          9.0KB |       523.9KB |  +514.9KB |   +15.1KB |             +7.2KB |             +12.9KB |     +18.4KB |
| netcoreapp3.0  |          9.5KB |       502.0KB |  +492.5KB |   +15.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       502.0KB |  +492.5KB |   +15.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net5.0         |          9.5KB |       449.4KB |  +439.9KB |   +16.1KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net6.0         |         10.0KB |       367.6KB |  +357.6KB |   +17.1KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       320.2KB |  +310.2KB |   +17.0KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       276.7KB |  +267.2KB |   +15.9KB |          +811bytes |              +1.6KB |      +4.2KB |
| net9.0         |          9.5KB |       180.7KB |  +171.2KB |   +15.9KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       143.9KB |  +133.9KB |   +15.9KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        25.7KB |   +15.7KB |   +16.4KB |                    |              +1.1KB |      +4.2KB |
