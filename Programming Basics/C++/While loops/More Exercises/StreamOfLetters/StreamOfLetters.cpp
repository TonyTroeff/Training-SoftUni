#include <iostream>
using namespace std;

int main()
{
	bool cIsFound = false, oIsFound = false, nIsFound = false;
	string result = "", currentWord = "";

	string input;
	cin >> input;
	while (input != "End") {
		char symbol = input[0];

		if ((65 <= symbol && symbol <= 90) || (97 <= symbol && symbol <= 122)) {
			if (symbol == 'c' && !cIsFound) {
				cIsFound = true;
			}
			else if (symbol == 'o' && !oIsFound) {
				oIsFound = true;
			}
			else if (symbol == 'n' && !nIsFound) {
				nIsFound = true;
			}
			else {
				currentWord += symbol;
			}

			if (cIsFound && oIsFound && nIsFound) {
				result = result + currentWord + ' ';
				cIsFound = false;
				oIsFound = false;
				nIsFound = false;
				currentWord = "";
			}
		}

		cin >> input;
	}

	cout << result;
}