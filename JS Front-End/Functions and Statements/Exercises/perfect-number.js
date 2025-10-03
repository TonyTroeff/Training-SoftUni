function solve(num) {
    const sqrt = Math.sqrt(num);
    let divisorsSum = 0;

    for (let i = 1; i <= sqrt; i++) {
        if (num % i !== 0) continue;

        divisorsSum += i;
        const otherDivisor = num / i;
        if (i !== otherDivisor) divisorsSum += otherDivisor;
    }

    if (num === divisorsSum / 2) console.log("We have a perfect number!");
    else console.log("It's not so perfect.");
}

solve(6);
solve(28);
solve(1236498);
