#include <iostream>
#include <string>
#include <vector>
using namespace std;

bool parenthesesAreBalanced(const string& text) {
	vector<char> stack;
	for (int i = 0; i < text.length(); i++) {
		if (text[i] == '{') {
			if (stack.size() > 0 && (stack[stack.size() - 1] == '[' || stack[stack.size() - 1] == '(')) return false;
			stack.push_back(text[i]);
		}
		else if (text[i] == '[') {
			if (stack.size() > 0 && stack[stack.size() - 1] == '(') return false;
			stack.push_back(text[i]);
		}
		else if (text[i] == '(') {
			stack.push_back(text[i]);
		}
		else if (text[i] == '}') {
			if (stack.size() == 0 || stack[stack.size() - 1] != '{') return false;
			stack.erase(stack.end() - 1);
		}
		else if (text[i] == ']') {
			if (stack.size() == 0 || stack[stack.size() - 1] != '[') return false;
			stack.erase(stack.end() - 1);
		}
		else if (text[i] == ')') {
			if (stack.size() == 0 || stack[stack.size() - 1] != '(') return false;
			stack.erase(stack.end() - 1);
		}
	}

	return stack.size() == 0;
}

int main()
{
	string line;
	getline(cin, line);

	bool isCorrect = parenthesesAreBalanced(line);
	if (isCorrect) cout << "valid" << endl;
	else cout << "invalid" << endl;
}
