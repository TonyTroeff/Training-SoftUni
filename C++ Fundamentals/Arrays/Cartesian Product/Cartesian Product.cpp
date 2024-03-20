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

    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++)  cout << arr[i] * arr[j] << ' ';
    }
    cout << endl;
}
