function solve(input) {
    const dogFoodPrice = 2.5;
    const catFoodPrice = 4;

    const dogs = Number(input[0]);
    const cats = Number(input[1]);

    const totalCosts = dogs * dogFoodPrice + cats * catFoodPrice;
    console.log(`${totalCosts} lv.`);
}
