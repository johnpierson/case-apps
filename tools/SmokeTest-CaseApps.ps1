# Offline smoke checks for installed / deployed CASE Apps (no Revit UI required).

param(
    [ValidateSet('2025', '2026', '2027')]
    [string]$RevitYear = '2026',
    [switch]$FromInstall
)

$ErrorActionPreference = 'Stop'
$root = Resolve-Path (Join-Path $PSScriptRoot '..')

if ($FromInstall) {
    $appDir = Join-Path $env:AppData "Autodesk\Revit\Addins\$RevitYear\Case.Apps"
    $addinFile = Join-Path $env:AppData "Autodesk\Revit\Addins\$RevitYear\Case.Apps.addin"
} else {
    $appDir = Join-Path $root "deploy\$RevitYear"
    $addinFile = Join-Path $root 'deploy\Case.Apps.addin'
}

$failures = New-Object System.Collections.Generic.List[string]

function Fail([string]$msg) {
    [void]$script:failures.Add($msg)
    Write-Host "FAIL: $msg" -ForegroundColor Red
}
function Ok([string]$msg) {
    Write-Host "OK:   $msg" -ForegroundColor Green
}

Write-Host "Smoke test (Revit $RevitYear) source: $appDir" -ForegroundColor Cyan

if (-not (Test-Path $appDir)) {
    Fail "App directory missing: $appDir"
    throw 'Abort'
}
if (-not (Test-Path $addinFile)) {
    Fail "Manifest missing: $addinFile"
} else {
    Ok 'Manifest present'
}

[xml]$xml = Get-Content $addinFile
$assembly = $xml.RevitAddIns.AddIn.Assembly
$className = $xml.RevitAddIns.AddIn.FullClassName
Ok "Assembly=$assembly Class=$className"

$ribbonName = 'Case.AppsRibbon.dll'
$ribbonPath = Join-Path $appDir $ribbonName
if (-not (Test-Path $ribbonPath)) {
    Fail "Ribbon DLL missing: $ribbonPath"
} else {
    $kb = [math]::Round((Get-Item $ribbonPath).Length / 1024)
    Ok "Ribbon DLL present (${kb}KB)"
}

$dlls = @(Get-ChildItem $appDir -Filter 'Case.*.dll' | ForEach-Object { $_.Name })
Ok "Found $($dlls.Count) Case assemblies"

$expectedCore = @(
    'Case.AppsRibbon.dll',
    'Case.ApplySysOrient.dll',
    'Case.BasicReporting.dll',
    'Case.DeleteViewsAndPurge.dll',
    'Case.SharedParameters.dll',
    'Case.ViewTemplates.dll',
    'Case.Subs.Exceler8.dll',
    'Case.Subs.ViewSync.dll'
)
foreach ($e in $expectedCore) {
    if ($dlls -notcontains $e) {
        Fail "Missing expected assembly: $e"
    } else {
        Ok "Found $e"
    }
}

foreach ($dll in $expectedCore) {
    $path = Join-Path $appDir $dll
    if (-not (Test-Path $path)) { continue }
    $bytes = [IO.File]::ReadAllBytes($path)
    if ($bytes[0] -ne 0x4D -or $bytes[1] -ne 0x5A) {
        Fail "$dll is not a PE file"
    } else {
        Ok "$dll is PE"
    }
}

$revitExe = "C:\Program Files\Autodesk\Revit $RevitYear\Revit.exe"
if (Test-Path $revitExe) {
    Ok "Revit $RevitYear installed"
} else {
    Write-Host "WARN: Revit $RevitYear not found at $revitExe" -ForegroundColor Yellow
}

if ($failures.Count -eq 0) {
    Write-Host ''
    Write-Host "Smoke test PASSED ($($dlls.Count) assemblies)." -ForegroundColor Green
    if ($FromInstall) {
        Write-Host "Open Revit $RevitYear and confirm ribbon tabs CASE Apps #1 / CASE Apps #2."
    }
    exit 0
}

Write-Host ''
Write-Host "Smoke test FAILED ($($failures.Count) issues)." -ForegroundColor Red
exit 1
