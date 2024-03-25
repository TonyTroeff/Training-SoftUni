#include <iostream>
using namespace std;

int main()
{
	string first, second;
	cin >> first >> second;

	int index = 0, result = 0;
	while (index < first.length() || index < second.length()) {
		int m1 = 1, m2 = 1;

		if (index < first.length()) m1 = first[index];
		if (index < second.length()) m2 = second[index];

		result += m1 * m2;

		index++;
	}

	cout << result << endl;
}
