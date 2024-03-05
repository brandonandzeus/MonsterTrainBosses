@echo off
set "src=Assets"
set "dst=C:\Program Files (x86)\Steam\steamapps\workshop\content\1102190\3172898394\plugins\Assets"
robocopy "%src%" "%dst%" /E /MIR
if ErrorLevel 8 (exit /B 1) else (exit /B 0)