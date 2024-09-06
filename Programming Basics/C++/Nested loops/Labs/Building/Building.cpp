#include <iostream>
using namespace std;

int main()
{
    int floors, rooms;
    cin >> floors >> rooms;

    for (int i = floors; i > 0; i--)
    {
        char letter;
        if (i == floors) letter = 'L';
        else if (i % 2 == 0) letter = 'O';
        else letter = 'A';

        for (int j = 0; j < rooms; j++)
        {
            if (j > 0) cout << ' ';
            cout << letter << i << j;
        }

        cout << endl;
    }
}
