#include <iostream>
#include <string>
#include <math.h>
using namespace std;

int main()
{
    int primeSum = 0, nonPrimeSum = 0;

    string command;
    cin >> command;
    while (command != "stop")
    {
        int number = stoi(command);

        if (number < 0) cout << "Number is negative." << endl;
        else
        {
            bool isPrime = true;
            double numSqrt = sqrt(number);
            for (int i = 2; i <= numSqrt; i++)
            {
                if (number % i == 0)
                {
                    isPrime = false;
                    break;
                }
            }

            if (isPrime) primeSum += number;
            else nonPrimeSum += number;
        }

        cin >> command;
    }

    cout << "Sum of all prime numbers is: " << primeSum << endl;
    cout << "Sum of all non prime numbers is: " << nonPrimeSum << endl;
}
