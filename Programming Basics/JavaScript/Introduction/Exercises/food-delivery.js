function solve(input) {
    const chicken = Number(input[0]);
    const fish = Number(input[1]);
    const vegetarian = Number(input[2]);

    const basePrice = chicken * 10.35 + fish * 12.4 + vegetarian * 8.15;
    const desertPrice = 0.2 * basePrice;

    const total = basePrice + desertPrice + 2.5;
    console.log(total);
}
