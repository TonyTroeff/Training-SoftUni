#include <iostream>
using namespace std;

int main()
{
    int firstTime, secondTime, thirdTime;
    cin >> firstTime >> secondTime >> thirdTime;

    int total = firstTime + secondTime + thirdTime;

    int minutes = total / 60, seconds = total % 60;

    // First approach:
    // if (seconds < 10) cout << minutes << ":0" << seconds << endl;
    // else cout << minutes << ":" << seconds << endl;

    // Second approach:
    // cout << minutes << ":";
    // if (seconds < 10) cout << "0";
    // cout << seconds << endl;
    
    // Third approach:
    cout << minutes << ":" << string(seconds < 10, '0') << seconds << endl;
}
