function solve(input) {
    const limit = Number(input[0]);

    let num = 1;
    while (num <= limit) {
        console.log(num);
        num = 2 * num + 1;
    }
}
