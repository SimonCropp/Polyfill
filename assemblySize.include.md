### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       388.5KB |  +380.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netstandard2.1 |          8.5KB |       344.5KB |  +336.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net461         |          8.5KB |       387.0KB |  +378.5KB |    +9.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net462         |          7.0KB |       391.0KB |  +384.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       390.5KB |  +383.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net471         |          8.5KB |       389.5KB |  +381.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net472         |          8.5KB |       388.5KB |  +380.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net48          |          8.5KB |       388.5KB |  +380.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net481         |          8.5KB |       388.5KB |  +380.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.0  |          9.0KB |       366.5KB |  +357.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       348.5KB |  +339.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       348.5KB |  +339.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       346.5KB |  +337.0KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       345.0KB |  +335.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       310.5KB |  +301.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       251.5KB |  +241.5KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       221.0KB |  +211.0KB |    +9.0KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       191.5KB |  +182.0KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net9.0         |          9.5KB |       120.0KB |  +110.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        95.5KB |   +85.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        21.0KB |   +11.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       565.4KB |  +557.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netstandard2.1 |          8.5KB |       494.4KB |  +485.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net461         |          8.5KB |       564.9KB |  +556.4KB |   +17.2KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net462         |          7.0KB |       568.9KB |  +561.9KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       568.2KB |  +561.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net471         |          8.5KB |       566.8KB |  +558.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net472         |          8.5KB |       564.7KB |  +556.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net48          |          8.5KB |       564.7KB |  +556.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net481         |          8.5KB |       564.7KB |  +556.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.0  |          9.0KB |       532.8KB |  +523.8KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       502.6KB |  +493.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       502.6KB |  +493.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       494.0KB |  +484.5KB |   +17.2KB |             +8.7KB |             +14.4KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       492.4KB |  +482.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net5.0         |          9.5KB |       440.5KB |  +431.0KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net6.0         |         10.0KB |       361.3KB |  +351.3KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       314.3KB |  +304.3KB |   +16.6KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       271.0KB |  +261.5KB |   +16.0KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |          9.5KB |       173.6KB |  +164.1KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       139.1KB |  +129.1KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        30.9KB |   +20.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
