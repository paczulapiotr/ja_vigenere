.data 
a_ASCII_B byte 97
z_ASCII_B byte 122
_A_ASCII_B byte 65
_Z_ASCII_B byte 90
a_A_DIFF_B byte 32
ALPHABET_LENGTH_B byte 26
.code
; ==============================================================
; Main procedure used for applying laplace filter on image row
; ### PARAMETERS ###
;	char* text						RCX => R10
;	char* key						RDX => R11
;	char* encrypted					R8						
;	int textLength,					R9						
;	int keyLength,	    			STACK [rbp+48] 			
; ### USED REGISTERS ###
; rax - c
; rbx 
; rcx 
; rdx 
; r12 - loop counter
; r13 
; r14 
; r15 - j
; ==============================================================

encrypt proc
; prepare for stack deallocation
push rbp
mov rbp, rsp
push rax
push rbx
push rcx
push rdx
push r12
push r13
push r14
push r15
; main code
PREPAREVARIABLES:
mov r10, rcx
mov r11, rdx
; run main loop
xor r12, r12
xor r13, r13
xor r14, r14
xor r15, r15 
MAINLOOP:
	mov al, byte ptr[r10]
	cmp al, a_ASCII_B
	jl IF_SKIP
	
	IF_TO_UPPER:
		cmp al, z_ASCII_B
		jg IF_SKIP
		; TO_UPPER:
		sub al, a_A_DIFF_B
		jz ENCRYPT_CHAR

	IF_SKIP:
		cmp al, _A_ASCII_B
		jl SKIP
		cmp al, _Z_ASCII_B
		jg SKIP
		jz ENCRYPT_CHAR

		SKIP:
		jz INCREMENT_LOOP

	ENCRYPT_CHAR:
	xor rbx, rbx
	mov rbx, R11
	add rbx, r15

	xor rcx, rcx
	mov cl, byte ptr[rbx]
	add al, cl

	MODULO:         ; calcs eax mod ebx, returns eax
    xor edx, edx  
    xor ebx, ebx
	mov bl, ALPHABET_LENGTH_B 
	div ebx     
    mov eax, edx ; the remainder was stored in edx 
	add al, _A_ASCII_B
	mov byte ptr[r8], al
	inc r8

	; set new j
	inc r15
	MODULO_KEY:
	mov rax, r15
	div dword ptr[rbp+48]
	mov r15, rdx

	INCREMENT_LOOP:
	inc r10 ; increment text char index
	inc r12 							; inrement loop counter
	cmp r12d, r9d	
	jz CLEARSTACK
	jmp MAINLOOP

CLEARSTACK:
; clear stack
	pop r15
	pop r14
	pop r13
	pop r12
	pop rdx
	pop rcx
	pop rbx
	pop rax
	pop rbp
	ret
encrypt endp 
end
