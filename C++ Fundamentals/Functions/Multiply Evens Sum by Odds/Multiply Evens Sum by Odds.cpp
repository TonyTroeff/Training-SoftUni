#include <iostream>
using namespace std;

int sumDigits(int number, const bool& even) {
    int result = 0;
    while (number != 0) {
        int digit = number % 10;
        bool remainderIsZero = digit % 2 == 0;

        if (remainderIsZero == even) result += digit;
        number /= 10;
    }

    return result;
}

// Alternative approach using function parameters:
// int sumDigits(int number, bool (*filter)(int)) {
//     int result = 0;
//     while (number != 0) {
//         int digit = number % 10;
//         if (filter(digit)) result += digit;
//         number /= 10;
//     }
//
//     return result;
// }

int main()
{
    int number;
    cin >> number;

    int evenSum = sumDigits(number, true);
    int oddSum = sumDigits(number, false);
    
    // Alternative approach using function parameters:
    // int evenSum = sumDigits(number, [](int digit) { return digit % 2 == 0; });
    // int oddSum = sumDigits(number, [](int digit) { return digit % 2 != 0; });

    int result = evenSum * oddSum;
    cout << result << endl;
}
