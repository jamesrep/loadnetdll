#include <stdio.h>
#include <string.h>
#include <Windows.h>

#define DLLEXPORT extern "C" __declspec(dllexport) 

DLLEXPORT void __cdecl JustProveLoad(HWND hwnd, HINSTANCE hinst, LPSTR cmdLine, int cmdShow)
{
	MessageBoxA(hwnd, "ECHO", cmdLine, MB_OK);
}

