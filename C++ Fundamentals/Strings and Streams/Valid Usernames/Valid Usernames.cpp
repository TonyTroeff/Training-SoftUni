#include <iostream>
#include <string>
using namespace std;

const string delimiter = ", ";

bool isValid(const string& username) {
	if (username.length() < 3 || username.length() > 16) return false;

	for (int i = 0; i < username.length(); i++) {
		if (!isalnum(username[i]) && username[i] != '-' && username[i] != '_')
			return false;
	}

	return true;
}

int main()
{
	string input;
	getline(cin, input);

	int start = 0;
	while (start < input.length()) {
		int end = input.find(delimiter, start);
		if (end == -1) end = input.length();

		string username = input.substr(start, end - start);
		if (isValid(username)) cout << username << endl;

		start = end + delimiter.length();
	}
}
