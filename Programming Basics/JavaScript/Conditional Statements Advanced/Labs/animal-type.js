function solve(input) {
    const animal = input[0];

    if (animal === "dog") console.log("mammal");
    else if (animal === "crocodile" || animal === "tortoise" || animal === "snake") console.log("reptile");
    else console.log("unknown");
}
