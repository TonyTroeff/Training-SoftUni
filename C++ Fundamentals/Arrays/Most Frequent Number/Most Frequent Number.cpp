#include <iostream>
using namespace std;

void readArray(int arr[], const int n) {
    for (int i = 0; i < n; i++) cin >> arr[i];
}

int main()
{
    constexpr int MAX_N = 100, VALUES = 10;
    int arr[MAX_N], freq[VALUES]{};

    int n;
    cin >> n;

    readArray(arr, n);

    int maxFreq = 0;
    for (int i = 0; i < n; i++) {
        int currentValue = arr[i];
        maxFreq = max(maxFreq, ++freq[currentValue]);
    }

    for (int i = 0; i < VALUES; i++) {
        if (freq[i] == maxFreq) cout << i << ' ';
    }
    cout << endl;
}
