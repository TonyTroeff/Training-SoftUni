function solve(fruit, grams, pricePerKg) {
    const kg = 0.001 * grams;
    const total = kg * pricePerKg;

    console.log(`I need $${total.toFixed(2)} to buy ${kg.toFixed(2)} kilograms ${fruit}.`);
}

solve("orange", 2500, 1.8);
solve("apple", 1563, 2.35);
