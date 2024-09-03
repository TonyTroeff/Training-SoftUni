#include <iostream>
using namespace std;

int main()
{
    const string expected = "s3cr3t!P@ssw0rd";

    string password;
    cin >> password;

    if (password == expected) cout << "Welcome" << endl;
    else cout << "Wrong password!" << endl;
}
