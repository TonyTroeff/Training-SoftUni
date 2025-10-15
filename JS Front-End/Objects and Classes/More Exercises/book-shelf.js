function solve(input) {
    const patterns = [/(?<id>.+) -> (?<genre>.+)/, /(?<title>.+): (?<author>.+), (?<genre>.+)/];

    const shelfById = {};
    const shelfByGenre = {};
    for (const element of input) {
        if (patterns[0].test(element)) {
            const match = element.match(patterns[0]);
            const id = match.groups.id;
            const genre = match.groups.genre;

            if (!shelfById.hasOwnProperty(id)) {
                shelfById[id] = { genre, books: [] };
                shelfByGenre[genre] = id;
            }
        } else if (patterns[1].test(element)) {
            const match = element.match(patterns[1]);
            const title = match.groups.title;
            const author = match.groups.author;
            const genre = match.groups.genre;

            if (shelfByGenre.hasOwnProperty(genre)) {
                const shelfId = shelfByGenre[genre];
                shelfById[shelfId].books.push({ title, author });
            }
        }
    }

    for (const [id, shelf] of Object.entries(shelfById).sort((a, b) => b[1].books.length - a[1].books.length)) {
        console.log(`${id} ${shelf.genre}: ${shelf.books.length}`);
        for (const book of shelf.books.sort((a, b) => a.title.localeCompare(b.title))) {
            console.log(`--> ${book.title}: ${book.author}`);
        }
    }
}

solve(["1 -> history", "1 -> action", "Death in Time: Criss Bell, mystery", "2 -> mystery", "3 -> sci-fi", "Child of Silver: Bruce Rich, mystery", "Hurting Secrets: Dustin Bolt, action", "Future of Dawn: Aiden Rose, sci-fi", "Lions and Rats: Gabe Roads, history", "2 -> romance", "Effect of the Void: Shay B, romance", "Losing Dreams: Gail Starr, sci-fi", "Name of Earth: Jo Bell, sci-fi", "Pilots of Stone: Brook Jay, history"]);
solve(["1 -> mystery", "2 -> sci-fi", "Child of Silver: Bruce Rich, mystery", "Lions and Rats: Gabe Roads, history", "Effect of the Void: Shay B, romance", "Losing Dreams: Gail Starr, sci-fi", "Name of Earth: Jo Bell, sci-fi"]);
