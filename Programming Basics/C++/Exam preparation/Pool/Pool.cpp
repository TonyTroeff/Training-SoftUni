#include <iostream>
#include <math.h>
using namespace std;

int main()
{
	int people;
	double entryPrice, loungePrice, umbrellaPrice;
	cin >> people >> entryPrice >> loungePrice >> umbrellaPrice;

	double totalCosts = people * entryPrice + ceil(0.75 * people) * loungePrice + ceil(0.5 * people) * umbrellaPrice;

	cout.setf(ios::fixed);
	cout.precision(2);

	cout << totalCosts << " lv." << endl;
}