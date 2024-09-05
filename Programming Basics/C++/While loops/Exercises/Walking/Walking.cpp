#include <iostream>
#include <string>
using namespace std;

int main()
{
	const int goalLimit = 10000;

	int totalSteps = 0;
	bool shouldContinue = true, goalIsReached = false;

	while (shouldContinue && !goalIsReached) {
		string input;
		getline(cin, input);

		int currentSteps;
		if (input == "Going home") {
			cin >> currentSteps;
			shouldContinue = false;
		}
		else currentSteps = stoi(input);

		totalSteps += currentSteps;
		if (totalSteps >= goalLimit) goalIsReached = true;
	}

	if (goalIsReached) cout << "Goal reached! Good job!" << endl;
	else cout << goalLimit - totalSteps << " more steps to reach goal." << endl;
}
