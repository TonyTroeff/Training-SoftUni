function solve(num) {
    let sum = 0;
    while (num !== 0) {
        sum += num % 10;
        num = Math.floor(num / 10);
    }

    console.log(sum);
}

solve(245678);
solve(97561);
solve(543);
