#include <iostream>
using namespace std;

void readArray(int arr[], const int n) {
    for (int i = 0; i < n; i++) cin >> arr[i];
}

void printArray(int arr[], const int n) {
    for (int i = 0; i < n; i++) cout << arr[i] << ' ';
    cout << endl;
}

// Solution #1, Complexity: O(n^2), Simple
void shiftArray(int arr[], const int n) {
    int first = arr[0];
    for (int i = 0; i < n - 1; i++) arr[i] = arr[i + 1];

    arr[n - 1] = first;
}

int main()
{
    constexpr int MAX_N = 100;
    int arr[MAX_N];

    int n;
    cin >> n;

    readArray(arr, n);

    int shifts;
    cin >> shifts;

    // One simple optimization here:
    shifts %= n;

    for (int i = 0; i < shifts; i++) shiftArray(arr, n);

    printArray(arr, n);
}

/*
// Solution #2, Complexity: O(n), Advanced
void rotateArray(int arr[], const int start, const int count) {
    int iterations = count / 2, end = start + count - 1;
    for (int i = 0; i < iterations; i++) {
        int swap = arr[start + i];
        arr[start + i] = arr[end - i];
        arr[end - i] = swap;
    }
}

int main()
{
    constexpr int MAX_N = 100;
    int arr[MAX_N];

    int n;
    cin >> n;

    readArray(arr, n);

    int shifts;
    cin >> shifts;

    // One simple optimization here:
    shifts %= n;

    if (shifts != 0) {
        rotateArray(arr, 0, shifts);
        rotateArray(arr, shifts, n - shifts);
        rotateArray(arr, 0, n);
    }

    printArray(arr, n);
}
*/