function solve(input) {
    const pricePerSquareMeter = 7.61;
    const discountPercentage = 0.18;

    const squareMeters = Number(input[0]);

    const totalCosts = squareMeters * pricePerSquareMeter;
    const discount = discountPercentage * totalCosts;
    const finalPrice = totalCosts - discount;

    console.log(`The final price is: ${finalPrice} lv.`);
    console.log(`The discount is: ${discount} lv.`);
}
