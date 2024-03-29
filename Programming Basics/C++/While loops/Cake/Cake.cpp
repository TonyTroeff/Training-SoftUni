#include <iostream>
#include <string>
using namespace std;

int main()
{
	int width, length;
	cin >> width >> length;

	int totalPieces = width * length;
	bool cakeIsEnough = true;

	string input;
	while (cin >> input && input != "STOP") {
		int piecesToTake = stoi(input);
		totalPieces -= piecesToTake;

		if (totalPieces < 0) {
			cakeIsEnough = false;
			break;
		}
	}

	if (cakeIsEnough) cout << totalPieces << " pieces are left." << endl;
	else cout << "No more cake left! You need " << abs(totalPieces) << " pieces more." << endl;
}
