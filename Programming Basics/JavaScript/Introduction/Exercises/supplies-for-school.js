function solve(input) {
    const pens = new Number(input[0]);
    const markers = new Number(input[1]);
    const diluent = new Number(input[2]);
    const discountPercentage = new Number(input[3]);

    const total = (pens * 5.8 + markers * 7.2 + diluent * 1.2) * (1 - discountPercentage * 0.01);
    console.log(total);
}
