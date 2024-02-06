#include <iostream>
#include <set>
#include <sstream>
#include <string>
#include <unordered_map>
#include <vector>
using namespace std;

char toLower(const char& letter) {
    if ('A' <= letter && letter <= 'Z') return 'a' + (letter - 'A');
    return letter;
}

vector<string> readWords() {
    string input;
    getline(cin, input);

    vector<string> result;
    string word;

    for (size_t i = 0; i < input.length(); i++) {
        if (isalpha(input[i])) word.push_back(input[i]);
        else if (word.length() != 0) {
            result.push_back(word);
            word.clear();
        }
    }

    if (word.length()) result.push_back(word);

    return result;
}

unordered_map<char, set<string>> categorizeWords(const vector<string>& words) {
    unordered_map<char, set<string>> result;

    for (size_t i = 0; i < words.size(); i++) {
        for (size_t j = 0; j < words[i].length(); j++) {
            char mapKey = toLower(words[i][j]);
            result[mapKey].insert(words[i]);
        }
    }

    return result;
}

void printWords(const set<string>& words) {
    for (const string& w : words)
        cout << w << ' ';
    cout << endl;
}

int main() {
    unordered_map<char, set<string>> catalog = categorizeWords(readWords());

    char query;
    while (cin >> query && query != '.') {
        char mapKey = toLower(query);

        auto match = catalog.find(mapKey);
        if (match == catalog.end()) cout << "---" << endl;
        else printWords(match->second);
    }
}