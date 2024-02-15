#include <iostream>
using namespace std;

int main()
{
    const double fuelPrice = 2.1, guidePrice = 100;

    double budget, requiredFuel;
    string day;
    cin >> budget >> requiredFuel >> day;

    double totalCosts = requiredFuel * fuelPrice + guidePrice;
    if (day == "Saturday") totalCosts *= 0.9;
    else if (day == "Sunday") totalCosts *= 0.8;

    cout.setf(ios::fixed);
    cout.precision(2);

    if (budget >= totalCosts) cout << "Safari time! Money left: " << budget - totalCosts << " lv." << endl;
    else cout << "Not enough money! Money needed: " << totalCosts - budget << " lv." << endl;
}
