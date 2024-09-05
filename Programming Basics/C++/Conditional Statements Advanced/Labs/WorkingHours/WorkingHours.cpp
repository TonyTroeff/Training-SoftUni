#include <iostream>
using namespace std;

int main()
{
    int hour;
    string day;
    cin >> hour >> day;

    bool isWorkingDay = day == "Monday" || day == "Tuesday" || day == "Wednesday" || day == "Thursday" || day == "Friday" || day == "Saturday";
    bool isWorkingHour = 10 <= hour && hour <= 18;

    if (isWorkingDay && isWorkingHour) cout << "open" << endl;
    else cout << "closed" << endl;
}
