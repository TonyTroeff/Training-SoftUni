#include <iostream>
#include <memory>
#include <sstream>
#include <string>
using namespace std;

#define ROWS 10
#define COLS 10

unique_ptr<unique_ptr<int[]>[]> readMatrix() {
	unique_ptr<unique_ptr<int[]>[]> matrix = make_unique<unique_ptr<int[]>[]>(ROWS);
	for (int i = 0; i < ROWS; i++) matrix[i] = make_unique<int[]>(COLS);

	int r;
	cin >> r >> ws;

	if (r > ROWS) throw invalid_argument("The numbers of rows cannot exceed the predefined maximum.");

	for (int i = 0; i < r; i++) {
		string input;
		getline(cin, input);

		istringstream ss(input);

		int index = 0, number;
		while (ss >> number) matrix[i][index++] = number;
		while (index < COLS) matrix[i][index++] = 0;
	}

	return matrix;
}

bool areEqual(const unique_ptr<unique_ptr<int[]>[]>& a, const unique_ptr<unique_ptr<int[]>[]>& b) {
	for (int i = 0; i < ROWS; i++) {
		for (int j = 0; j < COLS; j++) {
			if (a[i][j] != b[i][j]) return false;
		}
	}

	return true;
}

int main()
{
	unique_ptr<unique_ptr<int[]>[]> m1 = readMatrix(), m2 = readMatrix();
	cout << (areEqual(m1, m2) ? "equal" : "not equal") << endl;
}


//#define TOTAL_CELLS ROWS * COLS

//unique_ptr<int[]> readFlatMatrix() {
//	unique_ptr<int[]> matrix = make_unique<int[]>(TOTAL_CELLS);
//	
//	int r;
//	cin >> r >> ws;
//
//	if (r > ROWS) throw invalid_argument("The numbers of rows cannot exceed the predefined maximum.");
//	
//	for (int i = 0; i < r; i++) {
//		string input;
//		getline(cin, input);
//
//		istringstream ss(input);
//		
//		int index = 0, number;
//		while (ss >> number) matrix[i * COLS + index++] = number;
//		while (index < COLS) matrix[i * COLS + index++] = 0;
//	}
//	
//	return matrix;
//}

//bool areEqual(const unique_ptr<int[]>& a, const unique_ptr<int[]>& b) {
//	for (int i = 0; i < TOTAL_CELLS; i++) {
//		if (a[i] != b[i]) return false;
//	}
//
//	return true;
//}

//int main()
//{
//	unique_ptr<int[]> m1 = readFlatMatrix(), m2 = readFlatMatrix();
//	cout << (areEqual(m1, m2) ? "equal" : "not equal") << endl;
//}