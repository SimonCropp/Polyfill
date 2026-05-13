### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       321.0KB |  +313.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       274.0KB |  +265.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       319.5KB |  +311.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net462         |          7.0KB |       323.5KB |  +316.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       323.0KB |  +316.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       322.0KB |  +313.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net472         |          8.5KB |       321.0KB |  +312.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       321.0KB |  +312.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       321.0KB |  +312.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       297.0KB |  +288.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       276.5KB |  +267.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       276.5KB |  +267.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       268.5KB |  +259.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       266.5KB |  +257.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       230.0KB |  +220.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       168.0KB |  +158.0KB |   +12.5KB |            +10.0KB |           +512bytes |      +4.0KB |
| net7.0         |         10.0KB |       133.5KB |  +123.5KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       105.0KB |   +95.5KB |    +8.5KB |          +512bytes |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        65.5KB |   +56.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        42.0KB |   +32.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        19.0KB |    +9.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       464.6KB |  +456.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       392.4KB |  +383.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       464.2KB |  +455.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net462         |          7.0KB |       468.2KB |  +461.2KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       467.4KB |  +460.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       466.0KB |  +457.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net472         |          8.5KB |       464.0KB |  +455.5KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       464.0KB |  +455.5KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       464.0KB |  +455.5KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       430.7KB |  +421.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       399.1KB |  +390.1KB |   +16.7KB |             +8.7KB |             +14.4KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       399.1KB |  +390.1KB |   +16.7KB |             +8.7KB |             +14.4KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       381.8KB |  +372.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       379.8KB |  +370.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       325.4KB |  +315.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       243.4KB |  +233.4KB |   +20.2KB |            +11.7KB |              +1.1KB |      +4.7KB |
| net7.0         |         10.0KB |       190.1KB |  +180.1KB |   +16.6KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       146.8KB |  +137.3KB |   +16.0KB |          +811bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        90.3KB |   +80.8KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        58.2KB |   +48.2KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        27.9KB |   +17.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
