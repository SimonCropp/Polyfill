param(
    [string]$TfmGroup
)

# Define TFM groups for parallel CI matrix
$tfmGroups = @{
    'netfx'   = @('net462', 'net472', 'net48')
    'netcore' = @('netcoreapp3.1', 'net5.0', 'net6.0', 'net6.0-windows', 'net7.0')
    'modern'  = @('net8.0', 'net9.0', 'net10.0')
}

# Get allowed TFMs based on group, or allow all if no group specified
$allowedTfms = if ($TfmGroup -and $tfmGroups.ContainsKey($TfmGroup)) {
    $tfmGroups[$TfmGroup]
} else {
    $null  # null means allow all
}

if ($TfmGroup) {
    Write-Host "Running tests for TFM group: $TfmGroup" -ForegroundColor Yellow
}

Get-ChildItem -Recurse -Filter "*Tests.csproj" -Path . | ForEach-Object {
    $proj = $_.FullName
    $projName = $_.Name
    [xml]$xml = Get-Content $proj

    # Collect all framework values from TargetFrameworks and TargetFramework elements
    $allFrameworks = @()
    foreach ($tf in $xml.Project.PropertyGroup.TargetFrameworks) {
        if ($tf -is [string]) { $allFrameworks += $tf }
        elseif ($tf) { $allFrameworks += $tf.InnerText }
    }
    foreach ($tf in $xml.Project.PropertyGroup.TargetFramework) {
        if ($tf -is [string]) { $allFrameworks += $tf }
        elseif ($tf) { $allFrameworks += $tf.InnerText }
    }

    # Split by semicolon, filter out empty and MSBuild variables
    $frameworks = ($allFrameworks -join ';') -split ';' | Where-Object { $_ -and $_ -notmatch '^\$\(' }

    # Filter by TFM group if specified
    if ($allowedTfms) {
        $frameworks = $frameworks | Where-Object { $_ -in $allowedTfms }
    }

    foreach ($fw in $frameworks) {
        Write-Host "Running $projName [$fw]" -ForegroundColor Cyan
        dotnet run --project $proj --configuration Release --framework $fw --no-build
        if ($LASTEXITCODE -ne 0) { throw "Tests failed for $projName [$fw]" }
    }
}
