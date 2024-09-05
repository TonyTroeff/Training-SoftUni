#include <iostream>
using namespace std;

int main()
{
	int n, doctors = 7, totalTreated = 0, totalUntreated = 0;
	cin >> n;

	for (int i = 0; i < n; i++) {
		if ((i + 1) % 3 == 0 && (totalUntreated > totalTreated)) doctors++;

		int patients;
		cin >> patients;

		totalTreated += min(doctors, patients);
		totalUntreated += max(0, patients - doctors);
	}

	cout << "Treated patients: " << totalTreated << '.' << endl;
	cout << "Untreated patients: " << totalUntreated << '.' << endl;
}