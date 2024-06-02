function solve(input) {
    const nylon = Number(input[0]);
    const paint = Number(input[1]);
    const diluent = Number(input[2]);
    const hours = Number(input[3]);

    const materialsCost = 1.5 * (nylon + 2) + 14.5 * (paint * 1.1) + diluent * 5 + 0.4;
    const wage = 0.3 * materialsCost;

    const total = materialsCost + wage * hours;
    console.log(total);
}
