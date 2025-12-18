function Run-TestProject($proj) {
    $projName = (Get-Item $proj).Name
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

    foreach ($fw in $frameworks) {
        Write-Host "Running $projName [$fw]" -ForegroundColor Cyan
        dotnet run --project $proj --configuration Release --framework $fw --no-build
        if ($LASTEXITCODE -ne 0) { throw "Tests failed for $projName [$fw]" }
    }
}

# Run ApiBuilderTests first
$apiBuilderTests = Get-ChildItem -Recurse -Filter "ApiBuilderTests.csproj" -Path .
if ($apiBuilderTests) {
    Run-TestProject $apiBuilderTests.FullName

    # Check for git diffs after ApiBuilderTests
    $gitDiff = git diff --stat HEAD
    if ($gitDiff) {
        Write-Host "Git diffs detected after ApiBuilderTests:" -ForegroundColor Red
        Write-Host $gitDiff
        throw "Build failed: Uncommitted changes detected after running ApiBuilderTests"
    }
}

# Run remaining test projects
Get-ChildItem -Recurse -Filter "*Tests.csproj" -Path . | Where-Object { $_.Name -ne "ApiBuilderTests.csproj" } | ForEach-Object {
    Run-TestProject $_.FullName
}
