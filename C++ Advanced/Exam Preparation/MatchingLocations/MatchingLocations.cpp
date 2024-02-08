#include <iostream>
#include <sstream>
#include <string>
#include <unordered_map>
#include <vector>
using namespace std;

vector<string> split(const string& text, const char& delimiter) {
	vector<string> result;
	
	int index = 0;
	string word;
	while (index < text.length()) {
		while (index + 1 < text.length() && text[index + 1] != delimiter)
			word.push_back(text[index++]);

		result.push_back(word);
		word.clear();

		index++;
		while (index < text.length() && text[index] == delimiter) index++;
	}

	return result;
}

int main()
{
	unordered_map<string, string> locationsByName;
	unordered_map<string, vector<string>> locationsByCoordinates;

	string line;
	while (getline(cin, line) && line != ".") {
		vector<string> inputData = split(line, ',');

		locationsByName[inputData[0]] = line;

		string coordinatesKey;
		coordinatesKey.append(inputData[1]);
		coordinatesKey.push_back(' ');
		coordinatesKey.append(inputData[2]);

		locationsByCoordinates[coordinatesKey].push_back(line);
	}

	while (getline(cin, line) && line != ".") {
		auto matchByName = locationsByName.find(line);
		if (matchByName != locationsByName.end()) cout << matchByName->second << endl;

		auto matchByCoordinates = locationsByCoordinates.find(line);
		if (matchByCoordinates != locationsByCoordinates.end()) {
			for (const string& locationInfo : matchByCoordinates->second)
				cout << locationInfo << endl;
		}
	}
}
