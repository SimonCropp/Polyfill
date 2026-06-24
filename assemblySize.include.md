### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       347.5KB |  +339.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       302.0KB |  +293.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       346.5KB |  +338.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       350.0KB |  +343.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       349.5KB |  +342.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net471         |          8.5KB |       349.0KB |  +340.5KB |    +9.0KB |             +6.0KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       347.5KB |  +339.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       347.5KB |  +339.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       347.5KB |  +339.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       323.5KB |  +314.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       304.5KB |  +295.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       304.5KB |  +295.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       297.0KB |  +287.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       295.5KB |  +286.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       259.0KB |  +249.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       201.0KB |  +191.0KB |   +10.0KB |             +6.5KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       163.5KB |  +153.5KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.0KB |
| net8.0         |          9.5KB |       135.0KB |  +125.5KB |    +8.0KB |                    |           +512bytes |      +3.0KB |
| net9.0         |          9.5KB |        88.5KB |   +79.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        66.0KB |   +56.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        27.0KB |   +17.0KB |    +9.0KB |                    |              +1.0KB |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       507.8KB |  +499.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       436.6KB |  +428.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       507.8KB |  +499.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       511.3KB |  +504.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       510.6KB |  +503.6KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net471         |          8.5KB |       509.7KB |  +501.2KB |   +16.7KB |             +7.7KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       507.2KB |  +498.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       507.2KB |  +498.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       507.2KB |  +498.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       473.2KB |  +464.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       442.8KB |  +433.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       442.8KB |  +433.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       426.5KB |  +417.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       425.0KB |  +415.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       370.3KB |  +360.8KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net6.0         |         10.0KB |       292.3KB |  +282.3KB |   +17.7KB |             +8.2KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       236.2KB |  +226.2KB |   +16.6KB |             +6.9KB |              +1.1KB |      +3.7KB |
| net8.0         |          9.5KB |       192.7KB |  +183.2KB |   +15.5KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |          9.5KB |       125.2KB |  +115.7KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        94.4KB |   +84.4KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        41.3KB |   +31.3KB |   +16.5KB |                    |              +1.6KB |      +4.2KB |
