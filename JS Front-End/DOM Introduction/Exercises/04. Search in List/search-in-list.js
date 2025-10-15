function solve() {
    const searchText = document.getElementById("searchText").value;

    const towns = Array.from(document.querySelectorAll("ul#towns > li"));
    let matches = 0;
    for (const town of towns) {
        if (searchText !== "" && town.textContent.includes(searchText)) {
            town.style.fontWeight = "bold";
            town.style.textDecoration = "underline";
            matches++;
        } else {
            town.style.fontWeight = "normal";
            town.style.textDecoration = "none";
        }
    }

    const resultContainer = document.getElementById("result");
    resultContainer.textContent = `${matches} matches found`;
}
