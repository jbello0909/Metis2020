#include "pch.h"
#include "UnmanagedCode.h"
#include <string>

// Assembly functions import. We need to add each .asm file we are going to use.
extern "C" int GetTimeInSecondsAsm();
extern "C" char* LoadCorrectAsm();
extern "C" char* LoadWrongAsm();
extern "C" int CheckAnswerAsm(int a);
extern "C" char* GetTriggerAsm(char button);
extern "C" int GetCountAsm();  //

int GetTimeInSeconds()
{
     return GetTimeInSecondsAsm();
}

char* LoadCorrect()
{
     return LoadCorrectAsm();
}

char* LoadWrong()
{
     return LoadWrongAsm();
}

bool CheckAnswer(int result)
{
     int a = CheckAnswerAsm(result);
     if (a == 0)
     {
          return false;
     }

     return true;
}

char* GetTrigger(char button)
{
     return GetTriggerAsm(button);
}

int GetCount() 
{
    return GetCountAsm();
}
