#include <iostream>
#include <string>
#include <vector>
using namespace std;

vector<int> readVector(const int& n) {
    vector<int> result(n);
    for (int i = 0; i < n; i++) cin >> result[i];

    return result;
}

void printVector(const vector<int>& v) {
    for (vector<int>::const_iterator it = v.cbegin(); it < v.cend(); it++)
        cout << *it << ' ';
    cout << endl;
}

int main()
{
    int n;
    cin >> n;

    vector<int> checkup = readVector(n), installation = readVector(n);
    
    vector<int> result(n);
    for (int i = 0; i < n; i++) {
        int damagePerYear = installation[i] - checkup[i];
        result[i] = installation[i] / damagePerYear;
    }

    printVector(result);
}
