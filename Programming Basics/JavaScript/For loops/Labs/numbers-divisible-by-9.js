function solve(input) {
    let start = Number(input[0]);
    let end = Number(input[1]);

    const startRemainder = start % 9;
    if (startRemainder != 0) start += 9 - startRemainder;

    end -= end % 9;

    let sum = 0;
    for (let i = start; i <= end; i += 9) sum += i;

    console.log(`The sum: ${sum}`);
    for (let i = start; i <= end; i += 9) console.log(i);
}
