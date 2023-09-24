#include <iostream>
#include <string>
using namespace std;

int main()
{
	const double boundary = 1250.5;

	string actor, academyPointsInput, juriesCountInput;
	getline(cin, actor);
	getline(cin, academyPointsInput);
	getline(cin, juriesCountInput);

	double points = stod(academyPointsInput);
	int juriesCount = stoi(juriesCountInput);

	for (int i = 0; points <= boundary && i < juriesCount; i++) {
		string juryName, juryPointsInput;
		getline(cin, juryName);
		getline(cin, juryPointsInput);

		double juryPoints = stod(juryPointsInput);
		points += juryPoints * juryName.size() / 2;
	}

	cout.setf(ios::fixed);
	cout.precision(1);

	if (points > boundary) cout << "Congratulations, " << actor << " got a nominee for leading role with " << points << "!" << endl;
	else cout << "Sorry, " << actor << " you need " << boundary - points << " more!" << endl;
}
