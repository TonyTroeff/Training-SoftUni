function solve(arr) {
    const words = arr[0].split(" ");

    const freq = {};
    for (const word of words) freq[word] = 0;

    for (let i = 1; i < arr.length; i++) {
        const word = arr[i];
        if (word in freq) freq[word]++;
    }

    for (const [word, count] of Object.entries(freq).sort((a, b) => b[1] - a[1])) console.log(`${word} - ${count}`);
}

solve(["this sentence", "In", "this", "sentence", "you", "have", "to", "count", "the", "occurances", "of", "the", "words", "this", "and", "sentence", "because", "this", "is", "your", "task"]);
solve(["is the", "first", "sentence", "Here", "is", "another", "the", "And", "finally", "the", "the", "sentence"]);
