#include <iostream>
#include <string>
using namespace std;

int main()
{
	string line;
	getline(cin, line);

	int index = 0;
	while (index < line.length()) {
		while (index < line.length() && !isalpha(line[index]))
			cout << line[index++];

		int start = index;
		while (index + 1 < line.length() && isalpha(line[index + 1]))
			index++;

		string currentWord = line.substr(start, index - start + 1);
		currentWord[0] = toupper(currentWord[0]);

		cout << currentWord;

		index++;
	}

	cout << endl;
}
