### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       433.5KB |  +425.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| netstandard2.1 |          8.5KB |       376.0KB |  +367.5KB |    +8.5KB |             +6.0KB |              +8.5KB |     +13.5KB |
| net461         |          8.5KB |       431.5KB |  +423.0KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net462         |          7.0KB |       435.0KB |  +428.0KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net47          |          7.0KB |       435.0KB |  +428.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net471         |          8.5KB |       435.0KB |  +426.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net472         |          8.5KB |       433.0KB |  +424.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net48          |          8.5KB |       433.0KB |  +424.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net481         |          8.5KB |       433.0KB |  +424.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| netcoreapp2.0  |          9.0KB |       409.5KB |  +400.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| netcoreapp2.1  |          9.0KB |       384.5KB |  +375.5KB |    +8.5KB |             +6.5KB |              +9.5KB |     +14.5KB |
| netcoreapp2.2  |          9.0KB |       384.5KB |  +375.5KB |    +8.5KB |             +6.5KB |              +9.5KB |     +14.5KB |
| netcoreapp3.0  |          9.5KB |       377.0KB |  +367.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       377.0KB |  +367.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       340.5KB |  +331.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net6.0         |         10.0KB |       278.0KB |  +268.0KB |    +9.5KB |             +7.0KB |              +1.0KB |      +3.5KB |
| net7.0         |         10.0KB |       254.5KB |  +244.5KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       236.0KB |  +226.5KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net9.0         |          9.5KB |       157.0KB |  +147.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |       131.0KB |  +121.0KB |    +8.5KB |                    |              +1.0KB |      +3.5KB |
| net11.0        |         10.0KB |        18.0KB |    +8.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       633.1KB |  +625.1KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| netstandard2.1 |          8.5KB |       542.0KB |  +533.5KB |   +16.1KB |             +7.7KB |             +13.4KB |     +18.9KB |
| net461         |          8.5KB |       631.8KB |  +623.3KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net462         |          7.0KB |       635.3KB |  +628.3KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net47          |          7.0KB |       635.3KB |  +628.3KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net471         |          8.5KB |       635.4KB |  +626.9KB |   +16.6KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net472         |          8.5KB |       632.0KB |  +623.5KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net48          |          8.5KB |       632.0KB |  +623.5KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net481         |          8.5KB |       632.0KB |  +623.5KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| netcoreapp2.0  |          9.0KB |       597.8KB |  +588.8KB |   +16.6KB |             +8.2KB |             +14.4KB |     +19.9KB |
| netcoreapp2.1  |          9.0KB |       557.5KB |  +548.5KB |   +16.1KB |             +8.2KB |             +14.4KB |     +19.9KB |
| netcoreapp2.2  |          9.0KB |       557.5KB |  +548.5KB |   +16.1KB |             +8.2KB |             +14.4KB |     +19.9KB |
| netcoreapp3.0  |          9.5KB |       541.6KB |  +532.1KB |   +16.1KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       541.6KB |  +532.1KB |   +16.1KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       486.8KB |  +477.3KB |   +16.1KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net6.0         |         10.0KB |       402.6KB |  +392.6KB |   +17.1KB |             +8.7KB |              +1.6KB |      +4.2KB |
| net7.0         |         10.0KB |       365.0KB |  +355.0KB |   +16.5KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       334.3KB |  +324.8KB |   +15.9KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |          9.5KB |       226.5KB |  +217.0KB |   +15.9KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       189.9KB |  +179.9KB |   +15.9KB |                    |              +1.6KB |      +4.2KB |
| net11.0        |         10.0KB |        26.5KB |   +16.5KB |   +16.4KB |                    |              +1.1KB |      +4.2KB |
