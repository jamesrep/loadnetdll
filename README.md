# loadnetdll

James Dickson - 2016
Proof-of-concept of how to load a C#-DLL through rundll32. May be used for bypassing certain applocker-protection .... and similar.

Example how to execute:
rundll32 UnmanagedLoader.dll,load LoadNetDll.dll LoadNetDll.Class1 powershell
