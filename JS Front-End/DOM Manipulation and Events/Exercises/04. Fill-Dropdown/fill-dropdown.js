document.addEventListener("DOMContentLoaded", solve);

function solve() {
    const select = document.querySelector("select#menu");

    const form = document.querySelector("form");
    const textInput = form.querySelector("input#newItemText");
    const valueInput = form.querySelector("input#newItemValue");

    form.addEventListener("submit", function (event) {
        event.preventDefault();

        const text = textInput.value;
        const value = valueInput.value;

        const option = document.createElement("option");
        option.textContent = text;
        option.value = value;
        select.appendChild(option);

        textInput.value = "";
        valueInput.value = "";
    });
}
