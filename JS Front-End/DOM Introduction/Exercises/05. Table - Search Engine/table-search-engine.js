function solve() {
    const searchField = document.getElementById("searchField");
    const query = searchField.value.toLowerCase();

    const rows = Array.from(document.querySelectorAll("table.container > tbody > tr"));
    for (const row of rows) {
        row.classList.remove("select");
        if (query !== "" && row.textContent.toLowerCase().includes(query)) row.classList.add("select");
    }

    searchField.value = "";
}
