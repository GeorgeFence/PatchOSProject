org 0x7C00
bits 16

boot:
	mov ah, 0x02
	mov al, 0x01 ;sector to read count
	mov ch, 0x00
	mov cl, 0x230F ;from what sector
	mov dh, 0x00
	mov dl, 0x00
	mov bx, 0x1000
	mov es, bx
	int 0x13
	jc disk_error
	mov ah, 0x0e
	mov al, "$"
	mov bh, 0
	int 0x10
	jmp 0x1000:0 ;if real PatchOS kernel, set to 0x1000000

disk_error:
	mov ah, 0x0e
	mov al, "!"
	mov bh, 0
	int 0x10
	hlt

times 510-($-$$) db 0
dw 0xAA55