#!/bin/bash
# Nur installieren + starten (ohne neu zu bauen). Aufruf auf dem Mac:  sh ~/AngebotListe/yukle.sh
export DEVELOPER_DIR=/Applications/Xcode26.4.app/Contents/Developer
CIHAZ=00008110-00065D643EE0401E
xcrun devicectl list devices
cd ~/AngebotListe/AngebotListe.Mobile || exit 1
APP=$(find bin -maxdepth 5 -name "*.app" -type d | head -1)
if [ -z "$APP" ]; then echo "Keine App gefunden - zuerst: sh ~/AngebotListe/mac-yukle.sh"; exit 1; fi
xcrun devicectl device install app --device $CIHAZ "$APP" && \
xcrun devicectl device process launch --device $CIHAZ com.angebotliste.mobile && \
echo "=== TAMAM: yuklendi ve acildi ==="
