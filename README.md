# NOTE: This repo is primarily a host for the CASE apps that were discontinued on the previous repo. I cannot promise continued support for the apps. 

For more info on this refer to the following links:
>https://twitter.com/gtalarico/status/986790742139039749

>https://twitter.com/AYBABTM/status/986669506255818752

---

# CASE Apps

A revolutionary set of Autodesk Revit add-ins and tools, CASE Apps enhances your daily BIM workflows allowing your teams to do more — better and faster. Previously offered as pro and free subscriptions through [CASE] (http://case-inc.com/) and open sourced by [WeWork](http://wework.com), this software comes with a simple installer. 

While we hope some of our old and new friends continue to actively contribute to these apps on their own - we, as WeWork, will not be managing the GitHub community. We’ve included a description and instructions on how to access the open source code. After that, it’s up to you! 

Huge thanks to [Don Rudder](https://github.com/rudderdon) for developing most of these tools! And let’s not forget [Matthew Nelson](https://github.com/mnelson7982), [Matteo Cominetti](https://github.com/teocomi), and [Kyle Morin](https://github.com/kmorin) for contributing.

## Installation

### Quick install (dev / smoke test)
```powershell
.\build.ps1 -RevitVersions 2026
.\tools\Install-CaseApps.ps1 -RevitYear 2026
.\tools\SmokeTest-CaseApps.ps1 -RevitYear 2026 -FromInstall
```
Then start **Revit 2026** and look for ribbon tabs **CASE Apps #1** and **CASE Apps #2**.

Uninstall: `.\tools\Install-CaseApps.ps1 -RevitYear 2026 -Uninstall`

### Installer
Build with Inno Setup from `CaseApps-installer.iss` (components for Revit **2025 / 2026 / 2027**). Files land under `%AppData%\Autodesk\Revit\Addins\<year>\Case.Apps\` with `Case.Apps.addin` beside that folder.

## Modernization (Revit 2025–2027)

Upgraded for **Revit 2025, 2026, and 2027**:

| Year | Target framework | Revit API packages |
|------|------------------|--------------------|
| 2025 | `net8.0-windows` | Nice3point.Revit.Api.* |
| 2026 | `net8.0-windows` | Nice3point.Revit.Api.* |
| 2027 | `net10.0-windows` | Nice3point.Revit.Api.* |

### Layout
- **Projects:** `src/<AppName>/modern/` — one SDK-style project per add-in
- **Solution:** `src/CaseApps.sln`
- **Shared MSBuild:** `src/Directory.Build.props`, `src/Directory.Build.targets`
- **Outputs:** `deploy/2025`, `deploy/2026`, `deploy/2027`

### Building
Requires [.NET SDK 10+](https://dotnet.microsoft.com/download) (rolls forward for net8 builds).

```powershell
# All years
.\build.ps1

# One year
.\build.ps1 -RevitVersions 2026

# Or directly
dotnet build src\CaseApps.sln -c Release -p:RevitVersion=2026
```

### Notes
- VB.NET sources remain VB; tooling and APIs were modernized in place.
- `Case.Subs.OpenNURBS` is **not** in the modern solution (legacy .NET Framework OpenNURBS wrapper).
- API updates include `ElementId.Value`, `GroupTypeId` / `ForgeTypeId`, and Revit 2027 `ViewType.PressureLossReport`.

## Pull Requests
Feel free to clone/fork the repo and sumbit pull requests to keep improving it!

## Full app list

### Apps Set #1 (former CASE Free Apps)
-	Case.ApplySysOrient
-	Case.BasicReporting
-	Case.ChangeReplaceFamTypeNames
-	Case.DeleteViewsAndPurge
-	Case.Directionality
-	Case.DoorMarkRenumber
-	Case.Export.Families
-	Case.ExportSharedParameters
-	Case.ExtrudeRoomsToMass
-	Case.FamilySubcategories
-	Case.HiddenParameterToParameter
-	Case.ImageToDraftingView
-	Case.LightingLayout
-	Case.LineChanger
-	Case.ModeledRoomTags
-	Case.MultiViewDuplicate
-	Case.ObjectStyles
-	Case.ParallelWalls
-	Case.ReportGroupsByView
-	Case.RevClouds
-	Case.RoomInsertionPoint
-	Case.RoomSync
-	Case.SharedParameters
-	Case.ViewCreator
-	Case.ViewportReporting
-	Case.ViewTemplates


### Apps Set #2 (former CASE Pro Apps)
-	Case.Subs.DeleteViewsAndPurge
-	Case.Subs.Exceler8
-	Case.Subs.Linestyles
-	Case.Subs.MultiViewDuplicate
-	Case.Subs.OpenNURBS
-	Case.Subs.Renamer
-	Case.Subs.RoomsToMass
-	Case.Subs.SharedParameters
-	Case.Subs.SuperTag
-	Case.Subs.ViewSync
-	Case.Subs.ViewTemplates
-	Case.Subs.Worksets
-	Case.Subs.Xyzcopy 

### Apps Set #3 (not included in the installer)
-	Case.DimensionOverrides
-	Case.FreeBenchmarking
-	Case.Navis2BCF (Navisworks Addin)
-	Case.Subs.KeyMatcher
-	Case.UngroupAll
