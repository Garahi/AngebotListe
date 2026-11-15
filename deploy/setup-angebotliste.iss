; =====================================================================
;  AngebotsListenManager - Kurulum Sihirbazi (Inno Setup)
;  BauDorf kurulumuyla ayni mantik.
;  Derleme: deploy klasorunde -> YENI-KURULUM-OLUSTUR.cmd (cift tikla)
;  Cikti:   deploy\cikti\AngebotListe-Kurulum.exe
;  - Eski kurulumun USTUNE yazar (ayni AppId = otomatik guncelleme)
;  - Eski Visual-Studio-MSI kurulumunu ("AngeboListeManangerSetup")
;    otomatik bulur ve kaldirir (manuel silmeye gerek yok)
;  - Kullanici profiline kurar => Smart App Control / UAC takilmaz
;  - .NET Framework 4.7.2 yoksa uyarir
; =====================================================================

#define AppName "AngebotsListenManager"
#define AppExe "AngebotListe.exe"
#define Publisher "SahinSoftware"
#define SrcDir "..\AngebotListe\bin\Release"
#define IconSrc "..\AngebotListe\Resources\SalihS48x48.ico"
#ifndef AppVerStr
  #define AppVerStr "1.0"
#endif
#ifndef AppVerNum
  #define AppVerNum "1.0.0.0"
#endif

