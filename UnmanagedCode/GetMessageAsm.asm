.data
message BYTE "Message from assembly", 0

.code
GetMessageAsm proc
    mov rax, offset message
    ret
GetMessageAsm endp
end
