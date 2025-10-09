function solve(num) {
    let sum = 0;
    let count = 0;

    let copy = num;
    while (copy !== 0) {
        sum += copy % 10;
        count++;

        copy = Math.floor(copy / 10);
    }

    let res = num;
    while (sum / count <= 5) {
        res = 10 * res + 9;
        sum += 9;
        count++;
    }

    console.log(res);
}

solve(101);
solve(5835);
