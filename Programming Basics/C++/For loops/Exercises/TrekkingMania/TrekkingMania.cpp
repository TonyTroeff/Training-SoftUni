#include <iostream>
using namespace std;

int main()
{
    int n;
    cin >> n;

    int climbersCount = 0, p1 = 0, p2 = 0, p3 = 0, p4 = 0, p5 = 0;
    for (int i = 0; i < n; i++)
    {
        int peopleCount;
        cin >> peopleCount;

        if (peopleCount <= 5) { p1 += peopleCount; }
        else if (peopleCount <= 12) { p2 += peopleCount; }
        else if (peopleCount <= 25) { p3 += peopleCount; }
        else if (peopleCount <= 40) { p4 += peopleCount; }
        else { p5 += peopleCount; }

        climbersCount += peopleCount;
    }

    cout.setf(ios::fixed);
    cout.precision(2);

    cout << (p1 * 100.0) / climbersCount << "%" << endl;
    cout << (p2 * 100.0) / climbersCount << "%" << endl;
    cout << (p3 * 100.0) / climbersCount << "%" << endl;
    cout << (p4 * 100.0) / climbersCount << "%" << endl;
    cout << (p5 * 100.0) / climbersCount << "%" << endl;
}
