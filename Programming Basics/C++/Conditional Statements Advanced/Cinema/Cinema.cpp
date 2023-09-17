#include <iostream>
using namespace std;

int main()
{
    string projectionType;
    int rows, cols;
    cin >> projectionType >> rows >> cols;

    int seats = rows * cols;

    double ticketPrice = 0;
    if (projectionType == "Premiere") ticketPrice = 12;
    else if (projectionType == "Normal") ticketPrice = 7.5;
    else if (projectionType == "Discount") ticketPrice = 5;

    double totalProfit = seats * ticketPrice;

    cout.setf(ios::fixed);
    cout.precision(2);

    cout << totalProfit << " leva" << endl;
}