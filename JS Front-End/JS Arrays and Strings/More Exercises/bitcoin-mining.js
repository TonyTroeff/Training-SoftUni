function solve(earnings) {
    const goldPrice = 67.51;
    const bitcoinPrice = 11949.16;

    let money = 0;
    let firstBitcoinPurchase = -1;
    for (let i = 0; i < earnings.length; i++) {
        let dailyGold = Number(earnings[i]);
        if ((i + 1) % 3 === 0) dailyGold *= 0.7;

        money += dailyGold * goldPrice;
        if (firstBitcoinPurchase === -1 && money >= bitcoinPrice) firstBitcoinPurchase = i + 1;
    }

    const totalBitcoins = Math.floor(money / bitcoinPrice);
    money -= totalBitcoins * bitcoinPrice;

    console.log(`Bought bitcoins: ${totalBitcoins}`);
    if (firstBitcoinPurchase !== -1) console.log(`Day of the first purchased bitcoin: ${firstBitcoinPurchase}`);
    console.log(`Left money: ${money.toFixed(2)} lv.`);
}

solve([100, 200, 300]);
solve([50, 100]);
solve([3124.15, 504.212, 2511.124]);
