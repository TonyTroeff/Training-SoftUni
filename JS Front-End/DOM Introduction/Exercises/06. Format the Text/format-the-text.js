function solve() {
    const textArea = document.getElementById("input");
    const outputContainer = document.getElementById("output");

    const text = textArea.value
        .split(".")
        .map((s) => s.trim())
        .filter((s) => s.length > 0);
    let result = "<p>";

    for (let i = 0; i < text.length; i++) {
        if (i !== 0 && i % 3 === 0) result += "</p><p>";
        result += text[i] + ".";
    }

    result += "</p>";

    outputContainer.innerHTML = result;
}
