# ANGEBOTLISTE API - SUNUCU KURULUMU (7/24 calisan sunucu icin)
# ZIP'in acildigi klasorden calisir. KURULUM.bat ile (yonetici olarak) baslatin.
#
# Mevcut duzeni BOZMAZ:
#  - SQL Server'a dokunmaz (tablo/ayar degistirmez, sadece baglanir).
#  - Yolcu API (port 5080, https 443) ve diger servisler oldugu gibi kalir.
#  - Tailscale zaten kuruluysa adini/girisini degistirmez, sadece 10000 portunu ekler.
$ErrorActionPreference = 'Continue'
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
$here   = Split-Path -Parent $MyInvocation.MyCommand.Path
$hedef  = 'C:\AngebotApi'
$appDir = Join-Path $hedef 'bin\Release\net9.0'     # api-log.txt -> C:\AngebotApi\api-log.txt
$port   = 5100
$https  = 10000                                     # Funnel izinli portlar: 443 / 8443 / 10000
$gorev  = 'AngebotListe API'
$ts     = 'C:\Program Files\Tailscale\tailscale.exe'
$dotnet = 'C:\Program Files\dotnet\dotnet.exe'
function Baslik($t) { Write-Host ''; Write-Host "=== $t ===" -ForegroundColor Cyan }

function TS-Calistir([string[]]$argList) {
    $o = Join-Path $env:TEMP ('ts-' + [guid]::NewGuid() + '.txt'); $e = $o + '.err'
    $p = Start-Process $ts -ArgumentList $argList -RedirectStandardOutput $o -RedirectStandardError $e -PassThru -WindowStyle Hidden
    $acildi = $false
    while (-not $p.HasExited) {
        Start-Sleep -Seconds 1
        $t = ((Get-Content $o,$e -Raw -ErrorAction SilentlyContinue) -join "`n")
        if (-not $acildi -and $t -match 'https://login\.tailscale\.com/\S+') {
            Write-Host ('Tarayicida acildi: ' + $matches[0]) -ForegroundColor Yellow
            Write-Host 'Orada giris yapip Connect / Enable deyin.' -ForegroundColor Yellow
            Start-Process $matches[0]; $acildi = $true
        }
    }
}

function SQL-Test([string]$cs) {
    try {
        $c = New-Object System.Data.SqlClient.SqlConnection ($cs -replace 'TrustServerCertificate=True;?','' -replace 'Encrypt=False;?','')
        $c.Open(); $cmd = $c.CreateCommand(); $cmd.CommandText = 'SELECT COUNT(*) FROM Products'
        $n = $cmd.ExecuteScalar(); $c.Close(); return "$n"
    } catch { return $null }
}

if (-not (Test-Path (Join-Path $here 'app\AngebotApi.dll'))) {
    Write-Host 'app\AngebotApi.dll bulunamadi. ZIP dosyasini once tamamen acin (Alle extrahieren).' -ForegroundColor Red
    Read-Host 'Enter'; exit 1
}

Baslik '1/6 Dosyalar kopyalaniyor'
Get-ScheduledTask -TaskName $gorev -ErrorAction SilentlyContinue | Stop-ScheduledTask -ErrorAction SilentlyContinue
Get-CimInstance Win32_Process -Filter "Name='dotnet.exe'" | Where-Object { $_.CommandLine -like '*AngebotApi.dll*' } | ForEach-Object { Stop-Process -Id $_.ProcessId -Force }
Start-Sleep -Seconds 2
$eskiAyar = Join-Path $appDir 'appsettings.json'
$yedek = $null
if (Test-Path $eskiAyar) { $yedek = Get-Content $eskiAyar -Raw }   # guncellemede sunucudaki ayar korunur
New-Item $appDir -ItemType Directory -Force | Out-Null
Copy-Item (Join-Path $here 'app\*') $appDir -Recurse -Force
if ($yedek) { Set-Content $eskiAyar $yedek -Encoding UTF8; Write-Host 'Onceki sunucu ayari (appsettings.json) korundu.' }
Write-Host "Dosyalar $hedef klasorune kopyalandi." -ForegroundColor Green

Baslik '2/6 SQL Server baglantisi kontrol ediliyor (sadece okuma)'
$ayar = Get-Content $eskiAyar -Raw | ConvertFrom-Json
$cs = $ayar.ConnectionStrings.AngebotListe
$n = SQL-Test $cs
if ($n -eq $null) {
    # SQL ayni bilgisayardaysa dis IP bazen kendine baglanamaz -> yerel adresleri dene
    $ds = ([regex]::Match($cs, 'Data Source=([^;]+)')).Groups[1].Value
    foreach ($aday in @('localhost', '.', '.\SQLEXPRESS', 'localhost,1433')) {
        $cs2 = $cs -replace [regex]::Escape("Data Source=$ds"), "Data Source=$aday"
        $n = SQL-Test $cs2
        if ($n -ne $null) {
            $ayar.ConnectionStrings.AngebotListe = $cs2
            $ayar | ConvertTo-Json -Depth 5 | Set-Content $eskiAyar -Encoding UTF8
            Write-Host "SQL bu bilgisayarda: '$aday' adresi kullanilacak." -ForegroundColor Green
            break
        }
    }
}
if ($n -ne $null) { Write-Host "SQL baglantisi tamam ($n urun bulundu)." -ForegroundColor Green }
else { Write-Host "UYARI: SQL'e baglanilamadi. Gerekirse $eskiAyar icindeki 'Data Source' satirini duzeltin." -ForegroundColor Yellow }

