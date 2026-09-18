### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       390.5KB |  +382.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       345.0KB |  +336.5KB |    +8.5KB |             +6.5KB |              +8.5KB |     +13.5KB |
| net461         |          8.5KB |       389.0KB |  +380.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net462         |          7.0KB |       392.5KB |  +385.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net47          |          7.0KB |       392.5KB |  +385.5KB |    +8.5KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       391.5KB |  +383.0KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net472         |          8.5KB |       390.0KB |  +381.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| net48          |          8.5KB |       390.0KB |  +381.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net481         |          8.5KB |       390.0KB |  +381.5KB |    +9.0KB |             +6.5KB |              +9.5KB |     +14.5KB |
| netcoreapp2.0  |          9.0KB |       368.0KB |  +359.0KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       350.5KB |  +341.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       350.5KB |  +341.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       347.0KB |  +337.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       345.0KB |  +335.5KB |    +9.5KB |             +6.5KB |              +9.5KB |     +14.0KB |
| net5.0         |          9.5KB |       310.5KB |  +301.0KB |    +9.0KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net6.0         |         10.0KB |       252.0KB |  +242.0KB |    +9.5KB |             +6.5KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |       221.0KB |  +211.0KB |    +9.5KB |             +6.0KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |       191.5KB |  +182.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |          9.5KB |       120.0KB |  +110.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        95.5KB |   +85.5KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net11.0        |         10.0KB |        21.0KB |   +11.0KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       568.8KB |  +560.8KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netstandard2.1 |          8.5KB |       495.2KB |  +486.7KB |   +16.2KB |             +8.2KB |             +13.4KB |     +18.9KB |
| net461         |          8.5KB |       568.4KB |  +559.9KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net462         |          7.0KB |       571.9KB |  +564.9KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net47          |          7.0KB |       571.6KB |  +564.6KB |   +16.2KB |             +8.2KB |             +13.9KB |     +19.4KB |
| net471         |          8.5KB |       570.3KB |  +561.8KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net472         |          8.5KB |       567.7KB |  +559.2KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.9KB |
| net48          |          8.5KB |       567.7KB |  +559.2KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net481         |          8.5KB |       567.7KB |  +559.2KB |   +16.7KB |             +8.2KB |             +14.4KB |     +19.9KB |
| netcoreapp2.0  |          9.0KB |       535.8KB |  +526.8KB |   +16.7KB |             +8.7KB |             +14.4KB |     +19.4KB |
| netcoreapp2.1  |          9.0KB |       506.1KB |  +497.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp2.2  |          9.0KB |       506.1KB |  +497.1KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.0  |          9.5KB |       494.7KB |  +485.2KB |   +16.7KB |             +8.2KB |             +13.9KB |     +19.4KB |
| netcoreapp3.1  |          9.5KB |       492.7KB |  +483.2KB |   +17.2KB |             +8.2KB |             +14.4KB |     +19.4KB |
| net5.0         |          9.5KB |       440.7KB |  +431.2KB |   +16.7KB |             +8.7KB |             +14.4KB |     +19.4KB |
| net6.0         |         10.0KB |       362.0KB |  +352.0KB |   +17.2KB |             +8.2KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       314.6KB |  +304.6KB |   +17.1KB |             +7.4KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |       271.1KB |  +261.6KB |   +16.0KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |          9.5KB |       173.6KB |  +164.1KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |       139.1KB |  +129.1KB |   +16.0KB |                    |              +1.1KB |      +4.2KB |
| net11.0        |         10.0KB |        30.9KB |   +20.9KB |   +16.5KB |                    |              +1.1KB |      +4.2KB |
