function solve(input) {
    const wordRegex = /\w+/g;
    const words = input.match(wordRegex);
    console.log(words.map((x) => x.toUpperCase()).join(", "));
}

solve("Hi, how are you?");
solve("hello");
