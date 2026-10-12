### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       407.5KB |  +399.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       352.0KB |  +343.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       406.0KB |  +397.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net462         |          7.0KB |       409.5KB |  +402.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net47          |          7.0KB |       409.5KB |  +402.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net471         |          8.5KB |       408.5KB |  +400.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net472         |          8.5KB |       407.0KB |  +398.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net48          |          8.5KB |       407.0KB |  +398.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net481         |          8.5KB |       407.0KB |  +398.5KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.5KB |
| netcoreapp2.0  |          9.0KB |       385.0KB |  +376.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       361.0KB |  +352.0KB |    +7.5KB |             +5.5KB |              +8.0KB |     +13.0KB |
| netcoreapp2.2  |          9.0KB |       361.0KB |  +352.0KB |    +7.5KB |             +5.5KB |              +8.0KB |     +13.0KB |
| netcoreapp3.0  |          9.5KB |       349.5KB |  +340.0KB |    +7.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       349.5KB |  +340.0KB |    +7.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
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
| netstandard2.0 |          8.0KB |       595.0KB |  +587.0KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       507.3KB |  +498.8KB |   +16.6KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       594.5KB |  +586.0KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net462         |          7.0KB |       598.0KB |  +591.0KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net47          |          7.0KB |       598.0KB |  +591.0KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net471         |          8.5KB |       596.4KB |  +587.9KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net472         |          8.5KB |       593.8KB |  +585.3KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net48          |          8.5KB |       593.8KB |  +585.3KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net481         |          8.5KB |       593.8KB |  +585.3KB |   +17.1KB |             +8.7KB |             +14.4KB |     +19.9KB |
| netcoreapp2.0  |          9.0KB |       561.9KB |  +552.9KB |   +16.6KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       523.4KB |  +514.4KB |   +15.1KB |             +7.2KB |             +12.9KB |     +18.4KB |
| netcoreapp2.2  |          9.0KB |       523.4KB |  +514.4KB |   +15.1KB |             +7.2KB |             +12.9KB |     +18.4KB |
| netcoreapp3.0  |          9.5KB |       501.4KB |  +491.9KB |   +15.1KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       501.4KB |  +491.9KB |   +15.1KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       449.5KB |  +440.0KB |   +16.6KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       367.6KB |  +357.6KB |   +17.1KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       318.7KB |  +308.7KB |   +17.0KB |             +7.4KB |              +1.6KB |      +4.2KB |
| net8.0         |          9.5KB |       277.2KB |  +267.7KB |   +15.9KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |       181.3KB |  +171.8KB |   +15.4KB |                    |              +1.1KB |      +3.7KB |
| net10.0        |         10.0KB |       144.4KB |  +134.4KB |   +15.4KB |                    |              +1.1KB |      +3.7KB |
| net11.0        |         10.0KB |        25.7KB |   +15.7KB |   +16.4KB |                    |              +1.1KB |      +4.2KB |
