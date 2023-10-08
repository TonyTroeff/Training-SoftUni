#include <iostream>
using namespace std;

int main()
{
    int start, end;
    cin >> start >> end;

    for (int i = start; i <= end; i++)
    {
        int evenSum = 0, oddSum = 0;
        bool isEven = true;

        int num = i;
        while (num != 0)
        {
            int digit = num % 10;
            if (isEven) evenSum += digit;
            else oddSum += digit;

            isEven = !isEven;
            num /= 10;
        }

        if (evenSum == oddSum) cout << i << " ";
    }

    cout << endl;
}
