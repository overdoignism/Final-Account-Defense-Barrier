@echo off

rem ==== No Admin set ==== 
set __COMPAT_LAYER=RUNASINVOKER

rem ==== Modify arguments as needed ====
start "" "Final Account Defense Barrier.exe" "KDF:1" %1

rem ==== Don't Remove this ====
timeout /t 3 /nobreak >nul