#include <iostream>

int main()
{
	double usd;
	std::cin >> usd;

	double bgn = usd * 1.79549;

	std::cout.setf(std::ios::fixed);
	std::cout.precision(2);
	std::cout << bgn << std::endl;

	return 0;
}