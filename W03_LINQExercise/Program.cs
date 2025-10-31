using System.Xml.Serialization;

namespace W03_LINQExercise;


public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public string Genre { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public string Description { get; set; }

    public override string ToString()
    {
        return $"{Author} - {Title} ({Year})";
    }
}

class Program
{
    static void Main2(string[] args)
    {
        var books = BooksHelper.LoadBooks();
        // Filter Books by Genre, Find all the books in the “Science Fiction” genre
        var filteredByGenre = books.Where(b => b.Genre.Equals("science fiction", StringComparison.OrdinalIgnoreCase))
            .ToList();


        // Select Book Titles, Create a list of all book titles
        var allTitles = books.Select(b => b.Title);

        // Order Books by Publication Year
        var orderedByYear = books.OrderBy(b => b.Year);

        // Group Books by Availability
        var availability = books.GroupBy(b => b.IsAvailable);

        // Find the most Expensive book
        var mostExpensive = books.OrderByDescending(b => b.Price).FirstOrDefault();

        // Count books by Genre
        var genres = books.CountBy(b => b.Genre);

        // Project Book Details (Title + Author)
    }

    static void Main(string[] args)
    {
        var books = BooksHelper.LoadBooks();
        Console.WriteLine("All loaded books:");
        books.ForEach(Console.WriteLine);
        Console.WriteLine("\n-----------------------------------");

        // 1. Filter books by genre: "Science Fiction"
        var sciFiBooks = from book in books where book.Genre == "Science Fiction" select book;
        Console.WriteLine("Science Fiction books:");
        sciFiBooks.ToList().ForEach(Console.WriteLine);
        Console.WriteLine("\n-----------------------------------");

        // 2. Select book titles
        var titles = from book in books select book.Title;
        Console.WriteLine("Book titles:");
        titles.ToList().ForEach(Console.WriteLine);
        Console.WriteLine("\n-----------------------------------");

        // 3. Order books by publication year
        var orderedByYear = from book in books orderby book.Year select book;
        Console.WriteLine("Books ordered by year:");
        orderedByYear.ToList().ForEach(Console.WriteLine);
        Console.WriteLine("\n-----------------------------------");

        // 4. Group books by availability
        var groupedByAvailability = from book in books group book by book.IsAvailable into g select g;
        foreach (var group in groupedByAvailability)
        {
            Console.WriteLine(group.Key ? "Available:" : "Not Available:");
            foreach (var book in group)
            {
                Console.WriteLine($"   {book}");
            }
        }

        Console.WriteLine("\n-----------------------------------");

        // 5. Most expensive book
        var mostExpensive = books.OrderByDescending(b => b.Price).FirstOrDefault();
        Console.WriteLine("Most expensive book:");
        Console.WriteLine(mostExpensive);
        Console.WriteLine("\n-----------------------------------");

        // 6. Count books by genre
        var genreCounts = from book in books
            group book by book.Genre
            into g
            select new { Genre = g.Key, Count = g.Count() };
        Console.WriteLine("Book count by genre:");
        foreach (var entry in genreCounts)
        {
            Console.WriteLine($"{entry.Genre}: {entry.Count}");
        }

        Console.WriteLine("\n-----------------------------------");

        // 7. Project book details (Title + Author)
        var titleAndAuthor = from book in books select new { book.Title, book.Author };
        Console.WriteLine("Title and Author:");
        foreach (var entry in titleAndAuthor)
        {
            Console.WriteLine($"{entry.Title} by {entry.Author}");
        }

        Console.WriteLine("\n-----------------------------------");
    }

    public static class BooksHelper
    {
        public static List<Book> LoadBooks(string path = "Books.xml")
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<Book>), new XmlRootAttribute("Books"));
                using var reader = new StreamReader(path);
                return serializer.Deserialize(reader) as List<Book>;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error loading books: {e.Message}");
                return [];
            }
        }
    }
}
