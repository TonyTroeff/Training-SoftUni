#include <iostream>
#include <memory>
using namespace std;

int main()
{
	int n, m;
	cin >> n >> m;

	int totalCells = n * m;
	unique_ptr<int[]> matrix = make_unique<int[]>(totalCells);
	for (int i = 0; i < totalCells; i++) {
		cin >> matrix[i];
	}

	int r = 3, c = 4;
	cin >> r >> c;
	for (int i = 0; i < r; i++) {
		for (int j = 0; j < c; j++)
			cout << matrix[i * m + j] << ' ';
		
		cout << endl;
	}
}
