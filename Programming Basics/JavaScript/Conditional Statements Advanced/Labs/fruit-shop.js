function solve(input) {
    const product = input[0];
    const day = input[1];
    const quantity = Number(input[2]);

    let price = -1;
    if (day === "Monday" || day === "Tuesday" || day === "Wednesday" || day === "Thursday" || day === "Friday") {
        if (product === "banana") price = 2.5;
        else if (product === "apple") price = 1.2;
        else if (product === "orange") price = 0.85;
        else if (product === "grapefruit") price = 1.45;
        else if (product === "kiwi") price = 2.7;
        else if (product === "pineapple") price = 5.5;
        else if (product === "grapes") price = 3.85;
    } else if (day === "Saturday" || day === "Sunday") {
        if (product === "banana") price = 2.7;
        else if (product === "apple") price = 1.25;
        else if (product === "orange") price = 0.9;
        else if (product === "grapefruit") price = 1.6;
        else if (product === "kiwi") price = 3;
        else if (product === "pineapple") price = 5.6;
        else if (product === "grapes") price = 4.2;
    }

    if (price === -1) console.log("error");
    else {
        const totalCosts = price * quantity;
        console.log(totalCosts.toFixed(2));
    }
}
