#include <iostream>
#include <queue>
#include <limits.h>
using namespace std;

const int adjacentCellsCount = 4;
const pair<int, int> directions[adjacentCellsCount] = { make_pair(0, -1), make_pair(1, 0), make_pair(0, 1), make_pair(-1, 0) };

template <size_t rows, size_t cols>
void spreadBfs(char(&matrix)[rows][cols], queue<pair<int, int>>& rustCells) {
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

template <size_t rows, size_t cols>
void solveWithBfs(char(&matrix)[rows][cols], const int& time) {
	queue<pair<int, int>> rustCells;
	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < cols; j++) {
			if (matrix[i][j] == '!') rustCells.push(make_pair(i, j));
		}
	}

	for (int i = 0; i < time; i++)
		spreadBfs(matrix, rustCells);
}

template <size_t rows, size_t cols>
void spreadDfs(const size_t& row, const size_t& col, char(&matrix)[rows][cols], int (&minDist)[rows][cols], const int& order, const int& time) {
	for (int t = 0; t < adjacentCellsCount; t++) {
		size_t nextRow = row + directions[t].second;
		if (nextRow < 0 || nextRow >= rows) continue;

		size_t nextCol = col + directions[t].first;
		if (nextCol < 0 || nextCol >= cols) continue;

		if (matrix[nextRow][nextCol] != '#' && minDist[nextRow][nextCol] > order) {
			matrix[nextRow][nextCol] = '!';
			minDist[nextRow][nextCol] = order;
			
			if (order < time)
				spreadDfs(nextRow, nextCol, matrix, minDist, order + 1, time);
		}
	}
}

template <size_t rows, size_t cols>
void solveWithDfs(char(&matrix)[rows][cols], const int& time) {
	int minDist[rows][cols];

	for (size_t i = 0; i < rows; i++) {
		for (size_t j = 0; j < cols; j++) {
			if (matrix[i][j] == '!') minDist[i][j] = 0;
			else minDist[i][j] = INT_MAX;
		}
	}

	for (size_t i = 0; i < rows; i++) {
		for (size_t j = 0; j < cols; j++) {
			if (minDist[i][j] == 0)
				spreadDfs(i, j, matrix, minDist, 1, time);
		}
	}
}

template <size_t rows, size_t cols>
void print(const char(&matrix)[rows][cols]) {
	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < cols; j++) cout << matrix[i][j];
		cout << endl;
	}
}

int main()
{
	const int rows = 10, cols = 10;
	char matrix[rows][cols]{};

	for (int i = 0; i < rows; i++) {
		for (int j = 0; j < cols; j++) cin >> matrix[i][j];
	}

	int time;
	cin >> time;

	// solveWithBfs(matrix, time);
	solveWithDfs(matrix, time);

	print(matrix);
}