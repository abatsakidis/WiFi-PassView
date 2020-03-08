@echo off
netsh wlan show profiles | findstr "All" > temp.txt
