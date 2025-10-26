const mainSection = document.querySelector("section#main");

loadArticles();

async function loadArticles() {
    const getArticlesResponse = await fetch("http://localhost:3030/jsonstore/advanced/articles/list");
    const allArticles = await getArticlesResponse.json();

    for (const article of Object.values(allArticles)) {
        const articleDiv = document.createElement("div");
        articleDiv.classList.add("accordion");

        const articleTitleDiv = document.createElement("div");
        articleTitleDiv.classList.add("head");

        const articleTitleSpan = document.createElement("span");
        articleTitleSpan.textContent = article.title;

        const articleButton = document.createElement("button");
        articleButton.classList.add("button");
        articleButton.id = article._id;
        articleButton.textContent = "More";

        articleTitleDiv.appendChild(articleTitleSpan);
        articleTitleDiv.appendChild(articleButton);

        articleDiv.appendChild(articleTitleDiv);

        const articleContentDiv = document.createElement("div");
        articleContentDiv.classList.add("extra");

        articleDiv.appendChild(articleContentDiv);

        let isExpanded = false;
        articleButton.addEventListener("click", async () => {
            if (isExpanded) {
                articleContentDiv.innerHTML = "";
                articleContentDiv.style.display = "none";
                articleButton.textContent = "More";
            } else {
                const getArticleDetailsResponse = await fetch(`http://localhost:3030/jsonstore/advanced/articles/details/${article._id}`);
                const articleDetails = await getArticleDetailsResponse.json();

                const articleContentParagraph = document.createElement("p");
                articleContentParagraph.textContent = articleDetails.content;

                articleContentDiv.appendChild(articleContentParagraph);
                articleContentDiv.style.display = "block";
                articleButton.textContent = "Less";
            }

            isExpanded = !isExpanded;
        });

        mainSection.appendChild(articleDiv);
    }
}
