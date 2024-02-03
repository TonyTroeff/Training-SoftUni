#include <iostream>
#include <string>
#include <cstring>
#include <sstream>
using namespace std;

void solve1() {
	string input;
	while (getline(cin, input) && input != "end") {
		stringstream ss(input);

		int id;
		string name;
		ss >> id >> name;

		if (id >= 0) cout << id << " " << name << endl;
	}
}

void solve2() {
	// We will use the following bit pattern: |next|id|name|
	void* dataBegin = nullptr;
	void* dataEnd = nullptr;

	string input;
	while (getline(cin, input) && input != "end") {
		stringstream ss(input);

		int id;
		string name;
		ss >> id >> name;

		size_t requiredSize = sizeof(void*) + sizeof(id) + (name.length() + 1);

		void* currentBlockPtr = malloc(requiredSize);
		if (!currentBlockPtr) throw bad_alloc();

		if (!dataBegin) {
			dataBegin = currentBlockPtr;
		}
		else {
			void** prevToNextPtr = reinterpret_cast<void**>(dataEnd);
			*(prevToNextPtr) = currentBlockPtr;
		}

		char* currentBytesPtr = reinterpret_cast<char*>(currentBlockPtr);

		void** nextPtr = reinterpret_cast<void**>(currentBlockPtr);
		*nextPtr = NULL;

		int* idPtr = reinterpret_cast<int*>(currentBytesPtr + sizeof(void*));
		*idPtr = id;

		char* namePtr = reinterpret_cast<char*>(currentBytesPtr + sizeof(void*) + sizeof(int));
		copy(name.begin(), name.end(), namePtr);
		namePtr[name.length()] = NULL;

		dataEnd = currentBytesPtr;
	}

	void* iter = dataBegin;
	while (iter) {
		char* currentBytes = reinterpret_cast<char*>(iter);

		void** nextPtr = reinterpret_cast<void**>(currentBytes);
		void* next = *nextPtr;

		int* idPtr = reinterpret_cast<int*>(currentBytes + sizeof(void*));
		if (*idPtr >= 0) {
			char* namePtr = reinterpret_cast<char*>(currentBytes + sizeof(void*) + sizeof(int));
			cout << *idPtr << ' ' << namePtr << endl;
		}

		free(iter);
		iter = next;
	}
}

void solve3() {
	// We will use the following bit pattern: |next|id|name|
	char* dataBegin = nullptr;
	char* dataEnd = nullptr;

	string input;
	while (getline(cin, input) && input != "end") {
		stringstream ss(input);

		int id;
		string name;
		ss >> id >> name;

		size_t requiredSize = sizeof(char*) + sizeof(id) + (name.length() + 1);

		char* currentBytesPtr = new char[requiredSize];
		if (!dataBegin) {
			dataBegin = currentBytesPtr;
		}
		else {
			char** prevToNextPtr = reinterpret_cast<char**>(dataEnd);
			*(prevToNextPtr) = currentBytesPtr;
		}

		char** nextPtr = reinterpret_cast<char**>(currentBytesPtr);
		*nextPtr = NULL;

		int* idPtr = reinterpret_cast<int*>(currentBytesPtr + sizeof(char*));
		*idPtr = id;

		char* namePtr = reinterpret_cast<char*>(currentBytesPtr + sizeof(char*) + sizeof(int));
		copy(name.begin(), name.end(), namePtr);
		namePtr[name.length()] = NULL;

		dataEnd = currentBytesPtr;
	}

	char* iter = dataBegin;
	while (iter) {
		char* currentBytes = reinterpret_cast<char*>(iter);

		char** nextPtr = reinterpret_cast<char**>(currentBytes);
		char* next = *nextPtr;

		int* idPtr = reinterpret_cast<int*>(currentBytes + sizeof(char*));
		if (*idPtr >= 0) {
			char* namePtr = reinterpret_cast<char*>(currentBytes + sizeof(char*) + sizeof(int));
			cout << *idPtr << ' ' << namePtr << endl;
		}

		delete[] iter;
		iter = next;
	}
}

int main() {
	// solve1();
	// solve2();
	// solve3();
}