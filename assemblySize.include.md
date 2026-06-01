### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       326.5KB |  +318.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       280.0KB |  +271.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       325.5KB |  +317.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net462         |          7.0KB |       329.0KB |  +322.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net47          |          7.0KB |       328.5KB |  +321.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       328.0KB |  +319.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       326.5KB |  +318.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net48          |          8.5KB |       326.5KB |  +318.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net481         |          8.5KB |       326.5KB |  +318.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       303.0KB |  +294.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       282.5KB |  +273.5KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       282.5KB |  +273.5KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       274.0KB |  +264.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       272.5KB |  +263.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       236.0KB |  +226.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       177.0KB |  +167.0KB |   +10.0KB |             +7.0KB |           +512bytes |      +4.0KB |
| net7.0         |         10.0KB |       139.5KB |  +129.5KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       111.0KB |  +101.5KB |    +8.0KB |                    |           +512bytes |      +3.0KB |
| net9.0         |          9.5KB |        67.0KB |   +57.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        43.5KB |   +33.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        20.5KB |   +10.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       474.9KB |  +466.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       403.2KB |  +394.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       474.9KB |  +466.4KB |   +16.2KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net462         |          7.0KB |       478.4KB |  +471.4KB |   +16.2KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net47          |          7.0KB |       477.7KB |  +470.7KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       476.8KB |  +468.3KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       474.3KB |  +465.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net48          |          8.5KB |       474.3KB |  +465.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net481         |          8.5KB |       474.3KB |  +465.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.0  |          9.0KB |       441.6KB |  +432.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       409.9KB |  +400.9KB |   +16.7KB |             +8.7KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       409.9KB |  +400.9KB |   +16.7KB |             +8.7KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       392.1KB |  +382.6KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       390.6KB |  +381.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       335.9KB |  +326.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       256.9KB |  +246.9KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.7KB |
| net7.0         |         10.0KB |       200.8KB |  +190.8KB |   +16.6KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       157.1KB |  +147.6KB |   +15.5KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |          9.5KB |        92.7KB |   +83.2KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        60.7KB |   +50.7KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        30.3KB |   +20.3KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
