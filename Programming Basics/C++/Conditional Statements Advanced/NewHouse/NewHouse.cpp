#include <iostream>
using namespace std;

int main()
{
	string flowerType;
	int flowersCount, budget;
	cin >> flowerType >> flowersCount >> budget;

	double costs = 0;
	if (flowerType == "Roses") {
		costs = flowersCount * 5;
		if (flowersCount > 80) costs *= 0.9;
	}
	else if (flowerType == "Dahlias") {
		costs = flowersCount * 3.8;
		if (flowersCount > 90) costs *= 0.85;
	}
	else if (flowerType == "Tulips") {
		costs = flowersCount * 2.8;
		if (flowersCount > 80) costs *= 0.85;
	}
	else if (flowerType == "Narcissus") {
		costs = flowersCount * 3;
		if (flowersCount < 120) costs *= 1.15;
	}
	else if (flowerType == "Gladiolus") {
		costs = flowersCount * 2.5;
		if (flowersCount < 80) costs *= 1.2;
	}

	cout.setf(ios::fixed);
	cout.precision(2);
	if (costs <= budget) cout << "Hey, you have a great garden with " << flowersCount << " " << flowerType << " and " << budget - costs << " leva left." << endl;
	else cout << "Not enough money, you need " << costs - budget << " leva more." << endl;
}
