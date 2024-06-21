function solve(input) {
    const n = Number(input[0]);
    for (let i = 0, val = 1; i <= n; i += 2, val *= 4) console.log(val);
}
