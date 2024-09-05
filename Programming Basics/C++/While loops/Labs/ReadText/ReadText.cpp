#include <iostream>
#include <string>
using namespace std;

int main()
{
    string input;

    while (getline(cin, input) && input != "Stop")
        cout << input << endl;
}
