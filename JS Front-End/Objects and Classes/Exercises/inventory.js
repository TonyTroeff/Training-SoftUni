function solve(data) {
    const heroes = [];
    for (const line of data) {
        const [name, level, items] = line.split(" / ");
        const hero = { name, level: Number(level), items };
        heroes.push(hero);
    }

    for (const hero of heroes.sort((a, b) => a.level - b.level)) {
        console.log(`Hero: ${hero.name}`);
        console.log(`level => ${hero.level}`);
        console.log(`items => ${hero.items}`);
    }
}

solve(["Isacc / 25 / Apple, GravityGun", "Derek / 12 / BarrelVest, DestructionSword", "Hes / 1 / Desolator, Sentinel, Antara"]);
solve(["Batman / 2 / Banana, Gun", "Superman / 18 / Sword", "Poppy / 28 / Sentinel, Antara"]);
