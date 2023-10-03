#pragma once
class MessagePrompter
{
private:
    int data;

public:
    MessagePrompter(int i);
    ~MessagePrompter();

    void sayThis(const wchar_t* phrase);
};

extern "C" {
    __declspec(dllexport) MessagePrompter* CreateInstance(int i);
    __declspec(dllexport) void ReleaseInstance(MessagePrompter* instance);
    __declspec(dllexport) void testFunc(MessagePrompter* instance, const wchar_t* phrase);
}
