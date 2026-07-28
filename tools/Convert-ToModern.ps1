# Converts CASE Apps from year-shells + SharedProjects to single SDK-style projects (Revit 2025-2027).
# - Consolidates SharedProject + newest year UI (2023 preferred) into a modern/ folder
# - Leaves original year folders intact under each app (can delete later)
# - Generates CaseApps.sln

$ErrorActionPreference = "Stop"
$root = Resolve-Path (Join-Path $PSScriptRoot "..")
$src = Join-Path $root "src"

function Get-AppLanguage {
    param($AppDir)
    $shared = Get-ChildItem $AppDir -Directory | Where-Object { $_.Name -like "*SharedProject*" } | Select-Object -First 1
    if (-not $shared) { return $null }
    $hasCs = (Get-ChildItem $shared.FullName -Recurse -Filter *.cs -EA SilentlyContinue | Select-Object -First 1) -ne $null
    $hasVb = (Get-ChildItem $shared.FullName -Recurse -Filter *.vb -EA SilentlyContinue | Select-Object -First 1) -ne $null
    if ($hasCs -and -not $hasVb) { return "CSharp" }
    if ($hasVb) { return "VB" }
    return $null
}

function Get-SourceYearFolder {
    param($AppDir)
    foreach ($y in @("2023", "2022", "2021", "2020")) {
        $d = Get-ChildItem $AppDir -Directory | Where-Object { $_.Name -like "*.$y" } | Select-Object -First 1
        if ($d) { return $d }
    }
    return $null
}

function Copy-TreeContent {
    param($From, $To, [string[]]$ExcludeNames = @())
    if (-not (Test-Path $From)) { return }
    Get-ChildItem $From -Force | ForEach-Object {
        if ($ExcludeNames -contains $_.Name) { return }
        $dest = Join-Path $To $_.Name
        if ($_.PSIsContainer) {
            if (-not (Test-Path $dest)) { New-Item -ItemType Directory -Path $dest | Out-Null }
            Copy-TreeContent -From $_.FullName -To $dest -ExcludeNames $ExcludeNames
        }
        else {
            $skipExt = @(".csproj", ".vbproj", ".shproj", ".projitems", ".user", ".suo")
            if ($skipExt -contains $_.Extension.ToLower()) { return }
            # Skip auto-generated assembly info / My Application designer when consolidating
            if ($_.Name -eq "AssemblyInfo.cs" -or $_.Name -eq "AssemblyInfo.vb") { return }
            Copy-Item $_.FullName $dest -Force
        }
    }
}

function Write-CsProj {
    param($Path, $AssemblyName, $RootNamespace, $HasWpf)
    $wpf = if ($HasWpf) { "true" } else { "true" } # always true via Directory.Build.props
    @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <IsCaseAddin>true</IsCaseAddin>
    <RootNamespace>$RootNamespace</RootNamespace>
    <AssemblyName>$AssemblyName</AssemblyName>
    <OutputType>Library</OutputType>
  </PropertyGroup>
</Project>
"@ | Set-Content -Path $Path -Encoding UTF8
}

function Write-VbProj {
    param($Path, $AssemblyName, $RootNamespace)
    @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <IsCaseAddin>true</IsCaseAddin>
    <RootNamespace>$RootNamespace</RootNamespace>
    <AssemblyName>$AssemblyName</AssemblyName>
    <OutputType>Library</OutputType>
    <MyType>Windows</MyType>
    <OptionExplicit>On</OptionExplicit>
    <OptionStrict>Off</OptionStrict>
    <OptionInfer>On</OptionInfer>
    <OptionCompare>Binary</OptionCompare>
  </PropertyGroup>
</Project>
"@ | Set-Content -Path $Path -Encoding UTF8
}

$apps = Get-ChildItem $src -Directory | Where-Object {
    $_.Name -like "Case.*" -and $_.Name -notlike "_*"
}

$converted = @()
$skipped = @()

