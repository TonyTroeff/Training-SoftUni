function solve(stock, ordered) {
    const store = {};

    const all = [...stock, ...ordered];
    for (let i = 0; i < all.length; i += 2) {
        const product = all[i];
        let quantity = Number(all[i + 1]);
        if (store.hasOwnProperty(product)) quantity += store[product];

        store[product] = quantity;
    }

    for (const product in store) console.log(`${product} -> ${store[product]}`);
}

solve(["Chips", "5", "CocaCola", "9", "Bananas", "14", "Pasta", "4", "Beer", "2"], ["Flour", "44", "Oil", "12", "Pasta", "7", "Tomatoes", "70", "Bananas", "30"]);
solve(["Salt", "2", "Fanta", "4", "Apple", "14", "Water", "4", "Juice", "5"], ["Sugar", "44", "Oil", "12", "Apple", "7", "Tomatoes", "7", "Bananas", "30"]);
