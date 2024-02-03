#include <iostream>
#include <string>
#include <sstream>
#include <vector>
using namespace std;

typedef bool(action)(const string& a, const string& b);

static bool isShorter(const string& a, const string& b) {
	return a.length() < b.length();
}

static bool isLonger(const string& a, const string& b) {
	return a.length() > b.length();
}

static bool isLexicographicallySmaller(const string& a, const string& b) {
	return a < b;
}

// static action* getActionPointer(const int& operationCode) {
static bool(*getActionPointer(const int& operationCode)) (const string& a, const string& b) {
	switch (operationCode)
	{
	case 1:
		return isLexicographicallySmaller;
	case 2:
		return isShorter;
	case 3:
		return isLonger;
	default:
		return nullptr;
	}
}

static void solveWithPointers(const string& input, const int& operationCode) {
	stringstream wordsStream(input);

	vector<string> allWords;
	string currentWord;
	while (wordsStream >> currentWord) allWords.push_back(currentWord);

	bool (*predicate)(const string & a, const string & b) = getActionPointer(operationCode);
	if (predicate == nullptr) throw std::logic_error("Action pointer could not be obtained.");

	string result;
	for (size_t i = 0; i < allWords.size(); i++) {
		if (i == 0 || predicate(allWords[i], result))
			result = allWords[i];
	}

	cout << result << endl;
}

//static action& getActionReference(const int& operationCode) {
static bool(&getActionReference(const int& operationCode)) (const string& a, const string& b) {
	switch (operationCode)
	{
	case 1:
		return isLexicographicallySmaller;
	case 2:
		return isShorter;
	case 3:
		return isLonger;
	default:
		throw std::invalid_argument("Invalid operation code.");
	}
}

static void solveWithReferences(const string& input, const int& operationCode) {
	stringstream wordsStream(input);

	vector<string> allWords;
	string currentWord;
	while (wordsStream >> currentWord) allWords.push_back(currentWord);

	bool(&predicate)(const string & a, const string & b) = getActionReference(operationCode);

	string result;
	for (size_t i = 0; i < allWords.size(); i++) {
		if (i == 0 || predicate(allWords[i], result))
			result = allWords[i];
	}

	cout << result << endl;
}

int main()
{
	string input;
	getline(cin, input);

	int operationCode;
	cin >> operationCode;

	solveWithReferences(input, operationCode);
}