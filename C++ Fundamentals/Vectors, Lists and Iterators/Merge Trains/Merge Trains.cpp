#include <iostream>
#include <limits.h>
#include <sstream>
#include <string>
#include <vector>
using namespace std;

vector<int> readTrack() {
    string line;
    getline(cin, line);

    vector<int> result;
    istringstream iss(line);

    int number;
    while (iss >> number) result.push_back(number);

    return result;
}

void printVector(const vector<int>& v) {
    for (vector<int>::const_iterator it = v.cbegin(); it < v.cend(); it++)
        cout << *it << ' ';
    cout << endl;
}

int main()
{
    vector<int> trackA = readTrack(), trackB = readTrack();

    vector<int> result(trackA.size() + trackB.size());
    ostringstream operations;

    int indexA = trackA.size() - 1, indexB = trackB.size() - 1;
    while (indexA >= 0 || indexB >= 0) {
        int elementA = INT_MAX, elementB = INT_MAX;

        if (indexA >= 0) elementA = trackA[indexA];
        if (indexB >= 0) elementB = trackB[indexB];
        
        int resultIndex = indexA + indexB + 1;
        if (elementA <= elementB) {
            operations << 'A';
            result[resultIndex] = trackA[indexA];
            indexA--;
        }
        else {
            operations << 'B';
            result[resultIndex] = trackB[indexB];
            indexB--;
        }
    }

    cout << operations.str() << endl;
    printVector(result);
}