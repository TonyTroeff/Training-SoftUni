#include <iostream>
using namespace std;

int main()
{
    int budget, fishermenCount;
    string season;
    cin >> budget >> season >> fishermenCount;

    int rentPrice = 0;
    if (season == "Spring") rentPrice = 3000;
    else if (season == "Summer" || season == "Autumn") rentPrice = 4200;
    else if (season == "Winter") rentPrice = 2600;
    
    double discountMultiplier;
    if (fishermenCount <= 6) discountMultiplier = 0.9;
    else if (fishermenCount <= 11) discountMultiplier = 0.85;
    else discountMultiplier = 0.75;

    double totalCosts = rentPrice * discountMultiplier;

    if (fishermenCount % 2 == 0 && season != "Autumn") totalCosts *= 0.95;

    cout.setf(ios::fixed);
    cout.precision(2);
    if (budget >= totalCosts) cout << "Yes! You have " << budget - totalCosts << " leva left." << endl;
    else cout << "Not enough money! You need " << totalCosts - budget << " leva." << endl;
}