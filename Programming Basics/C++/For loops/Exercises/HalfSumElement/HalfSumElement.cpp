#include <iostream>
#include <limits.h>
using namespace std;

int main()
{
    int n;
    cin >> n;

    int max = INT_MIN, sum = 0;
    for (int i = 0; i < n; i++)
    {
        int currentNumber;
        cin >> currentNumber;

        sum += currentNumber;
        if (currentNumber > max) max = currentNumber;
    }

    int diff = abs(sum - 2 * max);

    if (diff == 0) cout << "Yes" << endl << "Sum = " << max << endl;
    else cout << "No" << endl << "Diff = " << diff << endl;
}