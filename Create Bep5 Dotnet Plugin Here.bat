@echo off
setlocal enabledelayedexpansion
set LF=^


REM Two empty lines above are necessary

REM set /p UnityVersion="Unity Version?!LF!"
REM set /p TFM="NET version? (e.g., 35, 46, 472)!LF!"
set /p pluginName="Plugin Name:!LF!"

REM MINE MOGUL
set unityVer=6000.2.9
set netVer=netstandard2.1
REM set pluginName=HideQuestHUD

REM dotnet new bep6plugin_unity_mono -n !pluginName! -T net!TFM! -U !unityVer!
REM dotnet new bep5plugin_unity_mono -n !pluginName! -T net!TFM! -U !unityVer!
REM dotnet restore !pluginName!

dotnet new classlib -n !pluginName! -f !netVer!

PAUSE