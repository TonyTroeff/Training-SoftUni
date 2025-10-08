function solve(x1, y1, x2, y2) {
    function calculateDistance(x, y) {
        return Math.sqrt(Math.pow(x, 2) + Math.pow(y, 2));
    }

    function checkValidity(x1, y1, x2, y2) {
        const distance = calculateDistance(x2 - x1, y2 - y1);
        const validity = Number.isInteger(distance) ? "valid" : "invalid";
        console.log(`{${x1}, ${y1}} to {${x2}, ${y2}} is ${validity}`);
    }

    checkValidity(x1, y1, 0, 0);
    checkValidity(x2, y2, 0, 0);
    checkValidity(x1, y1, x2, y2);
}

solve(3, 0, 0, 4);
solve(2, 1, 1, 1);
