### Assembly Sizes

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       221.5KB |  +213.5KB |    +9.0KB |             +7.0KB |              +9.0KB |     +14.0KB |
| netstandard2.1 |          8.5KB |       177.0KB |  +168.5KB |   +12.0KB |             +9.5KB |             +12.0KB |     +17.0KB |
| net461         |          8.5KB |       228.0KB |  +219.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net462         |          7.0KB |       226.5KB |  +219.5KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net47          |          7.0KB |       226.5KB |  +219.5KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net471         |          8.5KB |       226.5KB |  +218.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net472         |          8.5KB |       225.0KB |  +216.5KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net48          |          8.5KB |       225.0KB |  +216.5KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| net481         |          8.5KB |       225.0KB |  +216.5KB |    +9.5KB |             +7.0KB |              +9.5KB |     +14.0KB |
| netcoreapp2.0  |          9.0KB |       205.0KB |  +196.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.1  |          9.0KB |       190.0KB |  +181.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp2.2  |          9.0KB |       190.0KB |  +181.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| netcoreapp3.0  |          9.5KB |       182.0KB |  +172.5KB |   +12.5KB |            +10.0KB |              +9.5KB |     +14.0KB |
| netcoreapp3.1  |          9.5KB |       180.5KB |  +171.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net5.0         |          9.5KB |       150.5KB |  +141.0KB |    +9.0KB |             +6.5KB |              +9.0KB |     +14.0KB |
| net6.0         |         10.0KB |       111.0KB |  +101.0KB |    +9.5KB |             +7.0KB |           +512bytes |      +3.0KB |
| net7.0         |         10.0KB |        84.0KB |   +74.0KB |    +9.5KB |             +5.5KB |           +512bytes |      +3.5KB |
| net8.0         |          9.5KB |        67.5KB |   +58.0KB |    +8.5KB |                    |           +512bytes |      +3.5KB |
| net9.0         |         10.0KB |        33.5KB |   +23.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |
| net10.0        |         10.0KB |        21.0KB |   +11.0KB |    +9.5KB |                    |              +1.0KB |      +4.0KB |
| net11.0        |         10.0KB |        16.5KB |    +6.5KB |    +9.0KB |                    |           +512bytes |      +3.5KB |


### Assembly Sizes with EmbedUntrackedSources

|                | Empty Assembly | With Polyfill | Diff      | Ensure    | ArgumentExceptions | StringInterpolation | Nullability |
|----------------|----------------|---------------|-----------|-----------|--------------------|---------------------|-------------|
| netstandard2.0 |          8.0KB |       335.3KB |  +327.3KB |   +16.9KB |             +8.7KB |             +14.0KB |     +19.7KB |
| netstandard2.1 |          8.5KB |       267.1KB |  +258.6KB |   +19.9KB |            +11.2KB |             +17.0KB |     +22.7KB |
| net461         |          8.5KB |       342.3KB |  +333.8KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net462         |          7.0KB |       340.8KB |  +333.8KB |   +17.4KB |             +8.7KB |             +14.5KB |     +19.7KB |
| net47          |          7.0KB |       340.5KB |  +333.5KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net471         |          8.5KB |       340.5KB |  +332.0KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net472         |          8.5KB |       337.9KB |  +329.4KB |   +17.4KB |             +8.7KB |             +14.5KB |     +19.7KB |
| net48          |          8.5KB |       337.9KB |  +329.4KB |   +17.4KB |             +8.7KB |             +14.5KB |     +19.7KB |
| net481         |          8.5KB |       337.9KB |  +329.4KB |   +17.4KB |             +8.7KB |             +14.5KB |     +19.7KB |
| netcoreapp2.0  |          9.0KB |       309.6KB |  +300.6KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| netcoreapp2.1  |          9.0KB |       285.2KB |  +276.2KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| netcoreapp2.2  |          9.0KB |       285.2KB |  +276.2KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| netcoreapp3.0  |          9.5KB |       268.2KB |  +258.7KB |   +20.4KB |            +11.7KB |             +14.5KB |     +19.7KB |
| netcoreapp3.1  |          9.5KB |       266.7KB |  +257.2KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net5.0         |          9.5KB |       220.4KB |  +210.9KB |   +16.9KB |             +8.2KB |             +14.0KB |     +19.7KB |
| net6.0         |         10.0KB |       166.0KB |  +156.0KB |   +17.4KB |             +8.7KB |              +1.1KB |      +3.7KB |
| net7.0         |         10.0KB |       122.7KB |  +112.7KB |   +17.3KB |             +6.9KB |              +1.1KB |      +4.2KB |
| net8.0         |          9.5KB |        97.1KB |   +87.6KB |   +16.1KB |          +299bytes |              +1.1KB |      +4.2KB |
| net9.0         |         10.0KB |        49.3KB |   +39.3KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
| net10.0        |         10.0KB |        32.1KB |   +22.1KB |   +17.1KB |                    |              +1.6KB |      +4.7KB |
| net11.0        |         10.0KB |        24.6KB |   +14.6KB |   +16.6KB |                    |              +1.1KB |      +4.2KB |
