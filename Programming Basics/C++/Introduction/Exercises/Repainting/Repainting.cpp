#include <iostream>

int main()
{
    int nylon, paint, diluent, hours;
    std::cin >> nylon >> paint >> diluent >> hours;

    double materialsCost = 1.5 * (nylon + 2) + 14.5 * (paint * 1.1) + diluent * 5 + 0.4;
    double wage = 0.3 * materialsCost;
    double total = materialsCost + wage * hours;

    std::cout << total << std::endl;
}
