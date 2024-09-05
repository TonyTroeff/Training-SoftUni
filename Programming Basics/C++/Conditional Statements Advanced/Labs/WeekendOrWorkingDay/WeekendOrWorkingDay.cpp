#include <iostream>
using namespace std;

int main()
{
	string day;
	cin >> day;

	if (day == "Monday" || day == "Tuesday" || day == "Wednesday" || day == "Thursday" || day == "Friday") cout << "Working day" << endl;
	else if (day == "Saturday" || day == "Sunday") cout << "Weekend" << endl;
	else cout << "Error" << endl;
}
