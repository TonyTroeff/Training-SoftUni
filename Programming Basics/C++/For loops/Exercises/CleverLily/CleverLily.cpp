#include <iostream>
using namespace std;

int main()
{
    int age, toyPrice;
    double washingMachinePrice;
    
    cin >> age >> washingMachinePrice >> toyPrice;

    int savedMoney = 0, moneyToReceive = 10;
    for (int i = 1; i <= age; i++)
    {
        if (i % 2 == 0)
        {
            savedMoney += moneyToReceive - 1;
            moneyToReceive += 10;
        }
        else { savedMoney += toyPrice; }
    }

    cout.setf(ios::fixed);
    cout.precision(2);

    if (savedMoney >= washingMachinePrice) cout << "Yes! " << savedMoney - washingMachinePrice << endl;
    else cout << "No! " << washingMachinePrice - savedMoney << endl;
}
