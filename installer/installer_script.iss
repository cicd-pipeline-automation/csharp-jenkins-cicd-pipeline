; ============================================================================
; 🛠 Inno Setup Installer Script
; 📦 Project: Sample Flask Login (C# ASP.NET Core)
; 🎯 Purpose:
;     - Packages the published .NET application
;     - Generates a Windows installer (.exe)
;     - Uses dynamic values injected by build_config.iss
;
; ⚙ Expected Defines from build_config.iss:
;     - MyAppVersion   → Full version label (e.g., v1.0.1_Patch)
;     - MyOutputFile   → Path for output executable (e.g., result\v1.0.1_Patch.exe)
; ============================================================================

#define MyAppName "Sample Flask Login"

#ifndef MyAppVersion
  #define MyAppVersion "v1.0.0_Alpha"
#endif

#ifndef MyOutputFile
  #define MyOutputFile "result\\v1.0.0_Alpha.exe"
#endif


; ============================================================================
; 📦 Setup Configuration
; ============================================================================
[Setup]
AppId={{F4E61C77-5D25-4F3E-8F41-6D18E88C3F91}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher=Your Company
AppPublisherURL=https://example.com
AppSupportURL=https://example.com/support
AppUpdatesURL=https://example.com/updates

DefaultDirName={pf}\{#MyAppName}
DisableDirPage=no
DefaultGroupName={#MyAppName}

OutputDir=.
OutputBaseFilename={#MyOutputFile}

Compression=lzma
SolidCompression=yes

SetupIconFile=icons\app.ico
WizardImageFile=assets\banner.bmp
LicenseFile=assets\license.txt


; ============================================================================
; 🌐 Languages
; ============================================================================
[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"


; ============================================================================
; 🧩 Optional Tasks
; ============================================================================
[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"


; ============================================================================
; 📁 Files Included in Installer
;     These are generated from `dotnet publish` into the /publish folder
; ============================================================================
[Files]
Source: "..\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs


; ============================================================================
; 📌 Shortcuts (Start Menu & Desktop)
; ============================================================================
[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\SampleFlaskLogin.exe"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\SampleFlaskLogin.exe"; Tasks: desktopicon


; ============================================================================
; 🚀 Optional Auto-Run After Install
; ============================================================================
[Run]
Filename: "{app}\SampleFlaskLogin.exe"; \
    Description: "{cm:LaunchProgram,{#MyAppName}}"; \
    Flags: nowait postinstall skipifsilent
