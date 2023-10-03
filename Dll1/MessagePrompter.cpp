#include "pch.h"
#include "MessagePrompter.h"
#include <stdio.h>

MessagePrompter::MessagePrompter(int i) {
	data = i;
}

MessagePrompter::~MessagePrompter() {
	// Nothing to do.
}

void MessagePrompter::sayThis(const wchar_t* phrase) {
	wchar_t buffer[256];
	swprintf(buffer, 256, L"%s %d", phrase, data);
	MessageBox(NULL, buffer, L"HalloDLLWelt", MB_OK);
}

MessagePrompter* CreateInstance(int i) {
	return new MessagePrompter(i);
}

void ReleaseInstance(MessagePrompter* instance) {
	delete instance;
}

void testFunc(MessagePrompter* instance, const wchar_t* phrase) {
	instance->sayThis(L"von der MessagePrompter-Instanz");
	//MessageBox(NULL, phrase, L"Hello World Says", MB_OK);
}
