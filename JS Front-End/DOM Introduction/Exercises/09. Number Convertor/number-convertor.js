function solve() {
    const numberInput = document.getElementById("input");
    const number = Number(numberInput.value);

    const radixSelect = document.getElementById("selectMenuTo");
    const radix = radixSelect.value;

    let result = "";
    if (radix === "binary") result = number.toString(2);
    else if (radix === "hexadecimal") result = number.toString(16).toUpperCase();

    const output = document.getElementById("result");
    output.value = result;
}
