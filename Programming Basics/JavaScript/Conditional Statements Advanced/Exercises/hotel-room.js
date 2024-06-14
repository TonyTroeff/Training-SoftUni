function solve(input) {
    const month = input[0];
    const nights = Number(input[1]);

    let studioPrice = 0,
        apartmentPrice = 0;
    if (month === "May" || month === "October") {
        studioPrice = 50;
        apartmentPrice = 65;

        if (nights > 14) studioPrice *= 0.7;
        else if (nights > 7) studioPrice *= 0.95;
    } else if (month === "June" || month === "September") {
        studioPrice = 75.2;
        apartmentPrice = 68.7;

        if (nights > 14) studioPrice *= 0.8;
    } else if (month === "July" || month === "August") {
        studioPrice = 76;
        apartmentPrice = 77;
    }

    if (nights > 14) apartmentPrice *= 0.9;

    const apartmentCosts = apartmentPrice * nights,
        studioCosts = studioPrice * nights;

    console.log(`Apartment: ${apartmentCosts.toFixed(2)} lv.`);
    console.log(`Studio: ${studioCosts.toFixed(2)} lv.`);
}
