#include <iostream>
using namespace std;

int main()
{
	int examHour, examMinutes, arrivalHour, arrivalMinutes;
	cin >> examHour >> examMinutes >> arrivalHour >> arrivalMinutes;

	int normalizedExamTime = examHour * 60 + examMinutes, normalizedArrivalTime = arrivalHour * 60 + arrivalMinutes;

	// Positive values => we have arrived early
	// Negative values => we are late
	int diffInMinutes = normalizedExamTime - normalizedArrivalTime;

	if (diffInMinutes > 30) cout << "Early" << endl;
	else if (diffInMinutes >= 0) cout << "On time" << endl;
	else cout << "Late" << endl;

	if (diffInMinutes != 0) {
		int absoluteDiffInMinutes = abs(diffInMinutes);
		if (absoluteDiffInMinutes < 60) cout << absoluteDiffInMinutes << " minutes";
		else {
			int formattedHours = absoluteDiffInMinutes / 60;
			int formattedMinutes = absoluteDiffInMinutes % 60;

			cout << formattedHours << ":" << string(formattedMinutes < 10, '0') << formattedMinutes << " hours";
		}

		if (diffInMinutes > 0) cout << " before";
		else cout << " after";

		cout << " the start" << endl;
	}
}
