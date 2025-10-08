function solve(arr) {
    function isReverseEqual(a, b) {
        if (a.length !== b.length) return false;
        for (let i = 0; i < a.length; i++) {
            if (a[i] !== b[b.length - 1 - i]) return false;
        }

        return true;
    }

    const username = arr[0];

    for (let i = 1; i < arr.length; i++) {
        const attempt = arr[i];

        if (isReverseEqual(username, attempt)) {
            console.log(`User ${username} logged in.`);
            break;
        } else if (i === 4) {
            console.log(`User ${username} blocked!`);
            break;
        } else {
            console.log("Incorrect password. Try again.");
        }
    }
}

solve(["Acer", "login", "go", "let me in", "recA"]);
solve(["momo", "omom"]);
solve(["sunny", "rainy", "cloudy", "sunny", "not sunny"]);
