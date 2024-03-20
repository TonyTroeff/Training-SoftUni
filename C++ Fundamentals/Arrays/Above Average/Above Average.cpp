#include <iostream>
using namespace std;

void readArray(int arr[], const int& n) {
    for (int i = 0; i < n; i++) cin >> arr[i];
}

int calculateAverage(const int arr[], const int& n) {
    int sum = 0;
    for (int i = 0; i < n; i++) sum += arr[i];

    return sum / n;
}

void printArray(const int arr[], const int& n, const int& minValue) {
    for (int i = 0; i < n; i++) {
        if (arr[i] >= minValue) cout << arr[i] << ' ';
    }

    cout << endl;
}

int main()
{
    constexpr int MAX_N = 100;
    int arr[MAX_N];

    int n;
    cin >> n;

    readArray(arr, n);
    int average = calculateAverage(arr, n);
    printArray(arr, n, average);
}
