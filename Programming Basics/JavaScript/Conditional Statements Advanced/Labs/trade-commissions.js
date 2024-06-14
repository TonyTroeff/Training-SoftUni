function solve(input) {
    const city = input[0];
    const sales = Number(input[1]);

    let commissionPercentage = -1;
    if (city === "Sofia") {
        if (0 <= sales && sales <= 500) commissionPercentage = 0.05;
        else if (500 < sales && sales <= 1000) commissionPercentage = 0.07;
        else if (1000 < sales && sales <= 10000) commissionPercentage = 0.08;
        else if (10000 < sales) commissionPercentage = 0.12;
    } else if (city === "Varna") {
        if (0 <= sales && sales <= 500) commissionPercentage = 0.045;
        else if (500 < sales && sales <= 1000) commissionPercentage = 0.075;
        else if (1000 < sales && sales <= 10000) commissionPercentage = 0.1;
        else if (10000 < sales) commissionPercentage = 0.13;
    } else if (city === "Plovdiv") {
        if (0 <= sales && sales <= 500) commissionPercentage = 0.055;
        else if (500 < sales && sales <= 1000) commissionPercentage = 0.08;
        else if (1000 < sales && sales <= 10000) commissionPercentage = 0.12;
        else if (10000 < sales) commissionPercentage = 0.145;
    }

    if (commissionPercentage === -1) console.log("error");
    else {
        const commission = sales * commissionPercentage;
        console.log(commission.toFixed(2));
    }
}
