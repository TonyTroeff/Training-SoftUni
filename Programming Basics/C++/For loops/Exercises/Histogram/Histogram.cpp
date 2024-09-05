#include <iostream>
using namespace std;

int main()
{
    int n;
    cin >> n;

    int p1 = 0, p2 = 0, p3 = 0, p4 = 0, p5 = 0;
    for (int i = 0; i < n; i++)
    {
        int currentNumber;
        cin >> currentNumber;

        if (currentNumber < 200) p1++;
        else if (currentNumber < 400) p2++;
        else if (currentNumber < 600) p3++;
        else if (currentNumber < 800) p4++;
        else p5++;
    }

    cout.setf(ios::fixed);
    cout.precision(2);

    cout << (p1 * 100.0) / n << "%" << endl;
    cout << (p2 * 100.0) / n << "%" << endl;
    cout << (p3 * 100.0) / n << "%" << endl;
    cout << (p4 * 100.0) / n << "%" << endl;
    cout << (p5 * 100.0) / n << "%" << endl;
}
