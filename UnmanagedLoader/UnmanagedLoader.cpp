// James Dickson - 2016
// Proof-of-concept of how to load a C#-DLL through rundll32.
//
// Example how to execute:
// rundll32 UnmanagedLoader.dll,load LoadNetDll.dll LoadNetDll.Class1 powershell
//
using namespace System;
using namespace System::Reflection;
using namespace System::Runtime::InteropServices;

extern "C" __declspec( dllexport )
void _cdecl load(long hwnd, long hinst, char *lpszCmdLine, int nCmdShow) 
{	
	String^ delimStr = " ";
	String ^strPath = gcnew String(lpszCmdLine);

	array<Char>^ delimiter = delimStr->ToCharArray();
	array<String^>^ parameters = strPath->Split(delimiter);
	System::Reflection::Assembly ^assembly = System::Reflection::Assembly::LoadFile(parameters[0]);
	System::Type ^type = assembly->GetType(parameters[1]); // Class1

	ConstructorInfo^ mConstructor = type->GetConstructor(Type::EmptyTypes);
	Object^ mObject = mConstructor->Invoke(gcnew array<Object^>(0));

	MethodInfo ^minf = type->GetMethod(parameters[2]);

	if(parameters->Length <= 3)
	{
		minf->Invoke(mObject, gcnew array<Object^>(1){""});
	}
	else
	{
		array<String^>^ parameters2 = gcnew array <String ^>(parameters->Length -3);
		Array::Copy(parameters, 3, parameters2, 0, parameters->Length -3);
		minf->Invoke(mObject, parameters2);
	}

}
