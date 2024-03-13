#include <iostream>
using namespace std;

int sumDigits(int number, const bool& even) {
    if (number < 0) number *= -1;

    int result = 0;
    while (number != 0) {
        int digit = number % 10;
        bool remainderIsZero = digit % 2 == 0;

        if (remainderIsZero == even) result += digit;
        number /= 10;
    }

    return result;
}

int main()
{
    int number;
    cin >> number;

    int result = sumDigits(number, true) * sumDigits(number, false);
    cout << result << endl;
}
