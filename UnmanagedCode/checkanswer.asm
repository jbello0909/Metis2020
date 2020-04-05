.code
CheckAnswerAsm proc
    cmp ecx, 0
    je false
    mov eax, 1
    jmp end_
false: 
    mov eax, 0
end_:
    ret
CheckAnswerAsm endp
end