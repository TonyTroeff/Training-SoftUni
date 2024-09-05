#include <iostream>
using namespace std;

int main()
{
    int number;
    cin >> number;

    bool isValid = number == 0 || (100 <= number && number <= 200);
    if (!isValid) cout << "invalid" << endl;
}
