document.addEventListener("DOMContentLoaded", solve);

function solve() {
    const inputForm = document.querySelector("form#input");
    const inputTextarea = inputForm.querySelector("textarea");

    const shopForm = document.querySelector("form#shop");
    const shopTableBody = shopForm.querySelector("table > tbody");
    const outputTextarea = shopForm.querySelector("textarea");

    inputForm.addEventListener("submit", (e) => {
        e.preventDefault();

        const furnitureList = JSON.parse(inputTextarea.value);

        for (const furniture of furnitureList) {
            const row = document.createElement("tr");

            const imgCell = document.createElement("td");
            const img = document.createElement("img");
            img.src = furniture.img;
            imgCell.appendChild(img);
            row.appendChild(imgCell);

            const nameCell = document.createElement("td");
            const nameParagraph = document.createElement("p");
            nameParagraph.textContent = furniture.name;
            nameCell.appendChild(nameParagraph);
            row.appendChild(nameCell);

            const priceCell = document.createElement("td");
            const priceParagraph = document.createElement("p");
            priceParagraph.textContent = furniture.price;
            priceCell.appendChild(priceParagraph);
            row.appendChild(priceCell);

            const decorationFactorCell = document.createElement("td");
            const decorationFactorParagraph = document.createElement("p");
            decorationFactorParagraph.textContent = furniture.decFactor;
            decorationFactorCell.appendChild(decorationFactorParagraph);
            row.appendChild(decorationFactorCell);

            const checkBoxCell = document.createElement("td");
            const checkBox = document.createElement("input");
            checkBox.type = "checkbox";
            checkBoxCell.appendChild(checkBox);
            row.appendChild(checkBoxCell);

            shopTableBody.appendChild(row);
        }
    });

    shopForm.addEventListener("submit", (e) => {
        e.preventDefault();

        const boughtFurniture = [];
        let totalPrice = 0;
        let totalDecorationFactor = 0;
        const rows = shopTableBody.querySelectorAll("tr:has(input:checked)");

        for (const row of rows) {
            const name = row.children[1].textContent;
            const price = parseFloat(row.children[2].textContent);
            const decorationFactor = parseFloat(row.children[3].textContent);

            boughtFurniture.push(name);
            totalPrice += price;
            totalDecorationFactor += decorationFactor;
        }

        const averageDecorationFactor = totalDecorationFactor / boughtFurniture.length;

        outputTextarea.value = "";
        outputTextarea.value += `Bought furniture: ${boughtFurniture.join(", ")}\n`;
        outputTextarea.value += `Total price: ${totalPrice}\n`;
        outputTextarea.value += `Average decoration factor: ${averageDecorationFactor}`;
    });
}
