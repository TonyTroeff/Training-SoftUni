function solve(input) {
    const number = Number(input[0]);

    if (number < 100) console.log("Less than 100");
    else if (number <= 200) console.log("Between 100 and 200");
    else console.log("Greater than 200");
}
