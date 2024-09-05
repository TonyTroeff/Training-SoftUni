#include <iostream>
#include <string>
using namespace std;

int main()
{
    double necessaryAmount, savings;
    cin >> necessaryAmount >> savings;

    int consecutiveSpendOperations = 0, totalDays = 0;
    bool canSave = true;
    while (savings < necessaryAmount) {
        string operation;
        double operationAmount;
        cin >> operation >> operationAmount;

        totalDays++;

        if (operation == "save") {
            consecutiveSpendOperations = 0;
            savings += operationAmount;
        }
        else if (operation == "spend") {
            if (++consecutiveSpendOperations == 5) {
                canSave = false;
                break;
            }

            savings = max(savings - operationAmount, 0.0);
        }
    }

    if (canSave) cout << "You saved the money for " << totalDays << " days." << endl;
    else cout << "You can't save the money." << endl << totalDays << endl;
}
