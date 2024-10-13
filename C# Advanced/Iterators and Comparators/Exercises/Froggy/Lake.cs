using System.Collections;

namespace Froggy;

public class Lake : IEnumerable<int>
{
    private readonly int[] _stones;

    public Lake(int[] stones)
    {
        this._stones = stones ?? throw new ArgumentNullException(nameof(stones));
    }

    public IEnumerator<int> GetEnumerator()
    {
        for (int i = 0; i < this._stones.Length; i += 2)
            yield return this._stones[i];

        // int reverseBegin;
        // if (this._stones.Length % 2 == 0) reverseBegin = this._stones.Length - 1;
        // else reverseBegin = this._stones.Length - 2;

        int reverseBegin = this._stones.Length - (this._stones.Length % 2) - 1;

        for (int i = reverseBegin; i > 0; i -= 2)
            yield return this._stones[i];
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
