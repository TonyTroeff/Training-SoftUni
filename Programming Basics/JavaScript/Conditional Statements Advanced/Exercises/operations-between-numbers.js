function solve(input) {
    const n1 = Number(input[0]);
    const n2 = Number(input[1]);
    const operation = input[2];

    if ((operation === "/" || operation === "%") && n2 === 0) {
        console.log(`Cannot divide ${n1} by zero`);
    } else {
        if (operation === "/") {
            console.log(`${n1} / ${n2} = ${(n1 / n2).toFixed(2)}`);
        } else {
            let showParity = true;
            let result = 0;

            if (operation === "+") result = n1 + n2;
            else if (operation === "-") result = n1 - n2;
            else if (operation === "*") result = n1 * n2;
            else if (operation === "%") {
                result = n1 % n2;
                showParity = false;
            }

            let resultText = `${n1} ${operation} ${n2} = ${result}`;

            if (showParity) {
                const parity = result % 2 === 0 ? "even" : "odd";
                resultText += ` - ${parity}`;
            }

            console.log(resultText);
        }
    }
}
