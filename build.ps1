# Build CASE Apps for one or more Revit years
# Usage:
#   .\build.ps1
#   .\build.ps1 -RevitVersions 2025,2026,2027
#   .\build.ps1 -Configuration Debug -RevitVersions 2026

param(
    [string[]]$RevitVersions = @("2025", "2026", "2027"),
    [string]$Configuration = "Release",
    [string]$Solution = "src\CaseApps.sln"
)

$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $root

foreach ($year in $RevitVersions) {
    Write-Host "`n=== Building for Revit $year ($Configuration) ===" -ForegroundColor Cyan
    foreach ($y in @("2025", "2026", "2027")) {
        $dir = Join-Path $root "deploy\$y"
        if (-not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir | Out-Null }
    }
    dotnet build $Solution -c $Configuration -p:RevitVersion=$year --nologo
    if ($LASTEXITCODE -ne 0) {
        throw "Build failed for Revit $year"
    }
}

Write-Host "`nAll requested years built successfully." -ForegroundColor Green
