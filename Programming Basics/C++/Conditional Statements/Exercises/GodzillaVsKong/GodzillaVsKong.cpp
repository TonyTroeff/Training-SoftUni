#include <iostream>
using namespace std;

int main()
{
    // 1. Read input
    double budget, clothingCostsPerStatist;
    int statistsCount;
    cin >> budget >> statistsCount >> clothingCostsPerStatist;

    // 2. Calculate total expenses
    double decorationCosts = 0.1 * budget; // 10% of the total budget

    double totalClothingCosts = statistsCount * clothingCostsPerStatist;
    if (statistsCount > 150)
        totalClothingCosts *= 0.9; // Apply 10% discount

    double totalCosts = decorationCosts + totalClothingCosts;

    // 3. Print output
    cout.setf(ios::fixed);
    cout.precision(2);
    if (budget >= totalCosts) cout << "Action!" << endl << "Wingard starts filming with " << budget - totalCosts << " leva left." << endl;
    else cout << "Not enough money!" << endl << "Wingard needs " << totalCosts - budget << " leva more.";
}
