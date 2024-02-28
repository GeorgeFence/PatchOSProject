org 0
bits 16

global _start

_start:
	mov ah, 0x0e
	mov al, "Done Writing Kernel to memory!"
	mov bh, 0x00
	int 0x10
	hlt

times 510-($-$$) db 0
dw 0xAA55