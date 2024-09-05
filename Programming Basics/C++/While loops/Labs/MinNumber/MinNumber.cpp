#include <iostream>
#include <limits.h>
#include <string>
using namespace std;

int main()
{
    int min = INT_MAX;

    string input;
    while (cin >> input && input != "Stop")
    {
        int currentNumber = stoi(input);
        if (currentNumber < min) min = currentNumber;
    }

    cout << min << endl;
}
