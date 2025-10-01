function solve(word, text) {
    const pattern = new RegExp(`\\b${word}\\b`, "i");
    if (pattern.test(text)) console.log(word);
    else console.log(`${word} not found!`);
}

solve("javascript", "JavaScript is the best programming language");
solve("python", "JavaScript is the best programming language");
