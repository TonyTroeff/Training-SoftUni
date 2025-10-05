function solve(commands) {
    const movies = {};

    for (const command of commands) {
        if (command.startsWith("addMovie ")) {
            const name = command.substring(9);
            movies[name] = { name };
        } else if (command.includes(" directedBy ")) {
            const [name, director] = command.split(" directedBy ");
            if (name in movies) movies[name].director = director;
        } else if (command.includes(" onDate ")) {
            const [name, date] = command.split(" onDate ");
            if (name in movies) movies[name].date = date;
        }
    }

    for (const movie of Object.values(movies)) {
        if (movie.name && movie.director && movie.date) console.log(JSON.stringify(movie));
    }
}

solve(["addMovie Fast and Furious", "addMovie Godfather", "Inception directedBy Christopher Nolan", "Godfather directedBy Francis Ford Coppola", "Godfather onDate 29.07.2018", "Fast and Furious onDate 30.07.2018", "Batman onDate 01.08.2018", "Fast and Furious directedBy Rob Cohen"]);
solve(["addMovie The Avengers", "addMovie Superman", "The Avengers directedBy Anthony Russo", "The Avengers onDate 30.07.2010", "Captain America onDate 30.07.2010", "Captain America directedBy Joe Russo"]);
