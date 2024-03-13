#include <iostream>
using namespace std;

double findBasePrice(const string& product) {
    if (product == "coffee") return 1.5;
    else if (product == "water") return 1;
    else if (product == "coke") return 1.4;
    else if (product == "snacks") return 2;

    return 0;
}

int main()
{
    string product;
    int quantity;
    cin >> product >> quantity;

    double total = quantity * findBasePrice(product);

    cout.setf(ios::fixed);
    cout.precision(2);

    cout << total << endl;
}
