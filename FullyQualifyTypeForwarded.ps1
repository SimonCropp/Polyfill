# Script to fully qualify TypeForwardedTo attributes
# This makes both the attribute and the typeof() argument fully qualified

$files = Get-ChildItem -Path . -Recurse -Filter "*.cs" | Where-Object {
    $content = Get-Content $_.FullName -Raw
    $content -match "TypeForwardedTo"
}

Write-Host "Found $($files.Count) files with TypeForwardedTo"

foreach ($file in $files) {
    $content = Get-Content $file.FullName -Raw
    $modified = $false

    # Skip if already fully qualified (has System.Runtime.CompilerServices.TypeForwardedTo)
    if ($content -match '\[assembly:\s*System\.Runtime\.CompilerServices\.TypeForwardedTo') {
        Write-Host "Skipping $($file.Name) - already fully qualified"
        continue
    }

    # Pattern to find TypeForwardedTo with the type inside
    if ($content -match '\[assembly:\s*TypeForwardedTo\(typeof\(([^)]+)\)\)\]') {
        $typeInside = $Matches[1]

        # Find the namespace in the file
        $namespace = ""
        if ($content -match 'namespace\s+([\w\.]+)\s*;') {
            $namespace = $Matches[1]
        }

        Write-Host "Processing $($file.Name): Type=$typeInside, Namespace=$namespace"

        # Determine the fully qualified type name for typeof()
        $fullyQualifiedType = $typeInside
        if ($typeInside -notmatch '\.') {
            # Type is not qualified, so prepend namespace
            if ($namespace -ne "") {
                $fullyQualifiedType = "$namespace.$typeInside"
            }
        }

        # Replace the TypeForwardedTo line
        # Old: [assembly: TypeForwardedTo(typeof(TypeName))]
        # New: [assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof(Fully.Qualified.TypeName))]
        $oldPattern = '\[assembly:\s*TypeForwardedTo\(typeof\(' + [regex]::Escape($typeInside) + '\)\)\]'
        $newLine = "[assembly: System.Runtime.CompilerServices.TypeForwardedTo(typeof($fullyQualifiedType))]"

        $newContent = $content -replace $oldPattern, $newLine

        # Remove the "using System.Runtime.CompilerServices;" line if it appears right before TypeForwardedTo
        # This handles both with and without the #else before it
        $newContent = $newContent -replace 'using System\.Runtime\.CompilerServices;\s*\r?\n\s*\[assembly: System\.Runtime\.CompilerServices\.TypeForwardedTo', '[assembly: System.Runtime.CompilerServices.TypeForwardedTo'

        if ($newContent -ne $content) {
            Set-Content -Path $file.FullName -Value $newContent -NoNewline
            Write-Host "  Updated $($file.Name)"
            $modified = $true
        }
    }
}

Write-Host "`nDone!"
