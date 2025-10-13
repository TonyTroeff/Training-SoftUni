function solve(input) {
    let catalogue = {};

    for (let line of input) {
        let [product, price] = line.split(" : ");

        price = Number(price);
        catalogue[product] = price;
    }

    let prevLetter = "";
    for (const [product, price] of Object.entries(catalogue).sort((a, b) => a[0].localeCompare(b[0]))) {
        if (product[0] !== prevLetter) {
            prevLetter = product[0];
            console.log(prevLetter);
        }

        console.log(`  ${product}: ${price}`);
    }
}

solve(["Appricot : 20.4", "Fridge : 1500", "TV : 1499", "Deodorant : 10", "Boiler : 300", "Apple : 1.25", "Anti-Bug Spray : 15", "T-Shirt : 10"]);
solve(["Omlet : 5.4", "Shirt : 15", "Cake : 59"]);
