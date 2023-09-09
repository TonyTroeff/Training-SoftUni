#include <iostream>

int main()
{
    int pens, markers, diluent, discountPercentage;
    std::cin >> pens >> markers >> diluent >> discountPercentage;

    double total = (pens * 5.8 + markers * 7.2 + diluent * 1.2) * (1 - discountPercentage * 0.01);
    std::cout << total << std::endl;
}