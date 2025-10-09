function solve(arr) {
    function operate(lowerBound, crystal, func, operation) {
        let count = 0;

        let result = crystal;
        let candidate = func(crystal);
        while (candidate >= lowerBound) {
            result = candidate;
            count++;

            candidate = func(candidate);
        }

        if (count > 0) {
            console.log(`${operation} x${count}`);
            console.log("Transporting and washing");
            result = Math.floor(result);
        }

        return result;
    }

    const target = arr[0];

    for (let i = 1; i < arr.length; i++) {
        let crystal = arr[i];
        console.log(`Processing chunk ${crystal} microns`);

        crystal = operate(target - 1, crystal, (x) => x / 4, "Cut");
        crystal = operate(target - 1, crystal, (x) => x * 0.8, "Lap");
        crystal = operate(target - 1, crystal, (x) => x - 20, "Grind");
        crystal = operate(target - 1, crystal, (x) => x - 2, "Etch");

        if (crystal === target - 1) {
            console.log("X-ray x1");
            crystal++;
        }

        console.log(`Finished crystal ${crystal} microns`);
    }
}

solve([1375, 50000]);
solve([1000, 4000, 8100]);
