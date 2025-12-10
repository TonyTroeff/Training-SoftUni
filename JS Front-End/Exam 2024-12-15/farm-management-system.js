function solve(input) {
    // Most important step!
    const farmers = {};

    const n = Number(input[0]);
    for (let i = 1; i <= n; i++) {
        const data = input[i].split(" ");

        const name = data[0];
        const workArea = data[1];
        const tasks = data[2].split(",");

        // NOTE: Should we use object for tasks?
        farmers[name] = { workArea, tasks };
    }

    for (let commandPos = n + 1; input[commandPos] != "End"; commandPos++) {
        const data = input[commandPos].split(" / ");

        const command = data[0];
        if (command === "Execute") {
            const farmerName = data[1];
            const workArea = data[2];
            const task = data[3];

            // NOTE: Should we check if a farmer with this name exists?
            const requestedFarmer = farmers[farmerName];
            if (requestedFarmer.workArea === workArea && requestedFarmer.tasks.includes(task)) {
                console.log(`${farmerName} has executed the task: ${task}!`);
            } else {
                console.log(`${farmerName} cannot execute the task: ${task}.`);
            }
        } else if (command === "Change Area") {
            const farmerName = data[1];
            const newWorkArea = data[2];

            // NOTE: Should we check if a farmer with this name exists?
            const requestedFarmer = farmers[farmerName];
            requestedFarmer.workArea = newWorkArea;

            console.log(`${farmerName} has changed their work area to: ${newWorkArea}`);
        } else if (command === "Learn Task") {
            const farmerName = data[1];
            const newTask = data[2];

            // NOTE: Should we check if a farmer with this name exists?
            const requestedFarmer = farmers[farmerName];
            if (requestedFarmer.tasks.includes(newTask)) {
                console.log(`${farmerName} already knows how to perform ${newTask}.`);
            } else {
                requestedFarmer.tasks.push(newTask);
                console.log(`${farmerName} has learned a new task: ${newTask}.`);
            }
        }
    }

    for (const name in farmers) {
        const info = farmers[name];

        // Suggestion...
        console.log(`Farmer: ${name}, Area: ${info.workArea}, Tasks: ${info.tasks.sort().join(", ")}`);
    }
}

solve(["2", "John garden watering,weeding", "Mary barn feeding,cleaning", "Execute / John / garden / watering", "Execute / Mary / garden / feeding", "Learn Task / John / planting", "Execute / John / garden / planting", "Change Area / Mary / garden", "Execute / Mary / garden / cleaning", "End"]);
solve(["3", "Alex apiary harvesting,honeycomb", "Emma barn milking,cleaning", "Chris garden planting,weeding", "Execute / Alex / apiary / harvesting", "Learn Task / Alex / beeswax", "Execute / Alex / apiary / beeswax", "Change Area / Emma / apiary", "Execute / Emma / apiary / milking", "Execute / Chris / garden / watering", "Learn Task / Chris / pruning", "Execute / Chris / garden / pruning", "End"]);
