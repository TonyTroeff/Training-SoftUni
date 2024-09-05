#include <iostream>
#include <string>
using namespace std;

int main()
{
    cout.setf(ios::fixed);
    cout.precision(2);

    double amount = 0;

    string input;
    while (cin >> input && input != "NoMoreMoney")
    {
        double operation = stod(input);

        if (operation < 0)
        {
            cout << "Invalid operation!" << endl;
            break;
        }

        cout << "Increase: " << operation << endl;
        amount += operation;
    }

    cout << "Total: " << amount << endl;
}
