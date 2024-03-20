#include <iostream>
#include <limits.h>
using namespace std;

constexpr int MAX_N = 100;

void readArray(int arr[], const int& n) {
    cout << "read array" << endl;
    cout << "&arr = " << &arr << endl;
    cout << "arr = " << (void*)arr << endl;
    cout << "&arr[0] = " << &arr[0] << endl;
    for (int i = 0; i < n; i++) cin >> arr[i];
}

/*
// Required for "Solution #3"
constexpr int BITMASK_ENTRY_LENGTH = sizeof(int) * 8;
constexpr int BITMASK_ENTRIES = MAX_N / sizeof(int);

void findBitmaskIndices(const int& position, int& bitmaskEntryIndex, int& bitIndex) {
    bitmaskEntryIndex = position / BITMASK_ENTRY_LENGTH;
    bitIndex = position % BITMASK_ENTRY_LENGTH;
}
*/

int main()
{
    int arr[MAX_N];

    int n;
    cin >> n;

    readArray(arr, n);

    /*
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
    */

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

    /*
    // Solution #3, Complexity: O(n), Advanced
    int bitmask[BITMASK_ENTRIES]{};
    int runningMax = INT_MIN;

    for (int i = n - 1; i >= 0; i--) {
        if (arr[i] > runningMax) {
            int bitmaskEntryIndex, bitIndex;
            findBitmaskIndices(i, bitmaskEntryIndex, bitIndex);

            bitmask[bitmaskEntryIndex] |= 1 << bitIndex;
            runningMax = arr[i];
        }
    }

    for (int i = 0; i < n; i++) {
        int bitmaskEntryIndex, bitIndex;
        findBitmaskIndices(i, bitmaskEntryIndex, bitIndex);

        if (bitmask[bitmaskEntryIndex] & (1 << bitIndex))
            cout << arr[i] << ' ';
    }
    */

    cout << endl;
}
