#include <iostream>
using namespace std;

int main()
{
	int limit;
	cin >> limit;

	int sum = 0;
	while (sum < limit) 
	{
		int currentNumber;
		cin >> currentNumber;

		sum += currentNumber;
	}

	cout << sum << endl;
}
