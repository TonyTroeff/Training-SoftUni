#include <iostream>
using namespace std;

int main()
{
    int originalNumber;
    cin >> originalNumber;

    int disposableNumber = originalNumber;
    int digitsFactorialSum = 0;

    while (disposableNumber != 0) {
        int digit = disposableNumber % 10;

        int factorial = 1;
        for (int i = 2; i <= digit; i++) factorial *= i;

        digitsFactorialSum += factorial;
        disposableNumber /= 10;
    }

    if (originalNumber == digitsFactorialSum) cout << "yes" << endl;
    else cout << "no" << endl;
}
