del image.hdd
fsutil file createnew image.hdd 16777216
cd limine
limine.exe bios-install ../image.hdd
pause

cd ..

robocopy boot/limine-bios.sys image.hdd
robocopy boot/limine.cfg image.hdd
pause