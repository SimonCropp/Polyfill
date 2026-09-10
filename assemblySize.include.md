### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       370.5KB |  +362.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       326.0KB |  +317.5KB |    +8.5KB |             +6.5KB |              +8.5KB |     +13.5KB |
| net461         |          8.5KB |       369.0KB |  +360.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net462         |          7.0KB |       374.0KB |  +367.0KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net47          |          7.0KB |       374.0KB |  +367.0KB |    +7.5KB |             +6.5KB |              +7.5KB |     +12.0KB |
| net471         |          8.5KB |       371.5KB |  +363.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       370.5KB |  +362.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.0KB |
| net48          |          8.5KB |       370.5KB |  +362.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.0KB |
| net481         |          8.5KB |       370.5KB |  +362.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.0KB |
| netcoreapp2.0  |          9.0KB |       349.0KB |  +340.0KB |    +8.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       329.5KB |  +320.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       329.5KB |  +320.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.0  |          9.5KB |       325.5KB |  +316.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       323.5KB |  +314.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net5.0         |          9.5KB |       287.5KB |  +278.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net6.0         |         10.0KB |       229.0KB |  +219.0KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       195.5KB |  +185.5KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       165.5KB |  +156.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |       115.5KB |  +106.0KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net10.0        |         10.0KB |        93.5KB |   +83.5KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net11.0        |         10.0KB |        21.0KB |   +11.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       543.3KB |  +535.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       472.2KB |  +463.7KB |   +16.2KB |             +8.2KB |             +13.4KB |     +18.9KB |
| net461         |          8.5KB |       542.8KB |  +534.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net462         |          7.0KB |       547.8KB |  +540.8KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net47          |          7.0KB |       547.6KB |  +540.6KB |   +15.2KB |             +8.2KB |             +12.4KB |     +17.4KB |
| net471         |          8.5KB |       544.7KB |  +536.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       542.7KB |  +534.2KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.4KB |
| net48          |          8.5KB |       542.7KB |  +534.2KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.4KB |
| net481         |          8.5KB |       542.7KB |  +534.2KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.4KB |
| netcoreapp2.0  |          9.0KB |       511.2KB |  +502.2KB |   +15.7KB |             +8.7KB |             +14.4KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       479.4KB |  +470.4KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       479.4KB |  +470.4KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.0  |          9.5KB |       468.7KB |  +459.2KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       466.7KB |  +457.2KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net5.0         |          9.5KB |       412.6KB |  +403.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net6.0         |         10.0KB |       334.1KB |  +324.1KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       282.8KB |  +272.8KB |   +17.1KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       239.0KB |  +229.5KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |       166.8KB |  +157.3KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net10.0        |         10.0KB |       136.1KB |  +126.1KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net11.0        |         10.0KB |        30.9KB |   +20.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
