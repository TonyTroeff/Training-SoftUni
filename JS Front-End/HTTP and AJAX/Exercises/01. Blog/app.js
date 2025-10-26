function attachEvents() {
    const loadPostsButton = document.querySelector("button#btnLoadPosts");
    const viewPostButton = document.querySelector("button#btnViewPost");
    const postsSelect = document.querySelector("select#posts");

    const postTitleHeading = document.querySelector("h1#post-title");
    const postBodyParagraph = document.querySelector("p#post-body");
    const postCommentsList = document.querySelector("ul#post-comments");

    loadPostsButton.addEventListener("click", async () => {
        const getPostsResponse = await fetch("http://localhost:3030/jsonstore/blog/posts");
        const posts = await getPostsResponse.json();

        postsSelect.innerHTML = "";

        for (const postId in posts) {
            const option = document.createElement("option");
            option.value = postId;
            option.textContent = posts[postId].title;

            postsSelect.appendChild(option);
        }
    });

    viewPostButton.addEventListener("click", async () => {
        const selectedPostId = postsSelect.value;
        if (!selectedPostId) return;

        const getPostsResponse = await fetch("http://localhost:3030/jsonstore/blog/posts");
        const allPosts = await getPostsResponse.json();
        const post = allPosts[selectedPostId];

        const getCommentsResponse = await fetch("http://localhost:3030/jsonstore/blog/comments");
        const allComments = await getCommentsResponse.json();
        const comments = Object.values(allComments).filter((c) => c.postId === selectedPostId);

        postTitleHeading.textContent = post.title;
        postBodyParagraph.textContent = post.body;

        postCommentsList.innerHTML = "";
        for (const comment of comments) {
            const li = document.createElement("li");
            li.textContent = comment.text;
            postCommentsList.appendChild(li);
        }
    });
}

attachEvents();
