using System.Collections;

namespace IteratorsAndComparators;

public class Library : IEnumerable<Book>
{
    public List<Book> Books { get; }

    public Library(params Book[] books)
    {
        this.Books = new List<Book>(books);
        this.Books.Sort(new BookComparator());
    }

    public IEnumerator<Book> GetEnumerator()
        => new LibraryIterator(this.Books);

    IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();

    private class LibraryIterator : IEnumerator<Book>
    {
        private readonly List<Book> _books;
        private int _index;

        public LibraryIterator(List<Book> books)
        {
            this._books = books;
            this.Reset();
        }

        public Book Current => this._books[this._index];
        object IEnumerator.Current => this.Current;

        public bool MoveNext()
        {
            if (this._index == this._books.Count) return false;
            return ++this._index < this._books.Count;
        }

        public void Reset()
            => this._index = -1;

        public void Dispose()
        {
        }
    }
}
