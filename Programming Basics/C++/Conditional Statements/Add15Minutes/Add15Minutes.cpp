#include <iostream>
using namespace std;

int main()
{
	int hour, minutes;
	cin >> hour >> minutes;

	minutes += 15;
	if (minutes >= 60) {
		minutes -= 60;

		hour += 1;
		if (hour == 24) hour = 0;
	}

	cout << hour << ":" << string(minutes < 10, '0') << minutes << endl;
}
