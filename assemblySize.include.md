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
| netcoreapp3.0  |          9.5KB |       317.0KB |  +307.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       315.5KB |  +306.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net5.0         |          9.5KB |       279.5KB |  +270.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +13.5KB |
| net6.0         |         10.0KB |       221.0KB |  +211.0KB |   +10.0KB |             +7.0KB |           +512bytes |      +3.5KB |
| net7.0         |         10.0KB |       183.5KB |  +173.5KB |   +12.0KB |             +8.5KB |              +3.5KB |      +6.0KB |
| net8.0         |          9.5KB |       156.5KB |  +147.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |       109.5KB |  +100.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        87.5KB |   +77.5KB |    +8.5KB |                    |           +512bytes |      +3.0KB |
| net11.0        |         10.0KB |        20.5KB |   +10.5KB |    +9.5KB |          +512bytes |              +1.0KB |      +4.0KB |


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
| netcoreapp3.0  |          9.5KB |       457.4KB |  +447.9KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       455.9KB |  +446.4KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net5.0         |          9.5KB |       401.8KB |  +392.3KB |   +16.7KB |             +8.2KB |             +13.9KB |     +18.9KB |
| net6.0         |         10.0KB |       323.3KB |  +313.3KB |   +17.7KB |             +8.7KB |              +1.1KB |      +4.2KB |
| net7.0         |         10.0KB |       267.5KB |  +257.5KB |   +19.6KB |             +9.9KB |              +4.1KB |      +6.7KB |
| net8.0         |          9.5KB |       226.7KB |  +217.2KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |       158.5KB |  +149.0KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       127.8KB |  +117.8KB |   +16.0KB |                    |              +1.1KB |      +3.7KB |
| net11.0        |         10.0KB |        30.4KB |   +20.4KB |   +17.0KB |          +512bytes |              +1.6KB |      +4.7KB |
