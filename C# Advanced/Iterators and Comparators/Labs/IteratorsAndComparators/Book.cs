namespace IteratorsAndComparators;

public class Book : IComparable<Book>
{
    public string Title { get; }
    public int Year { get; }
    public List<string> Authors { get; }

    public Book(string title, int year, params string[] authors)
    {
        this.Title = title;
        this.Year = year;
        this.Authors = new List<string>(authors);
    }

    public int CompareTo(Book? other)
    {
        if (other is null) return -1;

        int yearComparison = this.Year.CompareTo(other.Year);
        if (yearComparison != 0) return yearComparison;

        return this.Title.CompareTo(other.Title);
    }

    public override string ToString() => $"{this.Title} - {this.Year}";
}
