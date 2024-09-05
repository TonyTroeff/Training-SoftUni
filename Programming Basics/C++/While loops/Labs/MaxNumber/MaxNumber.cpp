#include <iostream>
#include <limits.h>
#include <string>
using namespace std;

int main()
{
    int max = INT_MIN;

    string input;
    while (cin >> input && input != "Stop")
    {
        int currentNumber = stoi(input);
        if (currentNumber > max) max = currentNumber;
    }

    cout << max << endl;
}
