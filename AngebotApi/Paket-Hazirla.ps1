# AngebotListe API: 7/24 sunucuya tasinacak paketi (ZIP) hazirlar
# Projekt wird in einen Temp-Ordner kopiert und dort gebaut -> die laufende API auf diesem PC stoert nicht.
$api = 'C:\AngebotListe\AngebotListe\AngebotApi'
$tmp = Join-Path $env:TEMP 'AngebotApi_Sunucu'
$src = Join-Path $env:TEMP 'AngebotApi_Quelle'
Remove-Item $tmp,$src -Recurse -Force -ErrorAction SilentlyContinue
New-Item (Join-Path $tmp 'app') -ItemType Directory -Force | Out-Null
New-Item $src -ItemType Directory -Force | Out-Null
Copy-Item "$api\AngebotApi.csproj","$api\Program.cs","$api\appsettings.json" $src -Force
Write-Host 'API derleniyor (Release)...' -ForegroundColor Cyan
Push-Location $src
dotnet publish -c Release -o (Join-Path $tmp 'app') | Out-File (Join-Path $api 'publish-log.txt')
$ok = $LASTEXITCODE -eq 0
Pop-Location
Remove-Item $src -Recurse -Force -ErrorAction SilentlyContinue
if (-not $ok) { Write-Host "Derleme hatasi! Ayrinti: $api\publish-log.txt" -ForegroundColor Red; Read-Host 'Enter'; exit 1 }
Copy-Item "$api\Sunucu\*" $tmp -Force
$zip = 'C:\AngebotListe\AngebotApi_Sunucu.zip'
Remove-Item $zip -Force -ErrorAction SilentlyContinue
Compress-Archive -Path (Join-Path $tmp '*') -DestinationPath $zip
Remove-Item $tmp -Recurse -Force -ErrorAction SilentlyContinue
Write-Host "Hazir: $zip" -ForegroundColor Green
Start-Process explorer.exe -ArgumentList "/select,`"$zip`""
