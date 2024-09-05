#include <iostream>
using namespace std;

int main()
{
    int limit;
    cin >> limit;

    int num = 1;
    while (num <= limit)
    {
        cout << num << endl;
        num = 2 * num + 1;
    }
}
