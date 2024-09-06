#include <iostream>
using namespace std;

int main()
{
    int start, end, magicNumber;
    cin >> start >> end >> magicNumber;

    bool isFound = false;
    int order = 0;
    for (int i = start; i <= end; i++)
    {
        for (int j = start; j <= end; j++)
        {
            order++;

            if (i + j == magicNumber)
            {
                cout << "Combination N:" << order << " (" << i << " + " << j << " = " << magicNumber << ")" << endl;
                isFound = true;
                break;
            }
        }

        if (isFound) break;
    }

    if (!isFound)
        cout << order << " combinations - neither equals " << magicNumber << endl;
}
