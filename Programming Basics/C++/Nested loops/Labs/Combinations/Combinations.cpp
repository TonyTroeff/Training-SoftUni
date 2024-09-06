#include <iostream>
using namespace std;

int main()
{
    int n;
    cin >> n;

    // Mathematical solution using arithmetic progressions. Complexity: O(1)
    /*int ans = (n + 1) * (n + 2) / 2;
    cout << ans << endl;*/

    int ans = 0;

    for (int i = 0; i <= n; i++)
    {
        int remaining = n - i;
        for (int j = 0; j <= remaining; j++)
        {
            // i + j + _ = n
            ans++;
        }
    }

    cout << ans << endl;
}
