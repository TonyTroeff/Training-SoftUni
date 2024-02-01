#include <iostream>
#include <bitset>
using namespace std;

class Building {
public:
	string address;

	virtual ~Building()
	{
	}
};

class Shop : public Building {
public:
	string name;
};

void printSymbols(const char* letters, const int& length) {
	for (size_t i = 0; i < length; i++) cout << letters[i];
}

void printSymbol(const char& letter) {
	cout << letter;
}

void printBits(const void* ptr, size_t lengthInBytes) {
	const char* iterator = static_cast<const char*>(ptr);

	for (size_t i = 0; i < lengthInBytes; i++) {
		const char current = *(iterator + lengthInBytes - (i + 1));
		cout << bitset<8>(current);
	}
}

void corruptMemory(const char* letters) {
	const void* lettersPtr = letters;
	cout << lettersPtr << " (letters)" << endl;

	unsigned int* maliciousPtr = (unsigned int*)lettersPtr;
	cout << maliciousPtr << " (malicious pointer)" << endl;

	*maliciousPtr = 0x4D4F4F42;
}

void corruptMemory(const char& letter) {
	const void* letterPtr = &letter;
	cout << letterPtr << " (letter)" << endl;

	unsigned int* maliciousPtr = (unsigned int*)letterPtr;
	cout << maliciousPtr << " (malicious pointer)" << endl;

	*maliciousPtr = *maliciousPtr & ~((1 << 8) - 1) | 0x58; // If we clear more bits, a runtime exception will occur saying that `stack around variable .. was corrupted`.
}

void tryChangeConstValue(const char& letter) {
	char* letterPtr = const_cast<char*>(&letter); // if the referenced variable is actually const - we have undefined behavior

	const char originalValue = *letterPtr;
	*letterPtr = '!';
}

void demo1(const char* letters, const int& length) {
	cout << "----- Demonstrate memory corruption with an array of " << length << " characters -----" << endl;

	cout << "Before: "; printSymbols(letters, length); cout << endl;
	corruptMemory(letters);
	cout << "After: "; printSymbols(letters, length); cout << endl;

	cout << endl;
}

void demo2(const char& secretSymbol) {
	cout << "----- Demonstrate memory corruption with a single character -----" << endl;

	cout << "Before: "; printSymbol(secretSymbol); cout << endl;
	corruptMemory(secretSymbol);
	cout << "After: "; printSymbol(secretSymbol); cout << endl;

	cout << endl;
}

void demo3(const char& a, char& b) {
	cout << "----- Demonstrate how const values can be changed with const_cast -----" << endl;

	cout << "Before: "; printSymbol(a); printSymbol(b); cout << endl;
	tryChangeConstValue(a);
	tryChangeConstValue(b);
	cout << "After: "; printSymbol(a); printSymbol(b); cout << endl;

	cout << endl;
}

void demo4() {
	cout << "----- Demonstrate static_cast between int and float -----" << endl;

	float f = 12.3F;
	cout << "Before cast" << endl;
	cout << "Value: " << f << endl;
	cout << "Binary representation: "; printBits(&f, sizeof(float)); cout << endl;
	cout << "Address: " << &f << endl;
	cout << endl;

	int i = static_cast<int>(f);
	cout << "After cast" << endl;
	cout << "Value: " << i << endl;
	cout << "Binary representation: "; printBits(&i, sizeof(int)); cout << endl;
	cout << "Address: " << &i << endl;
	cout << endl;
}

void demo5() {
	cout << "----- Demonstrate reinterpret_cast -----" << endl;

	float f = 12.3F;
	cout << "Before cast" << endl;
	cout << "Value: " << f << endl;
	cout << "Binary representation: "; printBits(&f, sizeof(float)); cout << endl;
	cout << "Address: " << &f << endl;
	cout << endl;

	int* i = reinterpret_cast<int*>(&f);
	cout << "After cast" << endl;
	cout << "Value: " << *i << endl;
	cout << "Binary representation: "; printBits(i, sizeof(int)); cout << endl;
	cout << "Address: " << i << endl;
	cout << endl;
}

void demo6() {
	char standardLetter = 'a';

	void* voidPtr = &standardLetter; // Equivalent to `static_cast<void*>(&standardLetter)`
	char* charPtr = &standardLetter;

	int* badCast = static_cast<int*>(voidPtr); // When casting void pointers, we cannot benefit from the compile-time checking that static_cast gives us.

	// int* impossibleCast = static_cast<int*>(charPtr);
}

void demo7() {
	Building* building = new Building();
	building->address = "Sofia, St. Nevermind 13";

	Shop* shop = new Shop();
	shop->address = "Varna, St. Cherno more 10";
	shop->name = "We sell candies";

	Building* s1 = static_cast<Building*>(shop);
	Shop* s2 = static_cast<Shop*>(building); // undefined behavior

	Building* d1 = dynamic_cast<Building*>(shop);
	Shop* d2 = dynamic_cast<Shop*>(building);

	Building* r1 = reinterpret_cast<Building*>(shop);
	Shop* r2 = reinterpret_cast<Shop*>(building);
}

int main()
{
	cout << "The size of `char` is: " << sizeof(char) << " byte(s)." << endl;
	cout << "The size of `int` is: " << sizeof(int) << " byte(s)." << endl;
	cout << "The size of `float` is: " << sizeof(float) << " byte(s)." << endl;
	cout << endl;

	const int n = 4;
	const char letters[] = { 'a', 'b', 'c', 'd' };
	demo1(letters, n);

	const char secretSymbol = '*';
	demo2(secretSymbol);

	const char permanentSpecialSymbol = '#';
	char temporarySpecialSymbol = '@';
	demo3(permanentSpecialSymbol, temporarySpecialSymbol);

	demo4();
	demo5();
	demo6();
	demo7();
}