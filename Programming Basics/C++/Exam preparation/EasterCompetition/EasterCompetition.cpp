#include <iostream>
#include <string>
using namespace std;

int main()
{
    int n;
    cin >> n;
    cin.ignore();

    int bestScore = 0;
    string bestBaker;

    for (int i = 0; i < n; i++) {
        string currentBaker;
        getline(cin, currentBaker);
        
        int currentScore = 0;
        string gradeInput;
        while (getline(cin, gradeInput) && gradeInput != "Stop")
            currentScore += stoi(gradeInput);
        
        cout << currentBaker << " has " << currentScore << " points." << endl;

        if (currentScore > bestScore) {
            bestScore = currentScore;
            bestBaker = currentBaker;

            cout << currentBaker << " is the new number 1!" << endl;
        }
    }

    cout << bestBaker << " won competition with " << bestScore << " points!" << endl;
}
