function solve(commands) {
    const parking = {};
    for (const command of commands) {
        const [action, carNumber] = command.split(", ");

        if (action === "IN") {
            parking[carNumber] = true;
        } else if (action === "OUT") {
            delete parking[carNumber];
        }
    }

    const sortedCars = Object.keys(parking).sort((a, b) => a.localeCompare(b));
    if (sortedCars.length === 0) {
        console.log("Parking Lot is Empty");
    } else {
        for (const car of sortedCars) console.log(car);
    }
}

solve(["IN, CA2844AA", "IN, CA1234TA", "OUT, CA2844AA", "IN, CA9999TT", "IN, CA2866HI", "OUT, CA1234TA", "IN, CA2844AA", "OUT, CA2866HI", "IN, CA9876HH", "IN, CA2822UU"]);
solve(["IN, CA2844AA", "IN, CA1234TA", "OUT, CA2844AA", "OUT, CA1234TA"]);
