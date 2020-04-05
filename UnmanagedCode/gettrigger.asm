.data
TriggerA BYTE "ButtonAClicked", 0
TriggerB BYTE "ButtonBClicked", 0
TriggerC BYTE "ButtonCClicked", 0

.code
GetTriggerAsm proc
    cmp cl, 'A'
    je buttonA
    cmp cl, 'B'
    je buttonB
    cmp cl, 'C'
    je buttonC
    jmp end_
buttonA: 
    mov rax, offset TriggerA
    jmp end_
buttonB: 
    mov rax, offset TriggerB
    jmp end_
buttonC: 
    mov rax, offset TriggerC
    jmp end_
end_:
    ret
GetTriggerAsm endp
end