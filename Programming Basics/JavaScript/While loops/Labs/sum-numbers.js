function solve(input) {
    const limit = Number(input[0]);

    let sum = 0;
    let index = 1;
    while (sum < limit) sum += Number(input[index++]);

    console.log(sum);
}
