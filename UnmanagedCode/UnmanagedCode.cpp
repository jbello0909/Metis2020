#include "pch.h"
#include "UnmanagedCode.h"
#include <string>

// Assembly functions import. We need to add each .asm file we are going to use.
extern "C" int GetTimeInSecondsAsm();
extern "C" char* LoadCorrectAsm();
extern "C" char* LoadWrongAsm();
extern "C" int CheckAnswerAsm(int a);
extern "C" char* GetTriggerAsm(char button);
extern "C" int GetCountAsm();
extern "C" int IncrementAsm(int value);
extern "C" int UpdateScoreIncAsm(int score);
extern "C" int UpdateScoreDecAsm(int score);

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

int Increment(int value)
{
     return IncrementAsm(value);
}

int UpdateScoreInc(int score)
{
     return UpdateScoreIncAsm(score);
}

int UpdateScoreDec(int score)
{
     return UpdateScoreDecAsm(score);
}
