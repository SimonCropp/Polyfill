Get-ChildItem -Recurse -Filter "*Tests.csproj" -Path src | ForEach-Object {
    $proj = $_.FullName
    [xml]$xml = Get-Content $proj
    $frameworks = ($xml.Project.PropertyGroup.TargetFrameworks ?? $xml.Project.PropertyGroup.TargetFramework) -split ';'
    foreach ($fw in $frameworks | Where-Object { $_ }) {
        Write-Host "Running $($_.Name) [$fw]" -ForegroundColor Cyan
        dotnet run --project $proj --configuration Release --framework $fw --no-build
        if ($LASTEXITCODE -ne 0) { throw "Tests failed for $($_.Name) [$fw]" }
    }
}