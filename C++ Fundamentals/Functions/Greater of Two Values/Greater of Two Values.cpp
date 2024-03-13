#include <iostream>
#include <string>
using namespace std;

int getMax(const int& a, const int& b) {
    if (a > b) return a;
    return b;
}

char getMax(const char& a, const char& b) {
    if (a > b) return a;
    return b;
}

string getMax(const string& a, const string& b) {
    if (a > b) return a;
    return b;
}

int main()
{
    string type;
    cin >> type;

    if (type == "int") {
        int a, b;
        cin >> a >> b;

        int max = getMax(a, b);
        cout << max << endl;
    }
    else if (type == "char") {
        char a, b;
        cin >> a >> b;

        char max = getMax(a, b);
        cout << max << endl;
    }
    else if (type == "string") {
        string a, b;
        cin.ignore();

        getline(cin, a);
        getline(cin, b);

        string max = getMax(a, b);
        cout << max << endl;
    }
}
