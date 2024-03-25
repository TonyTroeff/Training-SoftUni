#include <iostream>
#include <string>
using namespace std;

int main()
{
	string line;
	getline(cin, line);

	for (int i = 0; i < line.length(); i++) line[i] += 3;

	cout << line << endl;
}
