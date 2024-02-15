#include <iostream>
using namespace std;

int main()
{
	string town, packageType, hasVipDiscount;
	int daysToStay;
	cin >> town >> packageType >> hasVipDiscount >> daysToStay;

	if (daysToStay < 1) cout << "Days must be positive number!" << endl;
	else {
		double basePrice = 0, vipDiscount = 0;
		bool isValid = true;
		if (town == "Bansko" || town == "Borovets") {
			if (packageType == "withEquipment") {
				basePrice = 100;
				vipDiscount = 0.1;
			}
			else if (packageType == "noEquipment") {
				basePrice = 80;
				vipDiscount = 0.05;
			}
			else isValid = false;
		}
		else if (town == "Varna" || town == "Burgas") {
			if (packageType == "withBreakfast") {
				basePrice = 130;
				vipDiscount = 0.12;
			}
			else if (packageType == "noBreakfast") {
				basePrice = 100;
				vipDiscount = 0.07;
			}
			else isValid = false;
		}
		else isValid = false;

		if (!isValid) cout << "Invalid input!" << endl;
		else {
			int daysToTax = daysToStay;
			if (daysToStay > 7) daysToTax--;

			double totalCosts = daysToTax * basePrice;
			if (hasVipDiscount == "yes") totalCosts *= 1 - vipDiscount;

			cout.setf(ios::fixed);
			cout.precision(2);

			cout << "The price is " << totalCosts << "lv! Have a nice time!" << endl;
		}
	}
}
