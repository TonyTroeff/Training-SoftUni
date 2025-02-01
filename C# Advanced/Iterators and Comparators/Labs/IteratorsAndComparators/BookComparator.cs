namespace IteratorsAndComparators;

public class BookComparator : IComparer<Book>
{
    public int Compare(Book? x, Book? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (x is null) return 1;
        if (y is null) return -1;

        int titleComparison = x.Title.CompareTo(y.Title);
        if (titleComparison != 0) return titleComparison;

        return -1 * x.Year.CompareTo(y.Year);
    }
}
