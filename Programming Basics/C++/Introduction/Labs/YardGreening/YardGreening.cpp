#include <iostream>
using namespace std;

int main()
{
    const double pricePerSquareMeter = 7.61;
    const double discountPercentage = 0.18;

    double squareMeters;
    cin >> squareMeters;

    double totalCosts = squareMeters * pricePerSquareMeter;
    double discount = discountPercentage * totalCosts;
    double finalPrice = totalCosts - discount;

    cout << "The final price is: " << finalPrice << " lv." << endl;
    cout << "The discount is: " << discount << " lv." << endl;
}
