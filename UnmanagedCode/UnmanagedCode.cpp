#include "pch.h"
#include "UnmanagedCode.h"

// Assembly functions import. We need to add each .asm file we are going to use.
extern "C" int GetNumberAsm();
extern "C" char* GetMessageAsm();

int GetTestNumber()
{
    return GetNumberAsm();
}

char* GetTestMessage()
{
    return GetMessageAsm();
}