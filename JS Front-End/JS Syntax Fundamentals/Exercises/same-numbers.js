function solve(num) {
    let lastDigit = num % 10;
    let allSame = true;
    let sum = 0;

    while (num !== 0) {
        const currentDigit = num % 10;

        if (currentDigit !== lastDigit) allSame = false;
        sum += currentDigit;

        num = Math.floor(num / 10);
    }

    console.log(allSame);
    console.log(sum);
}

solve(2222222);
solve(1234);
