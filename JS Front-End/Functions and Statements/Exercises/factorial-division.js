function solve(a, b) {
    let min = Math.min(a, b);
    let max = Math.max(a, b);
    let res = 1;
    for (let i = min + 1; i <= max; i++) res *= i;

    if (a < b) res = 1 / res;

    console.log(res.toFixed(2));
}

solve(5, 2);
solve(6, 2);
