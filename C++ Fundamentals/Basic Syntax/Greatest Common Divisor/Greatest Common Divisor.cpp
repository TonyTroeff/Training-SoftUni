#include <iostream>
using namespace std;

int main()
{
	int firstNumber, secondNumber;
	cin >> firstNumber >> secondNumber;

    while (firstNumber != 0 && secondNumber != 0)
    {
        if (firstNumber > secondNumber) firstNumber %= secondNumber;
        else secondNumber %= firstNumber;
    }

    int gcd;
    if (firstNumber == 0) gcd = secondNumber;
    else gcd = firstNumber;

    // Another elegant approach using bitwise operations:
    // int gcd = firstNumber | secondNumber;

    cout << gcd << endl;
}
