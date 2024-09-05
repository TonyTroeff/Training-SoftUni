#include <iostream>
using namespace std;

int main()
{
    string city;
    double sales;
    cin >> city >> sales;

    double commissionPercentage = -1;
    if (city == "Sofia")
    {
        if (0 <= sales && sales <= 500) commissionPercentage = 0.05;
        else if (500 < sales && sales <= 1000) commissionPercentage = 0.07;
        else if (1000 < sales && sales <= 10000) commissionPercentage = 0.08;
        else if (10000 < sales) commissionPercentage = 0.12;
    }
    else if (city == "Varna")
    {
        if (0 <= sales && sales <= 500) commissionPercentage = 0.045;
        else if (500 < sales && sales <= 1000) commissionPercentage = 0.075;
        else if (1000 < sales && sales <= 10000) commissionPercentage = 0.1;
        else if (10000 < sales) commissionPercentage = 0.13;
    }
    else if (city == "Plovdiv")
    {
        if (0 <= sales && sales <= 500) commissionPercentage = 0.055;
        else if (500 < sales && sales <= 1000) commissionPercentage = 0.08;
        else if (1000 < sales && sales <= 10000) commissionPercentage = 0.12;
        else if (10000 < sales) commissionPercentage = 0.145;
    }

    if (commissionPercentage == -1) cout << "error" << endl;
    else
    {
        double commission = sales * commissionPercentage;

        cout.setf(ios::fixed);
        cout.precision(2);
        cout << commission << endl;
    }
}
