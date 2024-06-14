function solve(input) {
    const budget = Number(input[0]);
    const season = input[1];

    let destination = "",
        accommodationType = "";
    let totalCosts = 0;

    if (budget <= 100) {
        destination = "Bulgaria";
        if (season === "summer") totalCosts = 0.3 * budget;
        else if (season === "winter") totalCosts = 0.7 * budget;
    } else if (budget <= 1000) {
        destination = "Balkans";
        if (season === "summer") totalCosts = 0.4 * budget;
        else if (season === "winter") totalCosts = 0.8 * budget;
    } else {
        destination = "Europe";
        accommodationType = "Hotel";
        totalCosts = 0.9 * budget;
    }

    if (accommodationType.length == 0) {
        if (season === "summer") {
            accommodationType = "Camp";
        } else if (season === "winter") {
            accommodationType = "Hotel";
        }
    }

    console.log(`Somewhere in ${destination}`);
    console.log(`${accommodationType} - ${totalCosts.toFixed(2)}`);
}
