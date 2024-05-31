function solve(input) {
    const nylon = new Number(input[0]);
    const paint = new Number(input[1]);
    const diluent = new Number(input[2]);
    const hours = new Number(input[3]);

    const materialsCost = 1.5 * (nylon + 2) + 14.5 * (paint * 1.1) + diluent * 5 + 0.4;
    const wage = 0.3 * materialsCost;

    const total = materialsCost + wage * hours;
    console.log(total);
}
