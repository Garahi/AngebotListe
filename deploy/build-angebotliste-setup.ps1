# =====================================================================
#  AngebotsListenManager Kurulum uretici (BauDorf ile ayni mantik)
#  1) Release derler  2) Inno Setup ile installer olusturur
#  Kullanim:  cd C:\AngebotListe\AngebotListe\deploy ;  .\build-angebotliste-setup.ps1
#  Surum vermek icin:  .\build-angebotliste-setup.ps1 -Version 1.1.0
# =====================================================================
param([string]$Version = "1.0.0")

$ErrorActionPreference = "Stop"
$here = Split-Path -Parent $MyInvocation.MyCommand.Path
$proj = Join-Path $here "..\AngebotListe\AngebotListe.vbproj"
$iss  = Join-Path $here "setup-angebotliste.iss"

# --- MSBuild bul (Visual Studio 2022/2026, hangi surum olursa olsun) ---
$msbuild = $null
$vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
if (Test-Path $vswhere) {
    $msbuild = & $vswhere -latest -prerelease -products * -requires Microsoft.Component.MSBuild -find "MSBuild\**\Bin\MSBuild.exe" | Select-Object -First 1
}
if (-not $msbuild) { $msbuild = "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" }
if (-not (Test-Path $msbuild)) { Write-Host "MSBuild bulunamadi (Visual Studio kurulu mu?)" -ForegroundColor Red; exit 1 }

# --- Inno Setup (ISCC) bul ---
$iscc = @(
    "$env:LOCALAPPDATA\Programs\Inno Setup 6\ISCC.exe",
    "${env:ProgramFiles(x86)}\Inno Setup 6\ISCC.exe",
    "$env:ProgramFiles\Inno Setup 6\ISCC.exe",
    "C:\Users\Salih\AppData\Local\Programs\Inno Setup 6\ISCC.exe"
) | Where-Object { Test-Path $_ } | Select-Object -First 1
if (-not $iscc) {
    Write-Host "Inno Setup 6 bulunamadi. Kurmak icin:  winget install JRSoftware.InnoSetup" -ForegroundColor Red; exit 1
}

Write-Host "== 1/2  Release derleniyor ==" -ForegroundColor Cyan
& $msbuild $proj -nologo -v:m -restore -p:Configuration=Release -p:RestorePackagesConfig=true -t:Build
if ($LASTEXITCODE -ne 0) { Write-Host "Derleme HATASI (AngebotListe acik olabilir - kapatip tekrar dene)" -ForegroundColor Red; exit 1 }

Write-Host "== 2/2  Installer olusturuluyor (v$Version) ==" -ForegroundColor Cyan
$verNum = if ($Version -match '^\d+\.\d+\.\d+$') { "$Version.0" } else { "1.0.0.0" }
& $iscc "/DAppVerStr=$Version" "/DAppVerNum=$verNum" $iss
if ($LASTEXITCODE -ne 0) { Write-Host "Installer HATASI" -ForegroundColor Red; exit 1 }

$out = Join-Path $here "cikti\AngebotListe-Kurulum.exe"
# Ek olarak: tarihli kopya + "EnSon_Kurulum" (her zaman en yenisi)
if (Test-Path $out) {
    $datiert = Join-Path $here ("cikti\AngebotListe-Kurulum_" + (Get-Date -Format "yyyy-MM-dd") + ".exe")
    Copy-Item $out $datiert -Force
    $ensonDir = Join-Path $here "cikti\EnSon_Kurulum"
    if (-not (Test-Path $ensonDir)) { New-Item -ItemType Directory -Path $ensonDir | Out-Null }
    Copy-Item $out (Join-Path $ensonDir "AngebotListe-Kurulum.exe") -Force
    Write-Host "Kopya: $datiert" -ForegroundColor Green
}
Write-Host "`nHAZIR: $out" -ForegroundColor Green
if (Test-Path $out) { "{0:N1} MB" -f ((Get-Item $out).Length/1MB) }
