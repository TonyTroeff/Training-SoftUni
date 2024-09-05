#include <iostream>
using namespace std;

int main()
{
    string day;
    cin >> day;

    int price = 0;
    if (day == "Monday" || day == "Tuesday" || day == "Friday") price = 12;
    else if (day == "Wednesday" || day == "Thursday") price = 14;
    else if (day == "Saturday" || day == "Sunday") price = 16;

    cout << price << endl;
}
