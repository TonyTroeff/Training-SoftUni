#include <iostream>
#include <unordered_map>
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

    int sum;
    cin >> sum;

    // Solution #1, Complexity: O(n^2), Simple
    for (int i = 0; i < n - 1; i++) {
        for (int j = i + 1; j < n; j++) {
            if (arr[i] + arr[j] == sum) cout << arr[i] << ' ' << arr[j] << endl;
        }
    }

    /*
    // NOTE: The map/unordered_map data structure will be presented in the next module
    // Solution #2, Complexity: O(n), Advanced
    unordered_map<int, int> counter;
    for (int i = 0; i < n; i++) counter[arr[i]]++;
    
    for (int i = 0; i < n; i++) {
        int diff = sum - arr[i];
        counter[arr[i]]--; // We need to reduce the count first (2 + 2 = 4)

        if (counter.find(diff) != counter.end()) {
            for (int j = 0; j < counter[diff]; j++)
                cout << arr[i] << ' ' << diff << endl;
        }
    }
    */
}
