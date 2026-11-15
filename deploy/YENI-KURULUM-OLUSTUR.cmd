@echo off
rem =====================================================================
rem  AngebotsListenManager - yeni kurulum (AngebotListe-Kurulum.exe) uret
rem  Cift tikla yeter. Once AngebotListe'yi kapat.
rem  Surum degistirmek icin asagidaki 1.0.0 degerini duzenle.
rem =====================================================================
cd /d "%~dp0"
powershell -NoProfile -ExecutionPolicy Bypass -File ".\build-angebotliste-setup.ps1" -Version 1.0.0
echo.
if errorlevel 1 (
  echo *** HATA olustu - yukaridaki mesaja bak ***
) else (
  echo Kurulum dosyasi: %~dp0cikti\AngebotListe-Kurulum.exe
  start "" "%~dp0cikti"
)
echo.
pause
