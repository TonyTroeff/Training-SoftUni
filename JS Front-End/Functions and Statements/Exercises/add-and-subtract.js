function solve(a, b, c) {
    function sum(x, y) {
        return x + y;
    }

    function subtract(x, y) {
        return x - y;
    }

    const result = subtract(sum(a, b), c);
    console.log(result);
}

solve(23, 6, 10);
solve(1, 17, 30);
solve(42, 58, 100);
