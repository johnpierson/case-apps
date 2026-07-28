# Install CASE Apps from deploy/{year} into the Revit Addins folder for smoke-testing.
# Usage:
#   .\tools\Install-CaseApps.ps1
#   .\tools\Install-CaseApps.ps1 -RevitYear 2026
#   .\tools\Install-CaseApps.ps1 -RevitYear 2026 -Uninstall

param(
    [ValidateSet("2025", "2026", "2027")]
    [string]$RevitYear = "2026",
    [switch]$Uninstall
)

$ErrorActionPreference = "Stop"
$root = Resolve-Path (Join-Path $PSScriptRoot "..")
$srcDir = Join-Path $root "deploy\$RevitYear"
$addinsRoot = Join-Path $env:AppData "Autodesk\Revit\Addins\$RevitYear"
$appDir = Join-Path $addinsRoot "Case.Apps"
$addinFile = Join-Path $addinsRoot "Case.Apps.addin"

if ($Uninstall) {
    if (Test-Path $appDir) { Remove-Item $appDir -Recurse -Force }
    if (Test-Path $addinFile) { Remove-Item $addinFile -Force }
    Write-Host "Uninstalled CASE Apps from $addinsRoot" -ForegroundColor Yellow
    return
}

if (-not (Test-Path $srcDir)) {
    throw "Deploy folder not found: $srcDir. Build first (.\build.ps1 -RevitVersions $RevitYear)."
}

if (-not (Test-Path $addinsRoot)) {
    New-Item -ItemType Directory -Path $addinsRoot -Force | Out-Null
}
if (Test-Path $appDir) { Remove-Item $appDir -Recurse -Force }
New-Item -ItemType Directory -Path $appDir -Force | Out-Null

# Copy binaries (skip per-app .addin files and symbols)
Get-ChildItem $srcDir -File | Where-Object {
    $_.Extension -notin @(".addin", ".pdb", ".xml", ".tmp")
} | Copy-Item -Destination $appDir -Force

# Manifest lives next to the year folder; Assembly path is Case.Apps\...
$manifestSrc = Join-Path $root "deploy\Case.Apps.addin"
if (-not (Test-Path $manifestSrc)) {
    $manifestSrc = Join-Path $srcDir "Case.Apps.addin"
}
Copy-Item $manifestSrc $addinFile -Force

$dllCount = (Get-ChildItem $appDir -Filter *.dll).Count
Write-Host "Installed CASE Apps for Revit $RevitYear" -ForegroundColor Green
Write-Host "  Manifest: $addinFile"
Write-Host "  Assemblies ($dllCount): $appDir"
Write-Host ""
Write-Host "Start Revit $RevitYear and look for ribbon tabs 'CASE Apps #1' and 'CASE Apps #2'."
Write-Host "To remove: .\tools\Install-CaseApps.ps1 -RevitYear $RevitYear -Uninstall"
