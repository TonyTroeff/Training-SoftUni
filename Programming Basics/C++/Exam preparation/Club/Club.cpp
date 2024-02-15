#include <iostream>
#include <string>
using namespace std;

int main()
{
	double wantedProfit;
	cin >> wantedProfit;

	double profit = 0;

	while (profit < wantedProfit) {
		string cocktail;
		getline(cin.ignore(), cocktail);
		if (cocktail == "Party!") break;

		int count;
		cin >> count;

		int basePrice = cocktail.length() * count;

		double discount = 0;
		if (basePrice % 2 != 0) discount = 0.25;

		profit += basePrice * (1 - discount);
	}

	cout.setf(ios::fixed);
	cout.precision(2);

	if (profit >= wantedProfit) cout << "Target acquired." << endl;
	else cout << "We need " << wantedProfit - profit << " leva more." << endl;

	cout << "Club income - " << profit << " leva." << endl;
}
