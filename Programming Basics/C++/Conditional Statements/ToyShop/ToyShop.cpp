#include <iostream>
using namespace std;

int main()
{
	// 1. Read input
	double tripPrice;
	int puzzles, dolls, bears, minions, trucks;
	cin >> tripPrice >> puzzles >> dolls >> bears >> minions >> trucks;

	// 2. Calculate profit
	double profit = puzzles * 2.6 + dolls * 3 + bears * 4.1 + minions * 8.2 + trucks * 2;
	if (puzzles + dolls + bears + minions + trucks >= 50)
		profit *= 0.75; // Make discount of 25%

	profit *= 0.9; // Pay rent (10% of the profit)

	// 3. Print output
	cout.setf(ios::fixed);
	cout.precision(2);
	
	if (profit >= tripPrice) cout << "Yes! " << profit - tripPrice << " lv left." << endl;
	else cout << "Not enough money! " << tripPrice - profit << " lv needed." << endl;
}
