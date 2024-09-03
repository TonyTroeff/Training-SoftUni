#include <iostream>

int main()
{
    int chicken, fish, vegetarian;
    std::cin >> chicken >> fish >> vegetarian;

    double basePrice = chicken * 10.35 + fish * 12.40 + vegetarian * 8.15;
    double desertPrice = 0.2 * basePrice;

    double total = basePrice + desertPrice + 2.5;

    std::cout << total << std::endl;
}
