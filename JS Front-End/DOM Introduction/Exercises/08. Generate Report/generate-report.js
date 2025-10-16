function solve() {
    const checkboxes = Array.from(document.querySelectorAll('table > thead > tr > th > input[type="checkbox"]'));

    const columns = [];
    checkboxes.forEach((checkbox, index) => {
        if (checkbox.checked) columns.push({ name: checkbox.getAttribute("name"), index });
    });

    const rows = Array.from(document.querySelectorAll("table > tbody > tr"));
    const report = [];

    for (const row of rows) {
        const cells = Array.from(row.querySelectorAll("td"));

        const obj = {};
        for (const col of columns) {
            obj[col.name] = cells[col.index].textContent;
        }

        report.push(obj);
    }

    const output = document.getElementById("output");
    output.value = JSON.stringify(report);
}
