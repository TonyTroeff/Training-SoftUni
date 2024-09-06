#include <iostream>
using namespace std;

int main()
{
    string destination;
    while (cin >> destination && destination != "End")
    {
        double minBudget;
        cin >> minBudget;

        double savedMoney = 0;
        while (savedMoney < minBudget)
        {
            double currentAmount;
            cin >> currentAmount;

            savedMoney += currentAmount;
        }

        cout << "Going to " << destination << "!" << endl;
    }
}
