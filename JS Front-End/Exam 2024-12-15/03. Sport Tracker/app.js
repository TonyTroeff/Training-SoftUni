const url = "http://localhost:3030/jsonstore/workout";

const loadButton = document.getElementById("load-workout");
const createButton = document.getElementById("add-workout");
const editButton = document.getElementById("edit-workout");
const workoutsList = document.getElementById("list");

const formContainer = document.querySelector("div#form");
const workoutInput = formContainer.querySelector("input#workout");
const locationInput = formContainer.querySelector("input#location");
const dateInput = formContainer.querySelector("input#date");

loadButton.addEventListener("click", reloadWorkouts);

createButton.addEventListener("click", async () => {
    const body = createWorkoutInstance();
    if (!body) return;

    await fetch(url, { method: "POST", body: JSON.stringify(body) });

    clearInputs();
    await reloadWorkouts();
});

let workoutToEdit = null;
editButton.addEventListener("click", async () => {
    if (!workoutToEdit) return;

    const body = createWorkoutInstance();
    if (!body) return;

    await fetch(`${url}/${workoutToEdit._id}`, { method: "PUT", body: JSON.stringify(body) });

    clearInputs();
    createButton.disabled = false;
    editButton.disabled = true;
    workoutToEdit = null;
    await reloadWorkouts();
});

function createWorkoutInstance() {
    if (!workoutInput.value || !locationInput.value || !dateInput.value) return null;
    return { workout: workoutInput.value, location: locationInput.value, date: dateInput.value };
}

function clearInputs() {
    workoutInput.value = "";
    locationInput.value = "";
    dateInput.value = "";
}

async function reloadWorkouts() {
    const getWorkoutsResponse = await fetch(url);
    const workouts = await getWorkoutsResponse.json();

    workoutsList.replaceChildren(...Object.values(workouts).map(createWorkoutElement));
}

function createWorkoutElement(workout) {
    const nameHeader = document.createElement("h2");
    nameHeader.textContent = workout.workout;

    const dateHeader = document.createElement("h3");
    dateHeader.textContent = workout.date;

    const locationHeader = document.createElement("h3");
    locationHeader.id = "location";
    locationHeader.textContent = workout.location;

    const changeButton = document.createElement("button");
    changeButton.className = "change-btn";
    changeButton.textContent = "Change";

    const deleteButton = document.createElement("button");
    deleteButton.className = "delete-btn";
    deleteButton.textContent = "Done";

    const buttonsContainer = document.createElement("div");
    buttonsContainer.id = "buttons-container";
    buttonsContainer.appendChild(changeButton);
    buttonsContainer.appendChild(deleteButton);

    const workoutContainer = document.createElement("div");
    workoutContainer.className = "container";
    workoutContainer.appendChild(nameHeader);
    workoutContainer.appendChild(dateHeader);
    workoutContainer.appendChild(locationHeader);
    workoutContainer.appendChild(buttonsContainer);

    changeButton.addEventListener("click", () => {
        workoutContainer.remove();

        workoutInput.value = workout.workout;
        locationInput.value = workout.location;
        dateInput.value = workout.date;

        createButton.disabled = true;
        editButton.disabled = false;

        workoutToEdit = workout;
    });

    deleteButton.addEventListener("click", async () => {
        await fetch(`${url}/${workout._id}`, { method: "DELETE" });
        await reloadWorkouts();
    });

    return workoutContainer;
}
