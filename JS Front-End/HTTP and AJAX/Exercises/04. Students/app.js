const resultTableBody = document.querySelector("table#results > tbody");
const inputForm = document.querySelector("form#form");

inputForm.addEventListener("submit", async (e) => {
    e.preventDefault();
    const formData = new FormData(inputForm);
    const body = Object.fromEntries(formData.entries());

    await fetch("http://localhost:3030/jsonstore/collections/students", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(body),
    });

    inputForm.reset();
    await loadStudents();
});

async function loadStudents() {
    const getStudentsRequest = await fetch("http://localhost:3030/jsonstore/collections/students");
    const allStudents = await getStudentsRequest.json();

    resultTableBody.innerHTML = "";
    for (const student of Object.values(allStudents)) {
        const tr = document.createElement("tr");

        const firstNameCell = document.createElement("td");
        firstNameCell.textContent = student.firstName;
        tr.appendChild(firstNameCell);

        const lastNameCell = document.createElement("td");
        lastNameCell.textContent = student.lastName;
        tr.appendChild(lastNameCell);

        const facultyNumberCell = document.createElement("td");
        facultyNumberCell.textContent = student.facultyNumber;
        tr.appendChild(facultyNumberCell);

        const gradeCell = document.createElement("td");
        gradeCell.textContent = student.grade;
        tr.appendChild(gradeCell);

        resultTableBody.appendChild(tr);
    }
}

loadStudents();
