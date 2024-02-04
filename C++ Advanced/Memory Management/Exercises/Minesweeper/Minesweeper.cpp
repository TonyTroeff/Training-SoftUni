#include <iostream>
#include <memory>
#include <string>
using namespace std;

int main()
{
	int n, m;
	cin >> n >> m >> ws;

	unique_ptr<unique_ptr<int[]>[]> result = make_unique<unique_ptr<int[]>[]>(n);
	for (int i = 0; i < n; i++) result[i] = make_unique<int[]>(m);

	for (int i = 0; i < n; i++) {
		string input;
		getline(cin, input);

		for (int j = 0; j < input.length(); j++) {
			if (input[j] != '!') continue;

			for (int r = max(i - 1, 0); r <= min(i + 1, n - 1); r++) {
				for (int c = max(j - 1, 0); c <= min(j + 1, m - 1); c++) {
					result[r][c]++;
				}
			}
		}
	}

	for (int i = 0; i < n; i++) {
		for (int j = 0; j < m; j++) cout << result[i][j];
		cout << endl;
	}
}