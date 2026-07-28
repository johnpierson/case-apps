#define MyAppName "CASE Apps"
#define MyAppVersion "27.1.0"
#define MyAppPublisher "CASE"
#define MyAppURL "https://github.com/rudderdon/case-apps"

#define RevitAppName  "Case.Apps"
#define RevitAddinFolder "{userappdata}\Autodesk\REVIT\Addins"
#define RevitFolder25 RevitAddinFolder+"\2025\"+RevitAppName
#define RevitAddin25  RevitAddinFolder+"\2025\"
#define RevitFolder26 RevitAddinFolder+"\2026\"+RevitAppName
#define RevitAddin26  RevitAddinFolder+"\2026\"
#define RevitFolder27 RevitAddinFolder+"\2027\"+RevitAppName
#define RevitAddin27  RevitAddinFolder+"\2027\"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{1AE796B6-5B97-4CC7-848A-C3F44956FB90}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DisableDirPage=yes
DefaultGroupName=CASE Design, Inc\{#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=.\LICENSE
OutputDir=.
OutputBaseFilename=CASEApps-installer
SetupIconFile=assets\logo.ico
WizardImageFile=assets\banner.bmp
Compression=lzma
SolidCompression=yes
ArchitecturesInstallIn64BitMode=x64compatible
;info: http://revolution.screenstepslive.com/s/revolution/m/10695/l/95041-signing-installers-you-create-with-inno-setup
;comment/edit the line below if you are not signing the exe with the CASE pfx
;SignTool=signtoolcase

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Components]
Name: revit25; Description: CASE Apps for Autodesk Revit 2025 (.NET 8);  Types: full
Name: revit26; Description: CASE Apps for Autodesk Revit 2026 (.NET 8);  Types: full
Name: revit27; Description: CASE Apps for Autodesk Revit 2027 (.NET 10); Types: full

[Types]
Name: "full"; Description: "All supported Revit years (2025–2027)"
Name: "custom"; Description: "Select Revit years"; Flags: iscustom

[Files]
;REVIT 2025 ~~~~~~~~~~~~~~~~~~~
Source: "deploy\2025\*"; DestDir: "{#RevitFolder25}"; Excludes: "*.pdb,*.xml,*.config,*.addin,*.tmp"; Flags: ignoreversion recursesubdirs createallsubdirs; Components: revit25
Source: "deploy\{#RevitAppName}.addin"; DestDir: "{#RevitAddin25}"; Flags: ignoreversion; Components: revit25

;REVIT 2026 ~~~~~~~~~~~~~~~~~~~
Source: "deploy\2026\*"; DestDir: "{#RevitFolder26}"; Excludes: "*.pdb,*.xml,*.config,*.addin,*.tmp"; Flags: ignoreversion recursesubdirs createallsubdirs; Components: revit26
Source: "deploy\{#RevitAppName}.addin"; DestDir: "{#RevitAddin26}"; Flags: ignoreversion; Components: revit26

;REVIT 2027 ~~~~~~~~~~~~~~~~~~~
Source: "deploy\2027\*"; DestDir: "{#RevitFolder27}"; Excludes: "*.pdb,*.xml,*.config,*.addin,*.tmp"; Flags: ignoreversion recursesubdirs createallsubdirs; Components: revit27
Source: "deploy\{#RevitAppName}.addin"; DestDir: "{#RevitAddin27}"; Flags: ignoreversion; Components: revit27

; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{cm:ProgramOnTheWeb,{#MyAppName}}"; Filename: "{#MyAppURL}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"

[Code]
function InitializeSetup: Boolean;
begin
  Result := True;
end;
