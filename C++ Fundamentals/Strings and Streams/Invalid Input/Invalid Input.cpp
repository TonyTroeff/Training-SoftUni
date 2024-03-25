#include <iostream>
#include <sstream>
#include <string>
using namespace std;

int main()
{
	string line;
	getline(cin, line);

	istringstream inputStream(line);
	ostringstream outputStream;
	
	int sum = 0;
	int number; 
	string word;
	while (inputStream) {
		int position = inputStream.tellg();
		if (inputStream >> number) sum += number;
		else {
			inputStream.clear();
			inputStream.seekg(position);

			if (inputStream >> word) outputStream << word << ' ';
		}
	}

	cout << sum << endl;
	cout << outputStream.str() << endl;
}
