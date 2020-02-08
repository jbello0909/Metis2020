#include "pch.h"
#include "UnmanagedCode.h"

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