foreach ($app in $apps) {
    $name = $app.Name
    $lang = Get-AppLanguage $app.FullName
    if (-not $lang) {
        $skipped += "$name (no shared project / language)"
        continue
    }

    $shared = Get-ChildItem $app.FullName -Directory | Where-Object { $_.Name -like "*SharedProject*" } | Select-Object -First 1
    $year = Get-SourceYearFolder $app.FullName
    if (-not $shared -or -not $year) {
        $skipped += "$name (missing shared or year folder)"
        continue
    }

    $modern = Join-Path $app.FullName "modern"
    if (Test-Path $modern) { Remove-Item $modern -Recurse -Force }
    New-Item -ItemType Directory -Path $modern | Out-Null

    # Shared sources first
    Copy-TreeContent -From $shared.FullName -To $modern -ExcludeNames @(
        "bin", "obj", ".vs"
    )

    # Year UI / resources / My Project / Properties / images (overlay, don't wipe shared)
    Copy-TreeContent -From $year.FullName -To $modern -ExcludeNames @(
        "bin", "obj", ".vs"
    )

    # Clean My Project junk that breaks SDK / netcore if needed later
    # Keep Resources.resx and Settings if present

    $rootNs = $name
    # Historical exception: AppsRibbon root namespace
    if ($name -eq "Case.AppsRibbon") { $rootNs = "Case.AppsRibbon" }

    $hasWpf = (Get-ChildItem $modern -Recurse -Filter *.xaml -EA SilentlyContinue | Select-Object -First 1) -ne $null

    if ($lang -eq "CSharp") {
        $projPath = Join-Path $modern "$name.csproj"
        Write-CsProj -Path $projPath -AssemblyName $name -RootNamespace $rootNs -HasWpf $hasWpf
    }
    else {
        $projPath = Join-Path $modern "$name.vbproj"
        Write-VbProj -Path $projPath -AssemblyName $name -RootNamespace $rootNs
    }

    $converted += [PSCustomObject]@{
        Name     = $name
        Lang     = $lang
        Project  = $projPath
        Relative = "src\$name\modern\$name.$(if($lang -eq 'CSharp'){'csproj'}else{'vbproj'})"
    }
    Write-Host "Converted $name ($lang)" -ForegroundColor Green
}

# Write new solution
$slnPath = Join-Path $src "CaseApps.sln"
$sb = New-Object System.Text.StringBuilder
[void]$sb.AppendLine("Microsoft Visual Studio Solution File, Format Version 12.00")
[void]$sb.AppendLine("# Visual Studio Version 17")
[void]$sb.AppendLine("VisualStudioVersion = 17.0.31903.59")
[void]$sb.AppendLine("MinimumVisualStudioVersion = 10.0.40219.1")

$csGuid = "FAE04EC0-301F-11D3-BF4B-00C04F79EFBC"
$vbGuid = "F184B08F-C81C-45F6-A57F-5ABD9991F28F"
$folderGuid = "2150E333-8FDC-42A3-9474-1A3956D46DE8"

$projectEntries = @()
foreach ($c in $converted | Sort-Object Name) {
    $typeGuid = if ($c.Lang -eq "CSharp") { $csGuid } else { $vbGuid }
    $projGuid = [guid]::NewGuid().ToString().ToUpper()
    $rel = "$($c.Name)\modern\$($c.Name).$(if($c.Lang -eq 'CSharp'){'csproj'}else{'vbproj'})"
    [void]$sb.AppendLine("Project(`"{$typeGuid}`") = `"$($c.Name)`", `"$rel`", `"{$projGuid}`"")
    [void]$sb.AppendLine("EndProject")
    $projectEntries += $projGuid
}

[void]$sb.AppendLine("Global")
[void]$sb.AppendLine("	GlobalSection(SolutionConfigurationPlatforms) = preSolution")
[void]$sb.AppendLine("		Debug|Any CPU = Debug|Any CPU")
[void]$sb.AppendLine("		Release|Any CPU = Release|Any CPU")
[void]$sb.AppendLine("	EndGlobalSection")
[void]$sb.AppendLine("	GlobalSection(ProjectConfigurationPlatforms) = postSolution")
foreach ($g in $projectEntries) {
    [void]$sb.AppendLine("		{$g}.Debug|Any CPU.ActiveCfg = Debug|Any CPU")
    [void]$sb.AppendLine("		{$g}.Debug|Any CPU.Build.0 = Debug|Any CPU")
    [void]$sb.AppendLine("		{$g}.Release|Any CPU.ActiveCfg = Release|Any CPU")
    [void]$sb.AppendLine("		{$g}.Release|Any CPU.Build.0 = Release|Any CPU")
}
[void]$sb.AppendLine("	EndGlobalSection")
[void]$sb.AppendLine("EndGlobal")

$sb.ToString() | Set-Content -Path $slnPath -Encoding UTF8

Write-Host "`n=== Summary ===" -ForegroundColor Cyan
Write-Host "Converted: $($converted.Count)"
Write-Host "Skipped:   $($skipped.Count)"
$skipped | ForEach-Object { Write-Host "  - $_" -ForegroundColor Yellow }
Write-Host "Solution:  $slnPath"
