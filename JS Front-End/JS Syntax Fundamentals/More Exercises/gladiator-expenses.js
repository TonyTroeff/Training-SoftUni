function solve(lostFights, helmetPrice, swordPrice, shieldPrice, armorPrice) {
    const cost = Math.floor(lostFights / 2) * helmetPrice + Math.floor(lostFights / 3) * swordPrice + Math.floor(lostFights / 6) * shieldPrice + Math.floor(lostFights / 12) * armorPrice;
    console.log(`Gladiator expenses: ${cost.toFixed(2)} aureus`);
}

solve(7, 2, 3, 4, 5);
solve(23, 12.5, 21.5, 40, 200);
