#include <iostream>
#include <queue>
using namespace std;

const int rows = 10, cols = 10, adjacentCellsCount = 4;
const pair<int, int> directions[adjacentCellsCount] = { make_pair(0, -1), make_pair(1, 0), make_pair(0, 1), make_pair(-1, 0) };

void spread(char(&matrix)[rows][cols], queue<pair<int, int>>& rustCells) {
	size_t generationSize = rustCells.size();

	for (size_t i = 0; i < generationSize; i++) {
		pair<int, int> &current = rustCells.front();
		rustCells.pop();

		for (int t = 0; t < adjacentCellsCount; t++) {
			int nextRow = current.first + directions[t].second;
			if (nextRow < 0 || nextRow >= rows) continue;

			int nextCol = current.second + directions[t].first;
			if (nextCol < 0 || nextCol >= cols) continue;

			if (matrix[nextRow][nextCol] == '.') {
				matrix[nextRow][nextCol] = '!';
				rustCells.push(make_pair(nextRow, nextCol));
			}
		}
	}
}

void print(const char(&matrix)[rows][cols]) {
	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < cols; j++) cout << matrix[i][j];
		cout << endl;
	}
}

int main()
{
	char matrix[rows][cols]{};

	queue<pair<int, int>> rustCells;
	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < cols; j++) {
			cin >> matrix[i][j];

			if (matrix[i][j] == '!') rustCells.push(make_pair(i, j));
		}
	}

	int time;
	cin >> time;

	for (int i = 0; i < time; i++)
		spread(matrix, rustCells);

	print(matrix);
}