#include <iostream>
#include <memory>
#include <sstream>
#include <string>
using namespace std;

#define MAXN 1000

int readNumbers(const string& input, const unique_ptr<int[]>& ptr) {
	istringstream ss(input);
	int n = 0, current;
	while (ss >> current) ptr[n++] = current;

	return n;
}

int remove(const unique_ptr<int[]>& ptr, const int& n, bool (*predicate)(const int&)) {
	int offset = 0;
	for (int i = 0; i < n; i++) {
		if (predicate(ptr[i])) {
			offset++;
			ptr[i] = 0;
		}
		else if (offset != 0) {
			ptr[i - offset] = ptr[i];
			ptr[i] = 0;
		}
	}

	return offset;
}

void swap(const unique_ptr<int[]>& ptr, const int& i1, const int& i2) {
	int temp = ptr[i1];
	ptr[i1] = ptr[i2];
	ptr[i2] = temp;
}

void reverse(const unique_ptr<int[]>& ptr, const int& n) {
	int half = n / 2;
	for (int i = 0; i < half; i++) swap(ptr, i, n - (i + 1));
}

int main()
{
	string input;
	getline(cin, input);

	unique_ptr<int[]> numbers = make_unique<int[]>(MAXN);
	int n = readNumbers(input, numbers);
	n -= remove(numbers, n, [](const int& x) { return x < 0; });

	if (n == 0) cout << "empty" << endl;
	else {
		reverse(numbers, n);
		for (int i = 0; i < n; i++) cout << numbers[i] << ' ';
		cout << endl;
	}
}
