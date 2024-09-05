#include <iostream>
using namespace std;

int main()
{
	int n1, n2;
	char operation;
	cin >> n1 >> n2 >> operation;

	if ((operation == '/' || operation == '%') && n2 == 0) {
		cout << "Cannot divide " << n1 << " by zero" << endl;
	}
	else {
		if (operation == '/') {
			double divisionResult = (double)n1 / n2;

			cout.setf(ios::fixed);
			cout.precision(2);

			cout << n1 << " / " << n2 << " = " << divisionResult;
		}
		else {
			bool showParity = true;
			int result = 0;

			if (operation == '+') result = n1 + n2;
			else if (operation == '-') result = n1 - n2;
			else if (operation == '*') result = n1 * n2;
			else if (operation == '%') {
				result = n1 % n2;
				showParity = false;
			}

			cout << n1 << ' ' << operation << ' ' << n2 << " = " << result;

			if (showParity) {
				string parity = result % 2 == 0 ? "even" : "odd";
				cout << " - " << parity;
			}

			cout << endl;
		}
	}
}