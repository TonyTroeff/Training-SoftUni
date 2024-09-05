#include <iostream>
using namespace std;

int main()
{
	int n;
	cin >> n;

	for (int i = 0, val = 1; i <= n; i += 2, val *= 4)
		cout << val << endl;
}
