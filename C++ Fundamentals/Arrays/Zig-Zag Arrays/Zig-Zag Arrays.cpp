#include <iostream>
using namespace std;

void printArray(const int* arr, const int& n) {
	for (int i = 0; i < n; i++) cout << arr[i] << ' ';
	cout << endl;
}

int main()
{
	constexpr int MAX_N = 100;
	int firstLine[MAX_N], secondLine[MAX_N];

	int n;
	cin >> n;

	for (int i = 0; i < n; i++) {
		if (i % 2 == 0) cin >> firstLine[i] >> secondLine[i];
		else cin >> secondLine[i] >> firstLine[i];
	}

	printArray(firstLine, n);
	printArray(secondLine, n);
}