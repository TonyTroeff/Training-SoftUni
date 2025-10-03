function solve(n) {
    const matrix = Array.from({ length: n }, () => Array.from({ length: n }, () => n));

    for (const row of matrix) console.log(row.join(" "));
}

solve(3);
solve(7);
solve(2);
