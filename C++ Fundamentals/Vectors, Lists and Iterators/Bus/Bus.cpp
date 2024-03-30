#include <iostream>
#include <vector>
using namespace std;

vector<int> readVector(const int& n) {
    vector<int> result(n);
    for (int i = 0; i < n; i++) cin >> result[i];

    return result;
}

int normalizeTime(const int& time) {
    int hours = time / 100, minutes = time % 100;
    return hours * 60 + minutes;
}

int main()
{
    int n;
    cin >> n;

    vector<int> busses = readVector(n);
    
    int departure;
    cin >> departure;

    int normalizedDepartureTime = normalizeTime(departure);

    int minDiff = INT_MAX, minDiffPosition = -1;
    for (int i = 0; i < busses.size(); i++) {
        int normalizedArrivalTime = normalizeTime(busses[i]);
        int currentDiff = normalizedDepartureTime - normalizedArrivalTime;

        if (currentDiff >= 0 && currentDiff < minDiff) {
            minDiff = currentDiff;
            minDiffPosition = i + 1;
        }
    }

    cout << minDiffPosition << endl;
}
