function solve(input) {
    const start = Number(input[0]);
    const end = Number(input[1]);

    let result = "";
    for (let i = start; i <= end; i++) {
        let evenSum = 0;
        let oddSum = 0;
        let isEven = true;

        let num = i;
        while (num !== 0) {
            let digit = num % 10;
            if (isEven) evenSum += digit;
            else oddSum += digit;

            isEven = !isEven;
            num = Math.floor(num / 10);
        }

        if (evenSum === oddSum) result += `${i} `;
    }

    console.log(result);
}
