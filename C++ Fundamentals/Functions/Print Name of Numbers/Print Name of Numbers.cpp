#include <iostream>
using namespace std;

void printSeparator() {
	cout << ' ';
}

void printDigit(const int& digit) {
	if (digit == 1) cout << "one";
	else if (digit == 2) cout << "two";
	else if (digit == 3) cout << "three";
	else if (digit == 4) cout << "four";
	else if (digit == 5) cout << "five";
	else if (digit == 6) cout << "six";
	else if (digit == 7) cout << "seven";
	else if (digit == 8) cout << "eight";
	else if (digit == 9) cout << "nine";

	printSeparator();
}

void printThousands(const int& digit) {
	printDigit(digit);
	cout << "thousand";

	printSeparator();
}

void printHundreds(const int& digit) {
	printDigit(digit);
	cout << "hundred";

	printSeparator();
}

void printTens(const int& digit) {
	if (digit == 2) cout << "twenty";
	else if (digit == 3) cout << "thirty";
	else if (digit == 4) cout << "forty";
	else if (digit == 5) cout << "fifty";
	else if (digit == 6) cout << "sixty";
	else if (digit == 7) cout << "seventy";
	else if (digit == 8) cout << "eighty";
	else if (digit == 9) cout << "ninety";

	printSeparator();
}

void printSpecial(const int& digit) {
	if (digit == 0) cout << "ten";
	else if (digit == 1) cout << "eleven";
	else if (digit == 2) cout << "twelve";
	else if (digit == 3) cout << "thirteen";
	else if (digit == 4) cout << "fourteen";
	else if (digit == 5) cout << "fifteen";
	else if (digit == 6) cout << "sixteen";
	else if (digit == 7) cout << "seventeen";
	else if (digit == 8) cout << "eighteen";
	else if (digit == 9) cout << "nineteen";
}

int main()
{
	int number;
	cin >> number;

	if (number == 0) cout << "zero" << endl;
	else {
		int thousands = number / 1000;
		if (thousands != 0) printThousands(thousands);

		int hundreds = (number % 1000) / 100;
		if (hundreds != 0) printHundreds(hundreds);

		int tens = (number % 100) / 10;
		int units = number % 10;
		if (tens == 1) printSpecial(units);
		else {
			if (tens != 0) printTens(tens);
			if (units != 0) printDigit(units);
		}
	}
}