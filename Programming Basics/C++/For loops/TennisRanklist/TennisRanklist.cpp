#include <iostream>
using namespace std;

int main()
{
    int n, initialPoints;
    cin >> n >> initialPoints;

    int newSeasonPoints = 0, wonTournamentsCount = 0;
    for (int i = 0; i < n; i++)
    {
        string position;
        cin >> position;

        if (position == "W")
        {
            newSeasonPoints += 2000;
            wonTournamentsCount++;
        }
        else if (position == "F") { newSeasonPoints += 1200; }
        else if (position == "SF") { newSeasonPoints += 720; }
    }

    cout.setf(ios::fixed);
    cout.precision(2);

    cout << "Final points: " << initialPoints + newSeasonPoints << endl;
    cout << "Average points: " << newSeasonPoints / n << endl;
    cout << (wonTournamentsCount * 100.0) / n << "%" << endl;
}

