function solve(input) {
    const bookToLookFor = input[0];

    let booksCount = 0;
    let isFound = false;

    let index = 1;
    while (input[index] != "No More Books") {
        if (input[index] == bookToLookFor) {
            isFound = true;
            break;
        }

        booksCount++;
        index++;
    }

    if (isFound) console.log(`You checked ${booksCount} books and found it.`);
    else {
        console.log("The book you search is not here!");
        console.log(`You checked ${booksCount} books.`);
    }
}
