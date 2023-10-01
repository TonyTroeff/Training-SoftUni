#include <iostream>
#include <string>
using namespace std;

int main()
{
	int maxBadGradesCount;
	cin >> maxBadGradesCount;

	int currentBadGradesCount = 0, sum = 0, count = 0;
	bool isSuccessful = true;

	string input, lastProblem;

	cin >> ws;
	getline(cin, input);
	while (input != "Enough") {
		int grade;
		cin >> grade;

		sum += grade;
		count++;
		lastProblem = input;

		if (grade <= 4) {
			currentBadGradesCount++;
			if (currentBadGradesCount == maxBadGradesCount) {
				isSuccessful = false;
				break;
			}
		}

		// NOTE: The previous if statement is equivalent to:
		// if (grade <= 4 && ++currentBadGradesCount == maxBadGradesCount) {
		// 	  isSuccessful = false;
		//	  break;
		// }

		cin >> ws;
		getline(cin, input);
	}

	if (isSuccessful) {
		cout.setf(ios::fixed);
		cout.precision(2);

		cout << "Average score: " << 1.0 * sum / count << endl;
		cout << "Number of problems: " << count << endl;
		cout << "Last problem: " << lastProblem << endl;
	}
	else {
		cout << "You need a break, " << currentBadGradesCount << " poor grades." << endl;
	}
}
