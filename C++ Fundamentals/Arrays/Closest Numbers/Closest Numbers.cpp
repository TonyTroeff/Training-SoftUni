#include <limits.h>
#include <iostream>
using namespace std;

void readArray(int arr[], const int n) {
	for (int i = 0; i < n; i++) cin >> arr[i];
}

int main()
{
	constexpr int MAX_N = 100;
	int arr[MAX_N];

	int n;
	cin >> n;

	readArray(arr, n);
	
	int minDiff;
	if (n == 1) minDiff = 0;
	else {
		minDiff = INT_MAX;
		for (int i = 0; i < n - 1; i++) {
			for (int j = i + 1; j < n; j++) 
				minDiff = min(minDiff, abs(arr[i] - arr[j]));
		}
	}

	cout << minDiff << endl;
}
