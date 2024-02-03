#include <iostream>
#include <memory>
#include <string>
using namespace std;

unique_ptr<char[]> transform(const char* text, const size_t& n, char (*transformFunc)(const char&)) {
	unique_ptr<char[]> result = make_unique<char[]>(n);

	for (size_t i = 0; i < n; i++) result[i] = transformFunc(text[i]);

	return result;
}

int main()
{
	string input;
	getline(cin, input);

	unique_ptr<char[]> lowerValue = transform(input.c_str(), input.length() + 1, [](const char& s) { return (char)tolower(s); });
	cout << lowerValue.get() << endl;

	unique_ptr<char[]> upperValue = transform(input.c_str(), input.length() + 1, [](const char& s) { return (char)toupper(s); });
	cout << upperValue.get() << endl;
}