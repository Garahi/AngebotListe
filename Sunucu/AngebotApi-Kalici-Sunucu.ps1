# AngebotListe - KALICI SUNUCU KURULUMU (Yolcu Transport / KankonSite ile ayni yontem: Tailscale Funnel, ucretsiz)
# - AngebotListe API'yi derler, oturum acilinca otomatik ve gizli baslatir (port 5100)
# - Tailscale Funnel ile sabit https adresi verir: https://<bilgisayar>.<ag>.ts.net:10000
#   (443 = Yolcu API, 8443 = KankonSite API; Funnel sadece 443 / 8443 / 10000 portlarina izin verir)
$ErrorActionPreference = 'Continue'
$kok   = 'C:\AngebotListe\AngebotListe'
$api   = Join-Path $kok 'AngebotApi'
$port  = 5100
$https = 10000
$ts    = 'C:\Program Files\Tailscale\tailscale.exe'

function Baslik($t) { Write-Host ''; Write-Host "=== $t ===" -ForegroundColor Cyan }

# 1) Tailscale (Yolcu kurulumunda yuklendi)
Baslik '1/4 Tailscale'
if (-not (Test-Path $ts)) { Write-Host 'Tailscale bulunamadi! Once Yolcu/Kankon sunucu kurulumunu calistirin.' -ForegroundColor Red; Read-Host 'Enter'; exit 1 }
Write-Host 'Tailscale hazir.' -ForegroundColor Green

# 2) API derle (Release)
Baslik '2/4 AngebotListe API derleniyor'
Get-CimInstance Win32_Process -Filter "Name='dotnet.exe'" | Where-Object { $_.CommandLine -like '*AngebotApi.dll*' } | ForEach-Object { Stop-Process -Id $_.ProcessId -Force }
Push-Location $api
dotnet build -c Release | Out-File (Join-Path $api 'build-log.txt')
$ok = $LASTEXITCODE -eq 0
Pop-Location
if (-not $ok) { Write-Host 'Derleme hatasi! Ayrinti: AngebotApi\build-log.txt' -ForegroundColor Red; Read-Host 'Enter'; exit 1 }
$dll = Get-ChildItem (Join-Path $api 'bin\Release') -Recurse -Filter 'AngebotApi.dll' | Select-Object -First 1
Write-Host "Derlendi: $($dll.FullName)" -ForegroundColor Green

# 3) Oturum acilinca otomatik, gizli baslat (cokerse 1 dk sonra yeniden)
Baslik '3/4 Otomatik baslatma'
$cmd = "Set-Location '$($dll.DirectoryName)'; `$env:ASPNETCORE_ENVIRONMENT='Production'; & dotnet '$($dll.FullName)' --urls http://localhost:$port *>> '$api\api-konsole.txt'"
$act = New-ScheduledTaskAction -Execute 'powershell.exe' -Argument "-NoProfile -WindowStyle Hidden -Command `"$cmd`"" -WorkingDirectory $dll.DirectoryName
$trg = New-ScheduledTaskTrigger -AtLogOn -User "$env:USERDOMAIN\$env:USERNAME"
$set = New-ScheduledTaskSettingsSet -AllowStartIfOnBatteries -DontStopIfGoingOnBatteries -ExecutionTimeLimit ([TimeSpan]::Zero) -RestartCount 999 -RestartInterval (New-TimeSpan -Minutes 1)
$prn = New-ScheduledTaskPrincipal -UserId "$env:USERDOMAIN\$env:USERNAME" -LogonType Interactive -RunLevel Limited
Register-ScheduledTask -TaskName 'AngebotListe API' -Action $act -Trigger $trg -Settings $set -Principal $prn -Force | Out-Null
Start-ScheduledTask -TaskName 'AngebotListe API'
Start-Sleep -Seconds 8
if (Get-NetTCPConnection -LocalPort $port -State Listen -ErrorAction SilentlyContinue) {
    Write-Host "API calisiyor (http://localhost:$port)." -ForegroundColor Green
} else { Write-Host "API henuz acilmadi; ayrinti icin $api\api-konsole.txt" -ForegroundColor Yellow }

# 4) Sabit https adresi (Funnel, 10000)
Baslik '4/4 Sabit https adresi'
& $ts funnel --bg --https=$https $port
$st  = & $ts status --json | ConvertFrom-Json
$url = 'https://' + $st.Self.DNSName.TrimEnd('.') + ':' + $https

Set-Content -Path (Join-Path $kok 'Sunucu\ADRES.txt') -Value $url
try { Set-Clipboard -Value $url } catch {}
Write-Host ''
Write-Host '==========================================================' -ForegroundColor Green
Write-Host ("   ANGEBOTLISTE SUNUCU ADRESI:  $url") -ForegroundColor Green
Write-Host '   (panoya kopyalandi, Sunucu\ADRES.txt dosyasina yazildi)' -ForegroundColor Green
Write-Host "   Test: tarayicida $url  ->  'AngebotListe API laeuft' gorunmeli" -ForegroundColor Green
Write-Host '==========================================================' -ForegroundColor Green
Read-Host 'Pencereyi kapatmak icin Enter'
