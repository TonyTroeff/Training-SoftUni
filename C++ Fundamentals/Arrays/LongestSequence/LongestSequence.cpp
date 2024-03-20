#include <iostream>
using namespace std;

void readArray(int arr[], const int& n) {
	for (int i = 0; i < n; i++) cin >> arr[i];
}

int main()
{
	constexpr int MAX_N = 100;
	int arr[MAX_N];

	int n;
	cin >> n;

	readArray(arr, n);

	int index = 0, maxLength = 0, sequenceElement = 0;
	while (index < n) {
		int start = index;
		while (index + 1 < n && arr[index] == arr[index + 1]) index++;

		int currentLength = index - start + 1;
		if (currentLength > maxLength) maxLength = currentLength;
		if (currentLength == maxLength) sequenceElement = arr[index];

		index++;
	}

	for (int i = 0; i < maxLength; i++) cout << sequenceElement << ' ';
	cout << endl;
}