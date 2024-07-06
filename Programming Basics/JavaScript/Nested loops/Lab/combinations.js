function solve(input) {
    const n = Number(input[0]);

    // Mathematical solution using arithmetic progressions. Complexity: O(1)
    /*int ans = (n + 1) * (n + 2) / 2;
    console.log(ans);*/

    let ans = 0;

    for (let i = 0; i <= n; i++) {
        let remaining = n - i;
        for (let j = 0; j <= remaining; j++) {
            // i + j + _ = n
            ans++;
        }
    }

    console.log(ans);
}
