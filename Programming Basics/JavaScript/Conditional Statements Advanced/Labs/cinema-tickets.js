function solve(input) {
    const day = input[0];

    let price = 0;
    if (day === "Monday" || day === "Tuesday" || day === "Friday") price = 12;
    else if (day === "Wednesday" || day === "Thursday") price = 14;
    else if (day === "Saturday" || day === "Sunday") price = 16;

    console.log(price);
}
