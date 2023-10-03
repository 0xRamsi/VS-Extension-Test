#include "pch.h"
#include "SimpleCalculaor.h"
#include "MessagePrompter.h"

int SimpleCalculator(int a, int b, int operation(int a, int b)) {
	int res = operation(a, b);	// This can be writte in C#, and provided as a callback.

	// This makes no sense. It's just here to prove that it's possible.
	MessagePrompter* hallo = new MessagePrompter(res);
	hallo->sayThis(L"C++ says:");
	delete hallo;

	return res;
}