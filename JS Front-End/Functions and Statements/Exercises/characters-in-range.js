function solve(from, to) {
    const start = Math.min(from.charCodeAt(0), to.charCodeAt(0));
    const stop = Math.max(from.charCodeAt(0), to.charCodeAt(0));

    const result = [];
    for (let i = start + 1; i < stop; i++) result.push(String.fromCharCode(i));

    console.log(result.join(" "));
}

solve("a", "d");
solve("#", ":");
solve("C", "#");
