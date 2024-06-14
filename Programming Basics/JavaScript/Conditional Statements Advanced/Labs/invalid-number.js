function solve(input) {
    const number = Number(input[0]);

    const isValid = number == 0 || (100 <= number && number <= 200);
    if (!isValid) console.log("invalid");
}
