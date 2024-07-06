function solve(input) {
    const money = Number(input[0]);

    let cents = Math.floor(money * 100);

    let coinsCount = 0;
    while (cents > 0) {
        if (cents >= 200) cents -= 200;
        else if (cents >= 100) cents -= 100;
        else if (cents >= 50) cents -= 50;
        else if (cents >= 20) cents -= 20;
        else if (cents >= 10) cents -= 10;
        else if (cents >= 5) cents -= 5;
        else if (cents >= 2) cents -= 2;
        else cents -= 1;

        coinsCount++;
    }

    console.log(coinsCount);
}
