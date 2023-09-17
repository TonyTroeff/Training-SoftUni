#include <iostream>
using namespace std;

int main()
{
	int degrees;
	string partOfDay;
	cin >> degrees >> partOfDay;

	string outfit, shoes;

	if (degrees <= 18) {
		if (partOfDay == "Morning") {
			outfit = "Sweatshirt";
			shoes = "Sneakers";
		}
		else if (partOfDay == "Afternoon" || partOfDay == "Evening") {
			outfit = "Shirt";
			shoes = "Moccasins";
		}
	}
	else if (degrees <= 24) {
		if (partOfDay == "Morning" || partOfDay == "Evening") {
			outfit = "Shirt";
			shoes = "Moccasins";
		}
		else if (partOfDay == "Afternoon") {
			outfit = "T-Shirt";
			shoes = "Sandals";
		}
	}
	else {
		if (partOfDay == "Morning") {
			outfit = "T-Shirt";
			shoes = "Sandals";
		}
		else if (partOfDay == "Afternoon") {
			outfit = "Swim Suit";
			shoes = "Barefoot";
		}
		if (partOfDay == "Evening") {
			outfit = "Shirt";
			shoes = "Moccasins";
		}
	}

	cout << "It's " << degrees << " degrees, get your " << outfit << " and " << shoes << "." << endl;
}