function solve(num, op1, op2, op3, op4, op5) {
    let result = Number(num);
    let operations = [op1, op2, op3, op4, op5];

    for (let i = 0; i < operations.length; i++) {
        let operation = operations[i];
        if (operation === "chop") result /= 2;
        else if (operation === "dice") result = Math.sqrt(result);
        else if (operation === "spice") result += 1;
        else if (operation === "bake") result *= 3;
        else if (operation === "fillet") result *= 0.8;

        console.log(result);
    }
}

solve("32", "chop", "chop", "chop", "chop", "chop");
solve("9", "dice", "spice", "chop", "bake", "fillet");
