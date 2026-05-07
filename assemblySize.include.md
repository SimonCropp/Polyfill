### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       306.5KB |  +298.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       259.0KB |  +250.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       305.0KB |  +296.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net462         |          7.0KB |       308.5KB |  +301.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net47          |          7.0KB |       308.5KB |  +301.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       307.5KB |  +299.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net472         |          8.5KB |       306.0KB |  +297.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net48          |          8.5KB |       306.0KB |  +297.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net481         |          8.5KB |       306.0KB |  +297.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       282.5KB |  +273.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       262.0KB |  +253.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       262.0KB |  +253.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       253.0KB |  +243.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       251.0KB |  +241.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       214.0KB |  +204.5KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.5KB |
| net6.0         |         10.0KB |       152.5KB |  +142.5KB |    +9.5KB |             +7.0KB |           +512bytes |      +4.0KB |
| net7.0         |         10.0KB |       118.0KB |  +108.0KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        89.5KB |   +80.0KB |    +8.5KB |          +512bytes |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        47.5KB |   +38.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        24.0KB |   +14.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        18.5KB |    +8.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       446.2KB |  +438.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       373.5KB |  +365.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       445.8KB |  +437.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net462         |          7.0KB |       449.3KB |  +442.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net47          |          7.0KB |       449.0KB |  +442.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       447.7KB |  +439.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net472         |          8.5KB |       445.1KB |  +436.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net48          |          8.5KB |       445.1KB |  +436.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net481         |          8.5KB |       445.1KB |  +436.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.0  |          9.0KB |       412.3KB |  +403.3KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       380.7KB |  +371.7KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       380.7KB |  +371.7KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       362.4KB |  +352.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       360.4KB |  +350.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       305.3KB |  +295.8KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.9KB |
| net6.0         |         10.0KB |       224.2KB |  +214.2KB |   +17.2KB |             +8.7KB |              +1.1KB |      +4.7KB |
| net7.0         |         10.0KB |       170.9KB |  +160.9KB |   +16.6KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       127.6KB |  +118.1KB |   +16.0KB |          +811bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        68.6KB |   +59.1KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        36.5KB |   +26.5KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        27.4KB |   +17.4KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
