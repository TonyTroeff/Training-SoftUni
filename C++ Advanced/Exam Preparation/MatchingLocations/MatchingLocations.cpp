#include <iostream>
#include <map>
#include <sstream>
#include <string>
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
	map<string, string> locationsByName;
	map<pair<double, double>, vector<string>> locationsByCoordinates; // Using unordered_map with pairs is tricky.

	string line;
	while (getline(cin, line) && line != ".") {
		vector<string> inputData = split(line, ',');
		string name = inputData[0];
		double latitude = stod(inputData[1]), longitude = stod(inputData[2]);

		locationsByName[name] = line;
		locationsByCoordinates[make_pair(latitude, longitude)].push_back(line);
	}

	while (getline(cin, line) && line != ".") {
		vector<string> inputData = split(line, ' ');
		if (inputData.size() == 1) {
			auto match = locationsByName.find(inputData[0]);
			if (match != locationsByName.end()) cout << match->second << endl;
		}
		else if (inputData.size() == 2) {
			pair<double, double> query = make_pair(stod(inputData[0]), stod(inputData[1]));
			auto match = locationsByCoordinates.find(query);
			if (match != locationsByCoordinates.end()) {
				for (const string& location : match->second) cout << location << endl;
			}
		}
	}
}
