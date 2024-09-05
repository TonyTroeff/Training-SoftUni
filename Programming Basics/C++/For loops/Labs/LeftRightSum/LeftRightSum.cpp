#include <iostream>
using namespace std;

int main()
{
    int n;
    cin >> n;

    int leftSum = 0;
    for (int i = 0; i < n; i++) 
    {
        int currentNumber;
        cin >> currentNumber;

        leftSum += currentNumber;
    }

    int rightSum = 0;
    for (int i = 0; i < n; i++)
    {
        int currentNumber;
        cin >> currentNumber;

        rightSum += currentNumber;
    }

    if (leftSum == rightSum) cout << "Yes, sum = " << leftSum << endl;
    else cout << "No, diff = " << abs(leftSum - rightSum) << endl;
}
