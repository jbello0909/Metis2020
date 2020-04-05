.data
message BYTE "Textures/Correct", 0

.code
LoadCorrectAsm proc
    mov rax, offset message
    ret
LoadCorrectAsm endp
end