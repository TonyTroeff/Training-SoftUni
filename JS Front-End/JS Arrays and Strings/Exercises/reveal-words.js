function solve(secrets, text) {
    for (const word of secrets.split(", ")) {
        const mask = "*".repeat(word.length);
        text = text.replace(mask, word);
    }

    console.log(text);
}

solve("great", "softuni is ***** place for learning new programming languages");
solve("great, learning", "softuni is ***** place for ******** new programming languages");
