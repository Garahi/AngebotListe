#!/bin/bash
# Nur installieren + starten (ohne neu zu bauen).  Aufruf:  cd ~/AngebotListe && sh yukle.sh
CIHAZ="iPhone (8)"
cd ~/AngebotListe/AngebotListe.Mobile || exit 1
APP=$(find bin -maxdepth 5 -name "*.app" -type d | head -1)
if [ -z "$APP" ]; then echo "Keine App gefunden - zuerst: sh mac-yukle.sh"; exit 1; fi
xcrun devicectl device install app --device "$CIHAZ" "$APP" && \
xcrun devicectl device process launch --device "$CIHAZ" com.angebotliste.mobile && \
echo "=== TAMAM: yuklendi ve acildi ==="
