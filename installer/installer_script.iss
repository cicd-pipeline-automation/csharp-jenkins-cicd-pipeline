; ============================================================================
; 🛠 Inno Setup Installer Script (Final Working Version)
; ============================================================================

#define MyAppName "Sample Flask Login"

#ifndef MyAppVersion
  #define MyAppVersion "v1.0.0_Alpha"
#endif

; Jenkins should provide ONLY the base filename (no folder)
#ifndef MyOutputFile
  #define MyOutputFile "SampleFlaskLoginInstaller_v1.0.0_Alpha"
#endif


; ============================================================================
; 📦 Setup Configuration
; ============================================================================
[Setup]
AppId={{F4E61C77-5D25-4F3E-8F41-6D18E88C3F91}}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher=Your Company
AppPublisherURL=https://example.com
AppSupportURL=https://example.com/support
AppUpdatesURL=https://example.com/updates

DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}

; Output directory is controlled by Jenkins
OutputDir=..\result
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
; 📁 Files Included (from dotnet publish)
; ============================================================================
[Files]
Source: "..\publish\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs


; ============================================================================
; 📌 Shortcuts
; ============================================================================
[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\SampleFlaskLogin.exe"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\SampleFlaskLogin.exe"; Tasks: desktopicon


; ============================================================================
; 🚀 Auto-run After Install (optional)
; ============================================================================
[Run]
Filename: "{app}\SampleFlaskLogin.exe"; \
    Description: "{cm:LaunchProgram,{#MyAppName}}"; \
    Flags: nowait postinstall skipifsilent