Baslik '3/6 .NET 9 kontrol ediliyor'
$var = $false
if (Test-Path $dotnet) { $var = (& $dotnet --list-runtimes) -match 'Microsoft\.AspNetCore\.App 9\.' }
if (-not $var) {
    Write-Host '.NET 9 indiriliyor ve kuruluyor (birkac dakika)...'
    $inst = Join-Path $env:TEMP 'dotnet-install.ps1'
    Invoke-WebRequest 'https://dot.net/v1/dotnet-install.ps1' -OutFile $inst -UseBasicParsing
    & powershell -NoProfile -ExecutionPolicy Bypass -File $inst -Channel 9.0 -Runtime aspnetcore -InstallDir 'C:\Program Files\dotnet'
}
if (-not (Test-Path $dotnet)) { Write-Host '.NET kurulamadi!' -ForegroundColor Red; Read-Host 'Enter'; exit 1 }
Write-Host '.NET 9 hazir.' -ForegroundColor Green

Baslik '4/6 Arka plan gorevi (oturum acmadan, bilgisayar acilinca calisir)'
$mesgul = Get-NetTCPConnection -LocalPort $port -State Listen -ErrorAction SilentlyContinue
if ($mesgul) {
    $p = Get-Process -Id ($mesgul | Select-Object -First 1).OwningProcess -ErrorAction SilentlyContinue
    Write-Host "Port $port baska bir program tarafindan kullaniliyor ($($p.ProcessName)). Kurulum durduruldu, hicbir sey degistirilmedi." -ForegroundColor Red
    Read-Host 'Enter'; exit 1
}
$act = New-ScheduledTaskAction -Execute $dotnet -Argument "`"$appDir\AngebotApi.dll`" --urls http://localhost:$port --environment Production" -WorkingDirectory $appDir
$trg = New-ScheduledTaskTrigger -AtStartup
$set = New-ScheduledTaskSettingsSet -AllowStartIfOnBatteries -DontStopIfGoingOnBatteries -ExecutionTimeLimit ([TimeSpan]::Zero) -RestartCount 999 -RestartInterval (New-TimeSpan -Minutes 1)
$prn = New-ScheduledTaskPrincipal -UserId 'SYSTEM' -LogonType ServiceAccount -RunLevel Highest
Register-ScheduledTask -TaskName $gorev -Action $act -Trigger $trg -Settings $set -Principal $prn -Force | Out-Null
Start-ScheduledTask -TaskName $gorev
Start-Sleep -Seconds 10
try {
    $r = Invoke-WebRequest "http://localhost:$port/" -UseBasicParsing -TimeoutSec 10
    Write-Host ('API calisiyor: ' + $r.Content) -ForegroundColor Green
} catch { Write-Host "UYARI: API cevap vermedi. $hedef\api-log.txt dosyasina bakin." -ForegroundColor Red }

Baslik '5/6 Tailscale'
if (-not (Test-Path $ts)) {
    Write-Host 'Tailscale kuruluyor...'
    $msi = Join-Path $env:TEMP 'tailscale.msi'
    Invoke-WebRequest 'https://pkgs.tailscale.com/stable/tailscale-setup-latest-amd64.msi' -OutFile $msi -UseBasicParsing
    Start-Process msiexec.exe -ArgumentList "/i `"$msi`" /quiet /norestart" -Wait
    Start-Sleep -Seconds 8
}
$st = $null; try { $st = & $ts status --json | ConvertFrom-Json } catch {}
if ($st -and $st.BackendState -eq 'Running') {
    Write-Host "Tailscale zaten bagli ($($st.Self.DNSName.TrimEnd('.'))) - adi ve girisi degistirilmedi." -ForegroundColor Green
} else {
    TS-Calistir @('up', '--unattended', '--hostname=angebot-api')
    $st = & $ts status --json | ConvertFrom-Json
}

Baslik "6/6 Kalici https adresi (port $https, mevcut 443/8443 adreslerine dokunulmaz)"
$fs = (& $ts funnel status 2>$null) -join "`n"
if ($fs -match ":$https\b" -and $fs -notmatch "127\.0\.0\.1:$port|localhost:$port") {
    Write-Host "UYARI: $https portu baska bir servise ayrilmis. Degistirilmedi:" -ForegroundColor Yellow
    Write-Host $fs
} else {
    TS-Calistir @('funnel', '--bg', "--https=$https", "$port")
}
$url = 'https://' + $st.Self.DNSName.TrimEnd('.') + ':' + $https
Set-Content (Join-Path $hedef 'ADRESSE.txt') $url
try { Set-Clipboard -Value $url } catch {}
Remove-Item (Join-Path $here 'app\appsettings.json') -Force -ErrorAction SilentlyContinue   # sifreli kopyayi sil

Write-Host ''
Write-Host '==========================================================' -ForegroundColor Green
Write-Host "   ANGEBOTLISTE SUNUCU ADRESI:  $url" -ForegroundColor Green
Write-Host '   iPhone uygulamasinda Server-Adresse alanina bunu yazin.' -ForegroundColor Green
Write-Host "   Test: tarayicida $url  ->  'AngebotListe API laeuft'" -ForegroundColor Green
Write-Host '   Adres ayrica C:\AngebotApi\ADRESSE.txt dosyasinda.' -ForegroundColor Green
Write-Host '==========================================================' -ForegroundColor Green
Read-Host 'Pencereyi kapatmak icin Enter'
