#include <iostream>
#include <string>
using namespace std;

int main()
{
	string text, query, replace;
	getline(cin, text);
	getline(cin, query);
	getline(cin, replace);

	int offset = 0, index;
	while ((index = text.find(query, offset)) != -1) {
		text.replace(index, query.length(), replace);
		offset = index + replace.length();
	}

	cout << text << endl;
}
