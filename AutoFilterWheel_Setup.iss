; autoFilterWheel ASCOM Driver Setup Script
; This script creates an installer for the autoFilterWheel ASCOM FilterWheel driver

#define MyAppName "ASCOM autoFilterWheel Driver"
#define MyAppVersion "6.6.0"
#define MyAppPublisher "autoFilterWheel Project"
#define MyAppURL "https://github.com/yourusername/autoFilterWheel"
#define MyAppExeName "ASCOM.autoFilterWheel.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
AppId={{B4E8C4A2-9F3D-4D2C-8A1B-7F5E9D8C6B4A}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\ASCOM\FilterWheel
DefaultGroupName=ASCOM FilterWheel
AllowNoIcons=yes
OutputDir={userdesktop}
OutputBaseFilename=autoFilterWheel_Setup_v{#MyAppVersion}
SetupIconFile=.\MotoFilterWheel\ASCOM.ico
Compression=lzma
SolidCompression=yes
; Require admin rights for ASCOM driver registration
PrivilegesRequired=admin

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
; Main driver executable and dependencies
Source: ".\MotoFilterWheel\bin\Release\ASCOM.autoFilterWheel.exe"; DestDir: "{app}"; Flags: ignoreversion regserver
Source: ".\MotoFilterWheel\bin\Release\ASCOM.autoFilterWheel.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\MotoFilterWheel\bin\Release\*.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\MotoFilterWheel\bin\Release\*.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: ".\MotoFilterWheel\ReadMe.htm"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\autoFilterWheel Setup"; Filename: "{app}\{#MyAppExeName}"; Parameters: "/setup"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"

[Registry]
; ASCOM FilterWheel registration entries
Root: HKLM; Subkey: "SOFTWARE\ASCOM\FilterWheel Drivers\ASCOM.autoFilterWheel.FilterWheel"; Flags: uninsdeletekey
Root: HKLM; Subkey: "SOFTWARE\ASCOM\FilterWheel Drivers\ASCOM.autoFilterWheel.FilterWheel"; ValueType: string; ValueName: ""; ValueData: "autoFilterWheel"

[Run]
; Register the driver after installation
Filename: "{app}\ASCOM.autoFilterWheel.exe"; Parameters: "/regserver"; StatusMsg: "Registering ASCOM driver..."; Flags: runhidden

[UninstallRun]
; Unregister the driver before uninstallation
Filename: "{app}\ASCOM.autoFilterWheel.exe"; Parameters: "/unregserver"; StatusMsg: "Unregistering ASCOM driver..."; Flags: runhidden

[Code]
// Check if ASCOM Platform is installed
function IsASCOMInstalled(): Boolean;
begin
  Result := RegKeyExists(HKLM, 'SOFTWARE\ASCOM');
end;

function InitializeSetup(): Boolean;
begin
  Result := True;
  if not IsASCOMInstalled() then
  begin
    if MsgBox('ASCOM Platform 6 is required but not detected on this system.' + #13#10 +
              'Please install ASCOM Platform 6 first from:' + #13#10 +
              'https://ascom-standards.org/' + #13#10#13#10 +
              'Continue anyway?', mbConfirmation, MB_YESNO) = IDNO then
      Result := False;
  end;
end;