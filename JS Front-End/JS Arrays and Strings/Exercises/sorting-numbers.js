function solve(arr) {
    arr.sort((a, b) => a - b);

    const result = [];
    for (let i = 0; i < arr.length; i++) {
        if (i % 2 === 0) result.push(arr[i / 2]);
        else result.push(arr[arr.length - (i + 1) / 2]);
    }

    return result;
}

solve([1, 65, 3, 52, 48, 63, 31, -3, 18, 56]);
