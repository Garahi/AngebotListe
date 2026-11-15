@echo off
powershell -NoProfile -ExecutionPolicy Bypass -Command "Start-Process powershell -Verb RunAs -ArgumentList '-NoProfile -ExecutionPolicy Bypass -NoExit -File C:\AngebotListe\AngebotListe\Sunucu\AngebotApi-Kalici-Sunucu.ps1'"
