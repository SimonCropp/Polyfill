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

    foreach ($fw in $frameworks) {
        Write-Host "Running $projName [$fw]" -ForegroundColor Cyan
        $trxName = "$($projName -replace '\.csproj$','')_${fw}.trx"
        $trxDir = Join-Path $PSScriptRoot "TestResults"
        dotnet run --project $proj --configuration Release --framework $fw --no-build -- --report-trx --report-trx-filename $trxName --results-directory $trxDir
        $testExitCode = $LASTEXITCODE

        if ($env:APPVEYOR_JOB_ID) {
            $trxFile = Join-Path $trxDir $trxName
            if (Test-Path $trxFile) {
                Write-Host "Uploading $trxName to AppVeyor" -ForegroundColor Green
                $wc = New-Object 'System.Net.WebClient'
                $wc.UploadFile("https://ci.appveyor.com/api/testresults/mstest/$($env:APPVEYOR_JOB_ID)", $trxFile)
            }
        }

        if ($testExitCode -ne 0) { throw "Tests failed for $projName [$fw]" }
    }
}
