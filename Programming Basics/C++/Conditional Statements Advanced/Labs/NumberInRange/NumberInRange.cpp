#include <iostream>
using namespace std;

int main()
{
    int number;
    cin >> number;

    if (number != 0 && -100 <= number && number <= 100) cout << "Yes" << endl;
    else cout << "No" << endl;
}
