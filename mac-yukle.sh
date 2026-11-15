#!/bin/bash
# Bauen + auf das iPhone installieren + starten.  Aufruf:  cd ~/AngebotListe && sh mac-yukle.sh
set -e
export PATH="$HOME/.dotnet:$PATH"
# Gerät per NAME ansprechen (die UDID wird vom neuen Xcode/iOS 27 nicht gefunden)
CIHAZ="iPhone (8)"
cd ~/AngebotListe; git checkout -- .; git clean -fd AngebotListe.Mobile; git pull
cd AngebotListe.Mobile; rm -rf bin obj
# Bauen mit Xcode26.4 (SDK), installieren mit dem Standard-Xcode (kennt iOS 27)
DEVELOPER_DIR=/Applications/Xcode26.4.app/Contents/Developer dotnet build -f net10.0-ios -p:RuntimeIdentifier=ios-arm64
APP=$(find bin -maxdepth 5 -name "*.app" -type d | head -1)
xcrun devicectl device install app --device "$CIHAZ" "$APP"
xcrun devicectl device process launch --device "$CIHAZ" com.angebotliste.mobile
echo "=== TAMAM: AngebotListe iPhone'a yuklendi ==="
