#include <iostream>
#include <limits.h>
using namespace std;

int main()
{
	int n;
	cin >> n;

	int min = INT_MAX, max = INT_MIN;
	for (int i = 0; i < n; i++)
	{
		int currentNumber;
		cin >> currentNumber;

		if (currentNumber < min) min = currentNumber;
		if (currentNumber > max) max = currentNumber;
	}

	cout << "Max number: " << max << endl;
	cout << "Min number: " << min << endl;
}
