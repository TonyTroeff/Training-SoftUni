#include <iostream>
using namespace std;

long calculateFactorial(const int& num) {
    long result = 1;
    for (int i = 2; i <= num; i++) result *= i;

    return result;
}

int main()
{
    int firstNumber, secondNumber;
    cin >> firstNumber >> secondNumber;

    long firstFactorial = calculateFactorial(firstNumber), secondFactorial = calculateFactorial(secondNumber);

    double result = (double)firstFactorial / secondFactorial;

    cout.setf(ios::fixed);
    cout.precision(2);

    cout << result << endl;
}