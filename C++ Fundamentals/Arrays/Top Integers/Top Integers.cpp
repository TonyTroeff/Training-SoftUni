#include <iostream>
#include <limits.h>
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

    // Solution #1, Complexity: O(n^2), Simple
    for (int i = 0; i < n; i++) {
        bool isTop = true;
        for (int j = i + 1; j < n; j++) {
            if (arr[j] >= arr[i]) {
                isTop = false;
                break;
            }
        }

        if (isTop) cout << arr[i] << ' ';
    }

    /*
    // Solution #2, Complexity: O(n), Advanced
    bool topElements[MAX_N]{};
    int runningMax = INT_MIN;

    for (int i = n - 1; i >= 0; i--) {
        topElements[i] = arr[i] > runningMax;
        if (topElements[i]) runningMax = arr[i];
    }

    for (int i = 0; i < n; i++) {
        if (topElements[i]) cout << arr[i] << ' ';
    }
    */

    cout << endl;
}
