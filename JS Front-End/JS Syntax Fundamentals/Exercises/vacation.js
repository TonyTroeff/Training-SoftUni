function solve(peopleCount, groupType, dayOfWeek) {
    let pricePerPerson = 0;

    if (groupType === "Students") {
        if (dayOfWeek === "Friday") pricePerPerson = 8.45;
        else if (dayOfWeek === "Saturday") pricePerPerson = 9.8;
        else if (dayOfWeek === "Sunday") pricePerPerson = 10.46;
    } else if (groupType === "Business") {
        if (dayOfWeek === "Friday") pricePerPerson = 10.9;
        else if (dayOfWeek === "Saturday") pricePerPerson = 15.6;
        else if (dayOfWeek === "Sunday") pricePerPerson = 16;
    } else if (groupType === "Regular") {
        if (dayOfWeek === "Friday") pricePerPerson = 15;
        else if (dayOfWeek === "Saturday") pricePerPerson = 20;
        else if (dayOfWeek === "Sunday") pricePerPerson = 22.5;
    }

    let totalPrice = peopleCount * pricePerPerson;
    if (groupType === "Students" && peopleCount >= 30) totalPrice *= 0.85;
    else if (groupType === "Business" && peopleCount >= 100) totalPrice -= pricePerPerson * 10;
    else if (groupType === "Regular" && peopleCount >= 10 && peopleCount <= 20) totalPrice *= 0.95;

    console.log(`Total price: ${totalPrice.toFixed(2)}`);
}

solve(30, "Students", "Sunday");
solve(40, "Regular", "Saturday");