[Setup]
AppId={{C7B2E5A1-4D3F-4A8E-9B61-5F2A8D0E3C47}
AppName={#AppName}
AppVersion={#AppVerStr}
VersionInfoVersion={#AppVerNum}
AppPublisher={#Publisher}
; Yonetici GEREKMEZ - kullanici profiline kurar
PrivilegesRequired=lowest
DefaultDirName={localappdata}\Programs\AngebotListe
DefaultGroupName=AngebotsListenManager
DisableProgramGroupPage=yes
OutputDir=cikti
OutputBaseFilename=AngebotListe-Kurulum
SetupIconFile={#IconSrc}
UninstallDisplayName={#AppName}
UninstallDisplayIcon={app}\AngebotListe.ico
Compression=lzma2
SolidCompression=yes
WizardStyle=modern
WizardResizable=yes
ArchitecturesAllowed=x86compatible x64compatible
; Eski surum acik olsa bile guncellemek icin kapatir
CloseApplications=yes
RestartApplications=no
SetupLogging=yes
UsePreviousSetupType=no
AlwaysShowDirOnReadyPage=yes

[Languages]
Name: "de"; MessagesFile: "compiler:Languages\German.isl"
Name: "tr"; MessagesFile: "compiler:Languages\Turkish.isl"

[CustomMessages]
de.DesktopIcon=Desktop-Verknüpfung erstellen
tr.DesktopIcon=Masaustune kisayol olustur
de.Shortcuts=Verknüpfungen:
tr.Shortcuts=Kisayollar:
de.StartNow=AngebotsListenManager jetzt starten
tr.StartNow=AngebotsListenManager'i simdi baslat
de.NetMissing=Für dieses Programm wird .NET Framework 4.7.2 (oder neuer) benötigt.%n%nBitte zuerst installieren:%nhttps://dotnet.microsoft.com/download/dotnet-framework/net472%n%nTrotzdem fortfahren?
tr.NetMissing=Bu program icin .NET Framework 4.7.2 (veya daha yenisi) gerekli.%n%nOnce bunu kurun:%nhttps://dotnet.microsoft.com/download/dotnet-framework/net472%n%nYine de devam edilsin mi?

[Tasks]
Name: "desktopicon"; Description: "{cm:DesktopIcon}"; GroupDescription: "{cm:Shortcuts}"

[Files]
; Release ciktisi (exe, config, DLL'ler, chromedriver, x64/x86/arm64 native)
; Gereksizler disarida: pdb/xml, macOS ve Linux dosyalari
Source: "{#SrcDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs; \
  Excludes: "*.pdb,*.xml,*.dylib,runtimes\linux\*,runtimes\osx\*,*.vshost.*"
Source: "{#IconSrc}"; DestDir: "{app}"; DestName: "AngebotListe.ico"; Flags: ignoreversion

[Dirs]
; Programm speichert PDFs und WhatsApp-Kontakte unter C:\AngebotListe
; (bei Deinstallation NICHT loeschen - Kundendaten!)
Name: "{sd}\AngebotListe"; Flags: uninsneveruninstall
Name: "{sd}\AngebotListe\WhatsapaYollanilan"; Flags: uninsneveruninstall

[Icons]
Name: "{group}\{#AppName}";            Filename: "{app}\{#AppExe}"; IconFilename: "{app}\AngebotListe.ico"
Name: "{group}\{#AppName} deinstallieren"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#AppName}";      Filename: "{app}\{#AppExe}"; IconFilename: "{app}\AngebotListe.ico"; Tasks: desktopicon

[Run]
Filename: "{app}\{#AppExe}"; Description: "{cm:StartNow}"; Flags: nowait postinstall skipifsilent

[Code]
{ ---- .NET Framework 4.7.2 (Release >= 461808) kontrolu ---- }
function InitializeSetup(): Boolean;
var
  Rel: Cardinal;
begin
  Result := True;
  if not (RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', Rel)
          and (Rel >= 461808)) then
    Result := (MsgBox(CustomMessage('NetMissing'), mbConfirmation, MB_YESNO) = IDYES);
end;

{ ---- Eski AngebotListe kurulumunu (MSI veya baska installer) bul ---- }
{ Kendi Inno kaydimiz (AppId_is1) atlanir - o zaten ustune yazilir }
function FindOldUninstaller(): String;
var
  Roots: array[0..2] of Integer;
  Paths: array[0..2] of String;
  i, j: Integer;
  SubKeys: TArrayOfString;
  KeyPath, DName, S: String;
begin
  Result := '';
  Roots[0] := HKCU; Paths[0] := 'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall';
  Roots[1] := HKLM; Paths[1] := 'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall';
  Roots[2] := HKLM; Paths[2] := 'SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall';
  for i := 0 to 2 do
  begin
    if RegGetSubkeyNames(Roots[i], Paths[i], SubKeys) then
    begin
      for j := 0 to GetArrayLength(SubKeys) - 1 do
      begin
        if Pos('C7B2E5A1-4D3F-4A8E-9B61-5F2A8D0E3C47', Uppercase(SubKeys[j])) > 0 then
          Continue;
        KeyPath := Paths[i] + '\' + SubKeys[j];
        if RegQueryStringValue(Roots[i], KeyPath, 'DisplayName', DName) then
        begin
          DName := Uppercase(DName);
          if (Pos('ANGEBOLISTE', DName) > 0) or (Pos('ANGEBOTLISTE', DName) > 0)
             or (Pos('ANGEBOTSLISTEN', DName) > 0) then
          begin
            if RegQueryStringValue(Roots[i], KeyPath, 'QuietUninstallString', S) then
            begin Result := S; Exit; end;
            if RegQueryStringValue(Roots[i], KeyPath, 'UninstallString', S) then
            begin Result := S; Exit; end;
          end;
        end;
      end;
    end;
  end;
end;

procedure RemoveOldVersion();
var
  Cmd, ExeOnly: String;
  ResultCode: Integer;
begin
  Cmd := FindOldUninstaller();
  if Cmd = '' then Exit;

  if Pos('MSIEXEC', Uppercase(Cmd)) > 0 then
  begin
    { Eski MSI (Program Files): GUID'i ayikla, kaldir.
      /passive: sadece ilerleme cubugu; gerekirse Windows yonetici onayi sorar }
    Exec('msiexec.exe', '/x ' + Copy(Cmd, Pos('{', Cmd), 38) + ' /passive /norestart',
         '', SW_SHOW, ewWaitUntilTerminated, ResultCode);
  end
  else
  begin
    ExeOnly := RemoveQuotes(Cmd);
    Exec(ExeOnly, '/VERYSILENT /SUPPRESSMSGBOXES /NORESTART', '',
         SW_HIDE, ewWaitUntilTerminated, ResultCode);
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssInstall then
    RemoveOldVersion();
end;
