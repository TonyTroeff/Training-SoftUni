document.addEventListener("DOMContentLoaded", solve);

function solve() {
    const form = document.querySelector("form#solutionCheck");
    const table = form.querySelector("table");
    const field = table.querySelector("tbody");

    const sizeSelect = document.querySelector("select#size");
    sizeSelect.addEventListener("change", (e) => {
        const size = e.target.value;
        const rowTemplate = document.createElement("tr");
        for (let i = 0; i < size; i++) {
            const input = document.createElement("input");
            input.type = "number";
            input.step = 1;
            input.min = 1;
            input.max = size;
            input.required = true;

            const cell = document.createElement("td");
            cell.appendChild(input);

            rowTemplate.appendChild(cell);
        }

        field.innerHTML = "";
        for (let i = 0; i < size; i++) {
            field.appendChild(rowTemplate.cloneNode(true));
        }
    });

    const outputParagraph = document.querySelector("p#check");
    form.addEventListener("submit", (e) => {
        e.preventDefault();

        const size = sizeSelect.value;
        const rowCache = Array.from({ length: size }, () => ({}));
        const colCache = Array.from({ length: size }, () => ({}));

        let isValid = true;
        const rows = field.querySelectorAll("tr");
        for (let i = 0; isValid && i < rows.length; i++) {
            const cells = rows[i].querySelectorAll("td");

            for (let j = 0; isValid && j < cells.length; j++) {
                const input = cells[j].querySelector("input");
                const value = input.value;

                if (value in rowCache[i] || value in colCache[j]) {
                    isValid = false;
                } else {
                    rowCache[i][value] = true;
                    colCache[j][value] = true;
                }
            }
        }

        if (isValid) {
            outputParagraph.textContent = "Success!";
            table.style.border = "2px solid green";
        } else {
            outputParagraph.textContent = "Keep trying...";
            table.style.border = "2px solid red";
        }
    });

    form.addEventListener("reset", () => {
        outputParagraph.textContent = "";
        table.style.border = "none";
    });
}
