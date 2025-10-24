document.addEventListener("DOMContentLoaded", solve);

function solve() {
    const form = document.getElementById("task-input");
    const input = form.querySelector("input[type='text']");

    const outputContainer = document.getElementById("content");

    form.addEventListener("submit", function (event) {
        event.preventDefault();
        const sections = input.value.split(", ");

        outputContainer.innerHTML = "";
        sections.forEach((section) => {
            const paragraph = document.createElement("p");
            paragraph.hidden = true;
            paragraph.textContent = section;

            const div = document.createElement("div");
            div.appendChild(paragraph);

            div.addEventListener("click", function () {
                paragraph.hidden = false;
            });

            outputContainer.appendChild(div);
        });
    });
}
