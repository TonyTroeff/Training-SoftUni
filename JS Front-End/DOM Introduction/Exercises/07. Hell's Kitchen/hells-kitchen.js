function solve() {
    const textArea = document.querySelector("div#inputs > textarea");

    const data = JSON.parse(textArea.value);
    const staffByRestaurant = {};

    for (const element of data) {
        const [restaurant, rawWorkersData] = element.split(" - ");
        if (!Object.hasOwn(staffByRestaurant, restaurant)) staffByRestaurant[restaurant] = [];

        for (const [worker, rawSalary] of rawWorkersData.split(", ").map((w) => w.split(" "))) {
            staffByRestaurant[restaurant].push({ name: worker, salary: Number(rawSalary) });
        }
    }

    let bestRestaurant = "";
    let bestAverageSalary = 0;
    for (const restaurant in staffByRestaurant) {
        const staff = staffByRestaurant[restaurant];
        if (staff.length === 0) continue;

        const averageSalary = staff.reduce((a, c) => a + c.salary, 0) / staff.length;
        if (averageSalary > bestAverageSalary) {
            bestAverageSalary = averageSalary;
            bestRestaurant = restaurant;
        }
    }

    const bestStaff = staffByRestaurant[bestRestaurant];
    const bestSalary = Math.max(...bestStaff.map((w) => w.salary));

    const bestRestaurantOutput = document.querySelector("div#outputs > div#bestRestaurant > p");
    bestRestaurantOutput.textContent = `Name: ${bestRestaurant} Average Salary: ${bestAverageSalary.toFixed(2)} Best Salary: ${bestSalary.toFixed(2)}`;

    const bestStaffOutput = document.querySelector("div#outputs > div#workers > p");
    bestStaffOutput.textContent = bestStaff
        .sort((a, b) => b.salary - a.salary)
        .map((w) => `Name: ${w.name} With Salary: ${w.salary}`)
        .join(" ");
}
