### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       327.0KB |  +319.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       280.0KB |  +271.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net461         |          8.5KB |       325.5KB |  +317.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net462         |          7.0KB |       329.0KB |  +322.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net47          |          7.0KB |       329.0KB |  +322.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       328.0KB |  +319.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net472         |          8.5KB |       327.0KB |  +318.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       327.0KB |  +318.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       327.0KB |  +318.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       303.0KB |  +294.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       282.5KB |  +273.5KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       283.0KB |  +274.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       274.5KB |  +265.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       272.5KB |  +263.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net5.0         |          9.5KB |       236.0KB |  +226.5KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net6.0         |         10.0KB |       177.0KB |  +167.0KB |   +10.0KB |             +7.5KB |              +1.0KB |      +4.0KB |
| net7.0         |         10.0KB |       139.5KB |  +129.5KB |    +9.5KB |             +5.5KB |           +512bytes |      +4.0KB |
| net8.0         |          9.5KB |       111.0KB |  +101.5KB |    +8.5KB |          +512bytes |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |        67.5KB |   +58.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        43.5KB |   +33.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        20.5KB |   +10.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       476.3KB |  +468.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       404.1KB |  +395.6KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net461         |          8.5KB |       475.8KB |  +467.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net462         |          7.0KB |       479.3KB |  +472.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net47          |          7.0KB |       479.1KB |  +472.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       477.7KB |  +469.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net472         |          8.5KB |       475.7KB |  +467.2KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       475.7KB |  +467.2KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       475.7KB |  +467.2KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       442.5KB |  +433.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       410.8KB |  +401.8KB |   +16.7KB |             +8.7KB |             +14.4KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       411.3KB |  +402.3KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       393.5KB |  +384.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       391.5KB |  +382.0KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net5.0         |          9.5KB |       336.8KB |  +327.3KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net6.0         |         10.0KB |       257.8KB |  +247.8KB |   +17.7KB |             +9.2KB |              +1.6KB |      +4.7KB |
| net7.0         |         10.0KB |       201.8KB |  +191.8KB |   +17.1KB |             +6.9KB |              +1.1KB |      +4.7KB |
| net8.0         |          9.5KB |       158.0KB |  +148.5KB |   +16.0KB |          +811bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |        94.1KB |   +84.6KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        61.2KB |   +51.2KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        30.8KB |   +20.8KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
