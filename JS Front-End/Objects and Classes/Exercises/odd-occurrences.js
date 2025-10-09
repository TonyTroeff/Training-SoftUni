function solve(text) {
    const words = text.split(" ");

    const freq = {};
    for (const word of words) {
        const lowerCased = word.toLowerCase();

        if (!(lowerCased in freq)) freq[lowerCased] = 0;
        freq[lowerCased]++;
    }

    const result = [];
    for (const [word, count] of Object.entries(freq)) {
        if (count % 2 !== 0) result.push(word);
    }

    console.log(result.join(" "));
}

solve("Java C# Php PHP Java PhP 3 C# 3 1 5 C#");
solve("Cake IS SWEET is Soft CAKE sweet Food");
