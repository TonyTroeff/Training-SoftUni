#include <iostream>

int main()
{
    double depositSum, annualInterest;
    int depositTerm;
    std::cin >> depositSum >> depositTerm >> annualInterest;

    double result = depositSum + depositTerm * (depositSum * annualInterest * 0.01 / 12);

    std::cout << result << std::endl;
}