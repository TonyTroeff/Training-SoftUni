#include <iostream>
using namespace std;

int main()
{
    int n;
    cin >> n;

    int oddSum = 0, evenSum = 0;
    for (int i = 0; i < n; i++)
    {
        int currentNumber;
        cin >> currentNumber;

        if (i % 2 == 0) evenSum += currentNumber;
        else oddSum += currentNumber;
    }

    if (oddSum == evenSum)
    {
        cout << "Yes" << endl;
        cout << "Sum = " << oddSum << endl;
    }
    else
    {
        cout << "No" << endl;
        cout << "Diff = " << abs(oddSum - evenSum) << endl;
    }
}
