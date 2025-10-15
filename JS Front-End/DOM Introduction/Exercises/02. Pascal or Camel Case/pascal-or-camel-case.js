function solve() {
    function toCamelCase(text) {
        const words = text.split(" ");
        return words.map((w, i) => (i === 0 ? w.toLowerCase() : capitalize(w))).join("");
    }

    function toPascalCase(text) {
        const words = text.split(" ");
        return words.map(capitalize).join("");
    }

    function capitalize(word) {
        return word.charAt(0).toUpperCase() + word.slice(1).toLowerCase();
    }

    const text = document.getElementById("text").value;
    const convention = document.getElementById("naming-convention").value;
    const resultContainer = document.getElementById("result");

    let result = "";
    if (convention === "Camel Case") result = toCamelCase(text);
    else if (convention === "Pascal Case") result = toPascalCase(text);
    else result = "Error!";

    resultContainer.textContent = result;
}
