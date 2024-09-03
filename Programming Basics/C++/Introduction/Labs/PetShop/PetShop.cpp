#include <iostream>
using namespace std;

int main()
{
	const double dogFoodPrice = 2.5;
	const double catFoodPrice = 4;

	int dogs, cats;
	cin >> dogs >> cats;

	double totalCosts = dogs * dogFoodPrice + cats * catFoodPrice;
	cout << totalCosts << " lv." << endl;
}
