#pragma once
class SimpleCalculaor
{
};

extern "C" {
	__declspec(dllexport) int SimpleCalculator(int a, int b, int operation(int a, int b));
}