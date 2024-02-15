#include <iostream>
#include <string>
using namespace std;

int main()
{
	int n;
	cin >> n;
	cin.ignore();

	int back = 0, chest = 0, legs = 0, abs = 0, shake = 0, bar = 0;
	for (int i = 0; i < n; i++) {
		string input;
		getline(cin, input);

		if (input == "Back") back++;
		else if (input == "Chest") chest++;
		else if (input == "Legs") legs++;
		else if (input == "Abs") abs++;
		else if (input == "Protein shake") shake++;
		else if (input == "Protein bar") bar++;
	}

	double trainingPercentage = 100.0 * (back + chest + legs + abs) / n;
	double buyingPercentage = 100.0 * (shake + bar) / n;

	cout.setf(ios::fixed);
	cout.precision(2);

	cout << back << " - back" << endl;
	cout << chest << " - chest" << endl;
	cout << legs << " - legs" << endl;
	cout << abs << " - abs" << endl;
	cout << shake << " - protein shake" << endl;
	cout << bar << " - protein bar" << endl;
	cout << trainingPercentage << "% - work out" << endl;
	cout << buyingPercentage << "% - protein" << endl;
}
