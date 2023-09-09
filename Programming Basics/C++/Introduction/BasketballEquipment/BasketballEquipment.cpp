#include <iostream>

int main()
{
    int tax;
    std::cin >> tax;

    double shoes = 0.6 * tax;
    double kit = 0.8 * shoes;
    double ball = 0.25 * kit;
    double accessories = 0.2 * ball;

    double total = tax + shoes + kit + ball + accessories;
    std::cout << total << std::endl;
}
