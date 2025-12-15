# Polyfill and TargetFrameworks

It is recommended that projects that consume Polyfill multi-target all TFMs that the project is expected to be consumed in.

As Polyfill is implemented as a source only nuget, the implementation for each polyfill is compiled into the IL of the consuming assembly. As a side-effect that implementation will continue to be used even if that assembly is executed in a runtime that has a more efficient implementation available.

As a result, in the context of a project producing nuget package, that project should target all frameworks from the lowest TargetFramework up to and including the current framework. 

For example:

 * If a nuget's minimum target is net6, then the resulting TargetFrameworks should also include net7.0 and net8.0
 * If a nuget's minimum target is net471, then the resulting TargetFrameworks should also include net472 and net48"


## Negative impacts

Failure to take the above approach will have several negative impacts:


### Performance

Some polyfills are implemented in a way that will not always have the equivalent performance (memory, CPU and IO) to the actual implementations.

For example the polyfill for `StringBuilder.Append(ReadOnlySpan<char>)` on netcore2 is:

```
public StringBuilder Append(ReadOnlySpan<char> value)
    => target.Append(value.ToString());
```

Which will result in a string allocation.


### Assembly Size

Assembly (and debug symbols) size is proportional to the amount of IL in an assembly. Failure to multi-target will result in extra redundant code (and IL) being included when bundled with an assembly that targets a higher TFM.

For example; given an empty assembly the size will be:

|                | Assembly | Symbols | Total |
|----------------|----------|---------|-------|
| net461         | 4 KB     | 8 KB    | 12 KB |
| netstandard2.0 | 4 KB     | 8 KB    | 12 KB |
| net9           | 4 KB     | 11 KB   | 15 KB |
| net10          | 4 KB     | 11 KB   | 15 KB |

If that project then pulls in all Polyfill features the resulting size will be:

|                | Assembly | Symbols | Total  |
|----------------|----------|---------|--------|
| net461         | 160 KB   | 69 KB   | 229 KB |
| netstandard2.0 | 165 KB   | 71 KB   | 236 KB |
| net9           | 27 KB    | 32 KB   | 59 KB  |
| net10          | 20 KB    | 30 KB   | 50 KB  |


### Assembly load time and memory usage

The time taken to load and JIT code is proportional to the amount of IL in that code. So a larger assembly, with more IL, will take longer to load an use more memory once loaded.


## MSbuild Warning

At build time, if a project is detected to not properly multi target, then a warning is recorded.


### Missing multi-target detection

<!-- snippet: MaxNetRequired -->
<a id='snippet-MaxNetRequired'></a>
```targets
<PropertyGroup>
  <MaxNetRequired>false</MaxNetRequired>
  <MaxNetRequired
    Condition="($(LowerFrameworks.Contains('net5')) OR
               $(LowerFrameworks.Contains('net6')) OR
               $(LowerFrameworks.Contains('net7')) OR
               $(LowerFrameworks.Contains('net8')) OR
               $(LowerFrameworks.Contains('netstandard')) OR
               $(LowerFrameworks.Contains('netcore')))
               AND
               !$(LowerFrameworks.Contains('net9'))
               AND
               !$(LowerFramework.Contains('net9'))"
    >true</MaxNetRequired>
  <MaxNetClassicRequired>false</MaxNetClassicRequired>
  <MaxNetClassicRequired
    Condition="(
                 $(LowerFrameworks.Contains('net4')) OR
                 $(LowerFrameworks.Contains('netstandard'))
               )
               AND
               !$(LowerFramework.Contains('net48'))
               AND
               !$(LowerFrameworks.Contains('net48'))"
    >true</MaxNetClassicRequired>
</PropertyGroup>
```
<sup><a href='/src/Polyfill/Polyfill.targets#L35-L62' title='Snippet source file'>snippet source</a> | <a href='#snippet-MaxNetRequired' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Missing multi-target warning

<!-- snippet: PolyfillValidateNugetTargets -->
<a id='snippet-PolyfillValidateNugetTargets'></a>
```targets
<Target
    Name="PolyfillValidateNugetTargets"
    AfterTargets="AfterBuild"
    Condition="$(NoWarn.Contains('PolyfillTargetsForNuget')) != true And '$(GeneratePackageOnBuild)' == 'true'">
  <Warning
      Code="PolyfillTargetsForNuget"
      Text="Projects that produce a nuget and consume Polyfill:
For best performance all frameworks from the lowest TargetFramework up to and including net48 should be targeted.
For example:
* If a nuget's minimum target is net47, then the resulting TargetFrameworks should include net471, net472, and net48.
* If a nuget's minimum target net6, then the resulting TargetFrameworks should include net7.0 and net8.0."
      HelpLink="https://github.com/SimonCropp/Polyfill#targetframeworks"
      Condition="$(MaxNetRequired)" />
</Target>
```
<sup><a href='/src/Polyfill/Polyfill.targets#L63-L78' title='Snippet source file'>snippet source</a> | <a href='#snippet-PolyfillValidateNugetTargets' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### Suppressing

The above warning can be suppressed using a `NoWarn` with `PolyfillTargetsForNuget`:

```
<NoWarn>$(NoWarn);PolyfillTargetsForNuget</NoWarn>
```


## See also

 * [PolyfillLib](polyfill-lib.md)
 * [Consuming and type visibility](consuming.md)
