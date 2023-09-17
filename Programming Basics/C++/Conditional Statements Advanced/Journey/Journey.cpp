#include <iostream>
using namespace std;

int main()
{
	double budget;
	string season;
	cin >> budget >> season;

	string destination = "", accommodationType = "";
	double totalCosts = 0;

	if (budget <= 100) {
		destination = "Bulgaria";
		if (season == "summer") totalCosts = 0.3 * budget;
		else if (season == "winter") totalCosts = 0.7 * budget;
	}
	else if (budget <= 1000) {
		destination = "Balkans";
		if (season == "summer") totalCosts = 0.4 * budget;
		else if (season == "winter") totalCosts = 0.8 * budget;
	}
	else {
		destination = "Europe";
		accommodationType = "Hotel";
		totalCosts = 0.9 * budget;
	}

	if (accommodationType.size() == 0) {
		if (season == "summer") {
			accommodationType = "Camp";
		}
		else if (season == "winter") {
			accommodationType = "Hotel";
		}
	}

	cout.setf(ios::fixed);
	cout.precision(2);
	cout << "Somewhere in " << destination << endl;
	cout << accommodationType << " - " << totalCosts << endl;
}
