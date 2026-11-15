; AngebotsListenManager – Kurulum (NSIS), gleiche Logik wie BauDorf-Kurulum:
; - pro Benutzer installiert (kein Admin / UAC nötig) -> %LOCALAPPDATA%\Programs\AngebotListe
; - Update: über die alte Version installieren, laufendes Programm wird beendet
; - alte Visual-Studio-MSI-Installation ("AngeboListeManangerSetup") wird automatisch entfernt
; - prüft .NET Framework 4.7.2, legt C:\AngebotListe an (bleibt bei Deinstallation erhalten)
Unicode true
!include "MUI2.nsh"
!include "LogicLib.nsh"

!define APPNAME   "AngebotsListenManager"
!define EXE       "AngebotListe.exe"
!define PUBLISHER "SahinSoftware"
!ifndef VERSION
  !define VERSION "1.0.0"
!endif
!define UNINSTKEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\AngebotListe"
!define OLDMSI    "{54330DA7-D143-4BAE-8F89-A6E81371D13E}"

Name "${APPNAME}"
OutFile "AngebotListe-Kurulum.exe"
InstallDir "$LOCALAPPDATA\Programs\AngebotListe"
InstallDirRegKey HKCU "${UNINSTKEY}" "InstallLocation"
RequestExecutionLevel user
SetCompressor /SOLID lzma
ManifestDPIAware true
BrandingText "${PUBLISHER}"
VIProductVersion "${VERSION}.0"
VIAddVersionKey /LANG=1031 "ProductName" "${APPNAME}"
VIAddVersionKey /LANG=1031 "CompanyName" "${PUBLISHER}"
VIAddVersionKey /LANG=1031 "FileVersion" "${VERSION}"
VIAddVersionKey /LANG=1031 "FileDescription" "${APPNAME} Setup"
VIAddVersionKey /LANG=1031 "LegalCopyright" "${PUBLISHER}"

!define MUI_ICON   "app\AngebotListe.ico"
!define MUI_UNICON "app\AngebotListe.ico"
!define MUI_ABORTWARNING
!define MUI_FINISHPAGE_RUN "$INSTDIR\${EXE}"
!define MUI_COMPONENTSPAGE_NODESC

!insertmacro MUI_PAGE_WELCOME
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

!insertmacro MUI_LANGUAGE "German"
!insertmacro MUI_LANGUAGE "Turkish"

LangString NetFehlt ${LANG_GERMAN}  "Für dieses Programm wird .NET Framework 4.7.2 (oder neuer) benötigt.$\n$\nBitte zuerst installieren:$\nhttps://dotnet.microsoft.com/download/dotnet-framework/net472$\n$\nTrotzdem fortfahren?"
LangString NetFehlt ${LANG_TURKISH} "Bu program için .NET Framework 4.7.2 (veya daha yenisi) gerekli.$\n$\nÖnce bunu kurun:$\nhttps://dotnet.microsoft.com/download/dotnet-framework/net472$\n$\nYine de devam edilsin mi?"
LangString SecProg ${LANG_GERMAN}  "Programm"
LangString SecProg ${LANG_TURKISH} "Program"
LangString SecDesk ${LANG_GERMAN}  "Desktop-Verknüpfung"
LangString SecDesk ${LANG_TURKISH} "Masaüstü kısayolu"
LangString AltWeg ${LANG_GERMAN}  "Alte Version wird entfernt..."
LangString AltWeg ${LANG_TURKISH} "Eski sürüm kaldırılıyor..."

Function .onInit
  !insertmacro MUI_LANGDLL_DISPLAY
  ReadRegDWORD $0 HKLM "SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full" "Release"
  ${If} $0 < 461808
    MessageBox MB_YESNO|MB_ICONEXCLAMATION "$(NetFehlt)" IDYES +2
    Abort
  ${EndIf}
FunctionEnd

; Alte MSI-Installation (Program Files) entfernen – Windows fragt evtl. einmal nach Admin-Bestätigung
Function AlteVersionEntfernen
  StrCpy $1 ""
  SetRegView 32
  ReadRegStr $1 HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${OLDMSI}" "DisplayName"
  SetRegView 64
  ${If} $1 == ""
    ReadRegStr $1 HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${OLDMSI}" "DisplayName"
  ${EndIf}
  ${If} $1 == ""
    ReadRegStr $1 HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${OLDMSI}" "DisplayName"
  ${EndIf}
  ${If} $1 != ""
    DetailPrint "$(AltWeg)"
    ExecWait 'msiexec.exe /x ${OLDMSI} /passive /norestart'
  ${EndIf}
FunctionEnd

Section "$(SecProg)" SecMain
  SectionIn RO
  ; laufendes Programm schließen (Update)
  nsExec::Exec 'taskkill /F /IM ${EXE}'
  Sleep 500
  Call AlteVersionEntfernen

  SetOutPath "$INSTDIR"
  File /r "app\*.*"

  ; Datenordner (PDFs, WhatsApp-Kontakte) – wird bei Deinstallation NICHT gelöscht
  CreateDirectory "C:\AngebotListe"
  CreateDirectory "C:\AngebotListe\WhatsapaYollanilan"

  CreateDirectory "$SMPROGRAMS\${APPNAME}"
  CreateShortcut "$SMPROGRAMS\${APPNAME}\${APPNAME}.lnk" "$INSTDIR\${EXE}" "" "$INSTDIR\AngebotListe.ico"
  CreateShortcut "$SMPROGRAMS\${APPNAME}\${APPNAME} deinstallieren.lnk" "$INSTDIR\Deinstallieren.exe"

  WriteUninstaller "$INSTDIR\Deinstallieren.exe"
  WriteRegStr HKCU "${UNINSTKEY}" "DisplayName" "${APPNAME}"
  WriteRegStr HKCU "${UNINSTKEY}" "DisplayVersion" "${VERSION}"
  WriteRegStr HKCU "${UNINSTKEY}" "Publisher" "${PUBLISHER}"
  WriteRegStr HKCU "${UNINSTKEY}" "DisplayIcon" "$INSTDIR\AngebotListe.ico"
  WriteRegStr HKCU "${UNINSTKEY}" "InstallLocation" "$INSTDIR"
  WriteRegStr HKCU "${UNINSTKEY}" "UninstallString" '"$INSTDIR\Deinstallieren.exe"'
  WriteRegStr HKCU "${UNINSTKEY}" "QuietUninstallString" '"$INSTDIR\Deinstallieren.exe" /S'
  WriteRegDWORD HKCU "${UNINSTKEY}" "NoModify" 1
  WriteRegDWORD HKCU "${UNINSTKEY}" "NoRepair" 1
SectionEnd

Section "$(SecDesk)" SecDesktop
  CreateShortcut "$DESKTOP\${APPNAME}.lnk" "$INSTDIR\${EXE}" "" "$INSTDIR\AngebotListe.ico"
SectionEnd

Section "Uninstall"
  nsExec::Exec 'taskkill /F /IM ${EXE}'
  Sleep 500
  Delete "$DESKTOP\${APPNAME}.lnk"
  RMDir /r "$SMPROGRAMS\${APPNAME}"
  RMDir /r "$INSTDIR"
  DeleteRegKey HKCU "${UNINSTKEY}"
SectionEnd
