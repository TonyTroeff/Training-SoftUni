#include <iostream>
#include <limits.h>
using namespace std;

int main()
{
    int n;
    cin >> n;

    int min = INT_MAX, max = INT_MIN;
    for (int i = 0; i < n; i++) {
        int current;
        cin >> current;

        if (current < min) min = current;
        if (current > max) max = current;
    }

    cout << min << ' ' << max << endl;
}
