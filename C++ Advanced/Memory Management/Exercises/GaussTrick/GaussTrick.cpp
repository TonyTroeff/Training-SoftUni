#include <iostream>
#include <memory>
#include <string>
#include <sstream>
using namespace std;

#define MAXN 1000

int readNumbers(const string& input, const unique_ptr<int[]>& ptr) {
	istringstream ss(input);
	int n = 0, current;
	while (ss >> current) ptr[n++] = current;

	return n;
}

int main()
{
	string input;
	getline(cin, input);

	unique_ptr<int[]> numbers = make_unique<int[]>(MAXN);
	int n = readNumbers(input, numbers);
	if (n > MAXN) throw logic_error("Too much numbers were entered.");

	for (int i = 0; i < n / 2; i++) cout << numbers[i] + numbers[n - (i + 1)] << ' ';
	
	if (n % 2 != 0) cout << numbers[n / 2];
	cout << endl;
}