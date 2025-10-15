function solve(input) {
    const garages = {};

    for (const element of input) {
        const data = element.split(" - ");
        const garageNumber = data[0];

        const carInfo = {};
        data[1].split(", ").forEach((prop) => {
            const [key, value] = prop.split(": ");
            carInfo[key] = value;
        });

        if (!garages.hasOwnProperty(garageNumber)) garages[garageNumber] = [];
        garages[garageNumber].push(carInfo);
    }

    for (const [garageNumber, cars] of Object.entries(garages)) {
        console.log(`Garage № ${garageNumber}`);

        for (const car of cars) {
            const info = Object.entries(car)
                .map(([key, value]) => `${key} - ${value}`)
                .join(", ");
            console.log(`--- ${info}`);
        }
    }
}

solve(["1 - color: blue, fuel type: diesel", "1 - color: red, manufacture: Audi", "2 - fuel type: petrol", "4 - color: dark blue, fuel type: diesel, manufacture: Fiat"]);
solve(["1 - color: green, fuel type: petrol", "1 - color: dark red, manufacture: WV", "2 - fuel type: diesel", "3 - color: dark blue, fuel type: petrol"]);
