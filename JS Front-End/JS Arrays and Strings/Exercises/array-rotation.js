function solve(arr, rotations) {
    const result = [];
    for (let i = 0; i < arr.length; i++) result.push(arr[(i + rotations) % arr.length]);

    console.log(result.join(" "));
}

solve([51, 47, 32, 61, 21], 2);
solve([32, 21, 61, 1], 4);
solve([2, 4, 15, 31], 5);
