function solve(input) {
    const patterns = [/user (?<user>.+)/, /article (?<article>.+)/, /(?<user>.+) posts on (?<article>.+): (?<title>.+), (?<content>.+)/];

    const users = {};
    const articles = {};
    const commentsByArticle = {};

    for (const element of input) {
        if (patterns[0].test(element)) {
            const match = element.match(patterns[0]);
            const user = match.groups.user;
            users[user] = [];
        } else if (patterns[1].test(element)) {
            const match = element.match(patterns[1]);
            const article = match.groups.article;
            articles[article] = [];
        } else if (patterns[2].test(element)) {
            const match = element.match(patterns[2]);
            const user = match.groups.user;
            const article = match.groups.article;
            const title = match.groups.title;
            const content = match.groups.content;

            if (users.hasOwnProperty(user) && articles.hasOwnProperty(article)) {
                if (!commentsByArticle.hasOwnProperty(article)) commentsByArticle[article] = [];
                commentsByArticle[article].push({ user, title, content });
            }
        }
    }

    for (const [article, comments] of Object.entries(commentsByArticle).sort((a, b) => b[1].length - a[1].length)) {
        console.log(`Comments on ${article}`);
        for (const comment of comments.sort((a, b) => a.user.localeCompare(b.user))) {
            console.log(`--- From user ${comment.user}: ${comment.title} - ${comment.content}`);
        }
    }
}

solve(["user aUser123", "someUser posts on someArticle: NoTitle, stupidComment", "article Books", "article Movies", "article Shopping", "user someUser", "user uSeR4", "user lastUser", "uSeR4 posts on Books: I like books, I do really like them", "uSeR4 posts on Movies: I also like movies, I really do", "someUser posts on Shopping: title, I go shopping every day", "someUser posts on Movies: Like, I also like movies very much"]);
solve(["user Mark", "Mark posts on someArticle: NoTitle, stupidComment", "article Bobby", "article Steven", "user Liam", "user Henry", "Mark posts on Bobby: Is, I do really like them", "Mark posts on Steven: title, Run", "someUser posts on Movies: Like"]);
