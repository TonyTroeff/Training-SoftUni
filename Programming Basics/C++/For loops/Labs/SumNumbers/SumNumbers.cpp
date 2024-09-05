#include <iostream>
using namespace std;

int main()
{
    int n;
    cin >> n;

    int sum = 0;
    for (int i = 0; i < n; i++)
    {
        int currentNumber;
        cin >> currentNumber;

        sum += currentNumber;
    }

    cout << sum << endl;
}
