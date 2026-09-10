### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       368.0KB |  +360.0KB |    +7.5KB |             +5.5KB |              +7.5KB |     +12.0KB |
| netstandard2.1 |          8.5KB |       322.0KB |  +313.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       365.5KB |  +357.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       370.5KB |  +363.5KB |    +7.5KB |             +6.5KB |              +9.0KB |     +12.0KB |
| net47          |          7.0KB |       370.0KB |  +363.0KB |    +7.5KB |             +6.5KB |              +9.5KB |     +12.0KB |
| net471         |          8.5KB |       368.0KB |  +359.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       366.5KB |  +358.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       366.5KB |  +358.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       366.5KB |  +358.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       345.5KB |  +336.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       325.5KB |  +316.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       325.5KB |  +316.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       321.5KB |  +312.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       320.0KB |  +310.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       284.0KB |  +274.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net6.0         |         10.0KB |       225.5KB |  +215.5KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       191.0KB |  +181.0KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.0KB |
| net8.0         |          9.5KB |       161.0KB |  +151.5KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net9.0         |          9.5KB |       114.0KB |  +104.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        92.0KB |   +82.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        21.0KB |   +11.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       539.5KB |  +531.5KB |   +15.2KB |             +7.2KB |             +12.4KB |     +17.4KB |
| netstandard2.1 |          8.5KB |       466.9KB |  +458.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       538.1KB |  +529.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       543.1KB |  +536.1KB |   +15.2KB |             +8.2KB |             +13.9KB |     +17.4KB |
| net47          |          7.0KB |       542.3KB |  +535.3KB |   +15.2KB |             +8.2KB |             +14.4KB |     +17.4KB |
| net471         |          8.5KB |       540.0KB |  +531.5KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       537.4KB |  +528.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       537.4KB |  +528.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       537.4KB |  +528.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       506.5KB |  +497.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       474.1KB |  +465.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       474.1KB |  +465.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       463.4KB |  +453.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       461.9KB |  +452.4KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       407.8KB |  +398.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net6.0         |         10.0KB |       329.3KB |  +319.3KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       276.5KB |  +266.5KB |   +16.6KB |             +6.9KB |              +1.1KB |      +3.7KB |
| net8.0         |          9.5KB |       232.7KB |  +223.2KB |   +16.0KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |          9.5KB |       164.6KB |  +155.1KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       133.9KB |  +123.9KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        30.9KB |   +20.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
