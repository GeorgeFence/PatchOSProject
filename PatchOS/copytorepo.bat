echo off
FOR /F "delims=|" %%I IN ('DIR "E:\Dokumenty\PATCHOS\PatchOS\PatchOS1Builds\" /B /O:D') DO SET FILE=%%I
set /A FILE=%FILE:~13,3%
set /A FILE+=1
echo %FILE%
copy "E:\Dokumenty\PATCHOS\VS\v 1-notdone orig\PatchOS\bin\Debug\net6.0\PatchOS.iso" E:\Dokumenty\PATCHOS\PatchOS\PatchOS1Builds\PatchOS1Build%FILE%.iso

pause
