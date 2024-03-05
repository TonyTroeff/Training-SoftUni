#include <iostream>
using namespace std;

int main()
{
    double firstNumber, secondNumber, thirdNumber;
    cin >> firstNumber >> secondNumber >> thirdNumber;

    if (firstNumber == 0 || secondNumber == 0 || thirdNumber == 0) {
        cout << '+' << endl;
    }
    else {
        int negativesCount = 0;
        if (firstNumber < 0) negativesCount++;
        if (secondNumber < 0) negativesCount++;
        if (thirdNumber < 0) negativesCount++;

        if (negativesCount % 2 == 0) cout << '+' << endl;
        else cout << '-' << endl;
    }
}
