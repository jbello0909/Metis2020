.data
message BYTE "Textures/Wrong", 0

.code
LoadWrongAsm proc
    mov rax, offset message
    ret
LoadWrongAsm endp
end