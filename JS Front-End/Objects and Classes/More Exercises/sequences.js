function solve(input) {
    const result = {};
    input.forEach((element) => {
        const arr = JSON.parse(element).sort((a, b) => b - a);
        result[JSON.stringify(arr)] = arr;
    });

    Object.entries(result)
        .sort((a, b) => a[1].length - b[1].length)
        .forEach(([_, value]) => console.log(`[${value.join(", ")}]`));
}

solve(["[-3, -2, -1, 0, 1, 2, 3, 4]", "[10, 1, -17, 0, 2, 13]", "[4, -3, 3, -2, 2, -1, 1, 0]"]);
solve(["[7.14, 7.180, 7.339, 80.099]", "[7.339, 80.0990, 7.140000, 7.18]", "[7.339, 7.180, 7.14, 80.099]"]);
