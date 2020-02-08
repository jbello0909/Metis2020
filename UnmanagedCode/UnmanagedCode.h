#pragma once
#include <string>
using namespace std;

extern "C" __declspec(dllexport)
int GetTestNumber();

extern "C" __declspec(dllexport)
char* GetTestMessage();