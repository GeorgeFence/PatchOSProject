echo off
FOR /F "delims=|" %%I IN ('DIR "..\Other\PatchOS1Builds" /B /O:D') DO SET FILE=%%I
set /A FILE=%FILE:~13,4%
set /A FILE+=1
echo %FILE%
copy "bin\Debug\net6.0\PatchOS.iso" ..\Other\PatchOS1Builds\PatchOS1Build%FILE%.iso

pause
