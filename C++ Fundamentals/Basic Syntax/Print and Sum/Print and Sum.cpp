#include <iostream>
using namespace std;

int main()
{
    int start, end;
    cin >> start >> end;

    int sum = 0;
    for (int i = start; i <= end; i++) {
        sum += i;
        cout << i << ' ';
    }

    cout << endl;
    cout << "Sum: " << sum << endl;
}
