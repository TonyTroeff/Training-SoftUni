function solve(input) {
    let primeSum = 0;
    let nonPrimeSum = 0;

    let index = 0;
    while (input[index] !== "stop") {
        const number = Number(input[index]);

        if (number < 0) console.log("Number is negative.");
        else {
            let isPrime = true;
            let numSqrt = Math.sqrt(number);
            for (let i = 2; i <= numSqrt; i++) {
                if (number % i === 0) {
                    isPrime = false;
                    break;
                }
            }

            if (isPrime) primeSum += number;
            else nonPrimeSum += number;
        }

        index++;
    }

    console.log(`Sum of all prime numbers is: ${primeSum}`);
    console.log(`Sum of all non prime numbers is: ${nonPrimeSum}`);
}
