function solve(input) {
    const degrees = Number(input[0]);
    const partOfDay = input[1];

    let outfit = "",
        shoes = "";
    if (degrees <= 18) {
        if (partOfDay === "Morning") {
            outfit = "Sweatshirt";
            shoes = "Sneakers";
        } else if (partOfDay === "Afternoon" || partOfDay === "Evening") {
            outfit = "Shirt";
            shoes = "Moccasins";
        }
    } else if (degrees <= 24) {
        if (partOfDay === "Morning" || partOfDay === "Evening") {
            outfit = "Shirt";
            shoes = "Moccasins";
        } else if (partOfDay === "Afternoon") {
            outfit = "T-Shirt";
            shoes = "Sandals";
        }
    } else {
        if (partOfDay === "Morning") {
            outfit = "T-Shirt";
            shoes = "Sandals";
        } else if (partOfDay === "Afternoon") {
            outfit = "Swim Suit";
            shoes = "Barefoot";
        } else if (partOfDay === "Evening") {
            outfit = "Shirt";
            shoes = "Moccasins";
        }
    }

    console.log(`It's ${degrees} degrees, get your ${outfit} and ${shoes}.`);
}
