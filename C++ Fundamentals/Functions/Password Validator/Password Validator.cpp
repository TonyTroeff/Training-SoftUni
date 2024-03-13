#include <iostream>
using namespace std;

int countLetters(const string& text) {
	int result = 0;

	for (int i = 0; i < text.length(); i++) {
		if (isalpha(text[i])) result++;
	}

	return result;
}

int countDigits(const string& text) {
	int result = 0;

	for (int i = 0; i < text.length(); i++) {
		if (isdigit(text[i])) result++;
	}

	return result;
}

int main()
{
	string password;
	cin >> password;

	bool isValid = true;
	if (password.length() < 6 || password.length() > 10) {
		cout << "Password must be between 6 and 10 characters" << endl;
		isValid = false;
	}

	int letters = countLetters(password);
	int digits = countDigits(password);
	if (letters + digits != password.length()) {
		cout << "Password must consist only of letters and digits" << endl;
		isValid = false;
	}

	if (digits < 2) {
		cout << "Password must have at least 2 digits" << endl;
		isValid = false;
	}

	if (isValid) cout << "Password is valid" << endl;
}
