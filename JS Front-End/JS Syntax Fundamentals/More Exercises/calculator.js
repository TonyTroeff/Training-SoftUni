function solve(a, op, b) {
    let result = 0;
    if (op === "+") result = a + b;
    else if (op === "-") result = a - b;
    else if (op === "*") result = a * b;
    else if (op === "/") result = a / b;

    console.log(result.toFixed(2));
}

solve(5, "+", 10);
solve(25.5, "-", 3);
