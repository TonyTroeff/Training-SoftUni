#include <iostream>
#include <string>
using namespace std;

int main()
{
	string input;
	getline(cin, input);

	int index = 0;
	string result;
	while (index < input.length()) {
		while (index + 1 < input.length() && input[index] == input[index + 1])
			index++;

		result.push_back(input[index]);
		index++;
	}

	cout << result << endl;
}
