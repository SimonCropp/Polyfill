### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       365.5KB |  +357.5KB |    +7.5KB |             +5.0KB |              +7.5KB |     +12.0KB |
| netstandard2.1 |          8.5KB |       319.5KB |  +311.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net461         |          8.5KB |       364.5KB |  +356.0KB |    +7.5KB |             +5.0KB |              +7.5KB |     +12.0KB |
| net462         |          7.0KB |       368.0KB |  +361.0KB |    +7.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net47          |          7.0KB |       367.5KB |  +360.5KB |    +7.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net471         |          8.5KB |       365.5KB |  +357.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net472         |          8.5KB |       365.5KB |  +357.0KB |    +7.5KB |             +5.0KB |              +7.5KB |     +12.0KB |
| net48          |          8.5KB |       365.5KB |  +357.0KB |    +7.5KB |             +5.0KB |              +7.5KB |     +12.0KB |
| net481         |          8.5KB |       365.5KB |  +357.0KB |    +7.5KB |             +5.0KB |              +7.5KB |     +12.0KB |
| netcoreapp2.0  |          9.0KB |       343.0KB |  +334.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.1  |          9.0KB |       323.0KB |  +314.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp2.2  |          9.0KB |       323.0KB |  +314.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       319.0KB |  +309.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| netcoreapp3.1  |          9.5KB |       317.5KB |  +308.0KB |    +8.5KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       281.5KB |  +272.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net6.0         |         10.0KB |       223.0KB |  +213.0KB |    +9.5KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       188.0KB |  +178.0KB |    +9.5KB |             +6.0KB |              +1.0KB |      +3.5KB |
| net8.0         |          9.5KB |       158.5KB |  +149.0KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net9.0         |          9.5KB |       111.5KB |  +102.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        89.5KB |   +79.5KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net11.0        |         10.0KB |        21.0KB |   +11.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       536.9KB |  +528.9KB |   +15.2KB |             +6.7KB |             +12.4KB |     +17.4KB |
| netstandard2.1 |          8.5KB |       464.3KB |  +455.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net461         |          8.5KB |       536.9KB |  +528.4KB |   +15.2KB |             +6.7KB |             +12.4KB |     +17.4KB |
| net462         |          7.0KB |       540.4KB |  +533.4KB |   +15.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net47          |          7.0KB |       539.7KB |  +532.7KB |   +15.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net471         |          8.5KB |       537.3KB |  +528.8KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net472         |          8.5KB |       536.2KB |  +527.7KB |   +15.2KB |             +6.7KB |             +12.4KB |     +17.4KB |
| net48          |          8.5KB |       536.2KB |  +527.7KB |   +15.2KB |             +6.7KB |             +12.4KB |     +17.4KB |
| net481         |          8.5KB |       536.2KB |  +527.7KB |   +15.2KB |             +6.7KB |             +12.4KB |     +17.4KB |
| netcoreapp2.0  |          9.0KB |       503.8KB |  +494.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.1  |          9.0KB |       471.5KB |  +462.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp2.2  |          9.0KB |       471.5KB |  +462.5KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       460.8KB |  +451.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| netcoreapp3.1  |          9.5KB |       459.2KB |  +449.7KB |   +16.2KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       405.1KB |  +395.6KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net6.0         |         10.0KB |       326.7KB |  +316.7KB |   +17.2KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       273.4KB |  +263.4KB |   +17.1KB |             +7.4KB |              +1.6KB |      +4.2KB |
| net8.0         |          9.5KB |       230.0KB |  +220.5KB |   +16.0KB |          +299bytes |              +1.1KB |      +3.7KB |
| net9.0         |          9.5KB |       161.9KB |  +152.4KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       131.2KB |  +121.2KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net11.0        |         10.0KB |        30.9KB |   +20.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
