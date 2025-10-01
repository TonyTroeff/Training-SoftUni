function solve(text = "") {
    const words = [];

    let index = 0;
    while (index < text.length) {
        let start = index;
        while (index + 1 < text.length && text[index + 1] !== text[index + 1].toUpperCase()) index++;

        words.push(text.substring(start, index + 1));
        index++;
    }

    console.log(words.join(", "));
}

solve("SplitMeIfYouCanHaHaYouCantOrYouCan");
solve("HoldTheDoor");
solve("ThisIsSoAnnoyingToDo");
