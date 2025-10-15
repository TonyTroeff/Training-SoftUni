function solve(input) {
    const units = {};

    const patterns = [/(?<leader>.+) arrives/, /(?<leader>.+): (?<army>.+), (?<count>\d+)/, /(?<army>.+) \+ (?<count>\d+)/, /(?<leader>.+) defeated/];

    for (const element of input) {
        if (patterns[0].test(element)) {
            const match = element.match(patterns[0]);
            const leader = match.groups.leader;
            units[leader] = { total: 0, armies: [] };
        } else if (patterns[1].test(element)) {
            const match = element.match(patterns[1]);
            const leader = match.groups.leader;
            const army = match.groups.army;
            const count = Number(match.groups.count);

            if (units.hasOwnProperty(leader)) {
                units[leader].armies.push({ name: army, count });
                units[leader].total += count;
            }
        } else if (patterns[2].test(element)) {
            const match = element.match(patterns[2]);
            const name = match.groups.army;
            const count = Number(match.groups.count);

            for (const assets of Object.values(units)) {
                for (const army of assets.armies) {
                    if (army.name === name) {
                        army.count += count;
                        assets.total += count;
                    }
                }
            }
        } else if (patterns[3].test(element)) {
            const match = element.match(patterns[3]);
            const leader = match.groups.leader;
            delete units[leader];
        }
    }

    for (const [leader, assets] of Object.entries(units).sort((a, b) => b[1].total - a[1].total)) {
        console.log(`${leader}: ${assets.total}`);

        for (const army of assets.armies.sort((a, b) => b.count - a.count)) {
            console.log(`>>> ${army.name} - ${army.count}`);
        }
    }
}

solve(["Rick Burr arrives", "Fergus: Wexamp, 30245", "Rick Burr: Juard, 50000", "Findlay arrives", "Findlay: Britox, 34540", "Wexamp + 6000", "Juard + 1350", "Britox + 4500", "Porter arrives", "Porter: Legion, 55000", "Legion + 302", "Rick Burr defeated", "Porter: Retix, 3205"]);
solve(["Rick Burr arrives", "Findlay arrives", "Rick Burr: Juard, 1500", "Wexamp arrives", "Findlay: Wexamp, 34540", "Wexamp + 340", "Wexamp: Britox, 1155", "Wexamp: Juard, 43423"]);
