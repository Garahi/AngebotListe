#!/bin/bash
# AngebotListe: GitHub'dan son hali çek, iPhone için derle, telefona yükle ve aç.
# Kullanım (Mac Terminal):  sh ~/AngebotListe/mac-yukle.sh
set -e
export DEVELOPER_DIR=/Applications/Xcode26.4.app/Contents/Developer
export PATH="$HOME/.dotnet:$PATH"
CIHAZ=00008110-00065D643EE0401E

cd ~/AngebotListe
git checkout -- .
git clean -fd AngebotListe.Mobile
git pull

cd AngebotListe.Mobile
rm -rf bin obj
dotnet build -f net10.0-ios -p:RuntimeIdentifier=ios-arm64

APP=$(find bin -maxdepth 5 -name "*.app" -type d | head -1)
xcrun devicectl device install app --device $CIHAZ "$APP"
xcrun devicectl device process launch --device $CIHAZ com.angebotliste.mobile
echo ""
echo "=== TAMAM: AngebotListe iPhone'a yüklendi ==="
