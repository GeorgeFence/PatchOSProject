echo off
FOR /F "delims=|" %%I IN ('DIR "D:\Dokumenty\PATCHOS\Repository\PatchOSProject\Other\PatchOS1Builds" /B /O:D') DO SET FILE=%%I
set /A FILE=%FILE:~13,3%
set /A FILE+=1
echo %FILE%
copy "D:\Dokumenty\PATCHOS\Repository\PatchOSProject\PatchOS\bin\Debug\net6.0\PatchOS.iso" D:\Dokumenty\PATCHOS\Repository\PatchOSProject\Other\PatchOS1Builds\PatchOS1Build%FILE%.iso

pause
