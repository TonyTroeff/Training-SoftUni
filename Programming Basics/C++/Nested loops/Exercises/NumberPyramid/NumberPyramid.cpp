#include <iostream>
using namespace std;

int main()
{
    int n;
    cin >> n;

    int row = 1;
    int current = 1;

    while (current <= n)
    {
        for (int i = 0; i < row; i++)
        {
            cout << current << " ";
            if (++current > n) break;
        }

        row++;
        cout << endl;
    }
}
