#pragma once
#include <string>
using namespace std;

extern "C" __declspec(dllexport)
int GetTimeInSeconds();

extern "C" __declspec(dllexport)
char* LoadCorrect();

extern "C" __declspec(dllexport)
char* LoadWrong();

extern "C" __declspec(dllexport)
bool CheckAnswer(int a);

extern "C" __declspec(dllexport)
char* GetTrigger(char button);

extern "C" __declspec(dllexport)
int Increment(int value);

extern "C" __declspec(dllexport)
int UpdateScoreInc(int value);

extern "C" __declspec(dllexport)
int UpdateScoreDec(int value);