# Migration script for NUnit to TUnit
$files = Get-ChildItem -Path "C:\Code\Polyfill\src\Tests" -Filter "*.cs" -File | Where-Object { $_.Name -notlike "*obj*" }

foreach ($file in $files) {
    $content = Get-Content $file.FullName -Raw

    # Skip if already migrated (contains async Task)
    if ($content -match "public async Task") {
        continue
    }

    # Replace Assert patterns
    $content = $content -replace 'Assert\.AreEqual\(([^,]+),\s*([^)]+)\)', 'await Assert.That($2).IsEqualTo($1)'
    $content = $content -replace 'Assert\.AreNotEqual\(([^,]+),\s*([^)]+)\)', 'await Assert.That($2).IsNotEqualTo($1)'
    $content = $content -replace 'Assert\.IsTrue\(([^)]+)\)', 'await Assert.That($1).IsTrue()'
    $content = $content -replace 'Assert\.True\(([^)]+)\)', 'await Assert.That($1).IsTrue()'
    $content = $content -replace 'Assert\.IsFalse\(([^)]+)\)', 'await Assert.That($1).IsFalse()'
    $content = $content -replace 'Assert\.False\(([^)]+)\)', 'await Assert.That($1).IsFalse()'
    $content = $content -replace 'Assert\.IsNull\(([^)]+)\)', 'await Assert.That($1).IsNull()'
    $content = $content -replace 'Assert\.IsNotNull\(([^)]+)\)', 'await Assert.That($1).IsNotNull()'

    # Change test methods to async Task
    $content = $content -replace '(\[Test\]\s+public\s+)void\s+', '$1async Task '

    Set-Content $file.FullName $content
}
