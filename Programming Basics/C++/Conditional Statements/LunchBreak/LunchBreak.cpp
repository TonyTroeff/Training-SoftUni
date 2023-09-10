#include <iostream>
#include <math.h>
#include <string>
using namespace std;

int main()
{
	// 1. Read input
	string seriesName;
	int episodeDuration, breakDuration;

	getline(cin, seriesName);
	cin >> episodeDuration >> breakDuration;

	// 2. Calculate required time
	double lunchDuration = 0.125 * breakDuration;
	double restDuration = 0.25 * breakDuration;
	double necessaryDuration = episodeDuration + lunchDuration + restDuration;

	// 3. Print output
	if (breakDuration >= necessaryDuration)
		cout << "You have enough time to watch " << seriesName << " and left with " << ceil(breakDuration - necessaryDuration) << " minutes free time." << endl;
	else
		cout << "You don't have enough time to watch " << seriesName << ", you need " << ceil(necessaryDuration - breakDuration) << " more minutes." << endl;
}
