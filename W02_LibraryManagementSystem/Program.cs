using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace W02_LibraryManagementSystem;

public abstract class MediaItem
{
    private int _condition;

    public int Condition
    {
        get => _condition;
        set => _condition = Math.Clamp(value, 0, 100);
    }

    public string Title { get; init; } = string.Empty;
    public string ISBN { get; init; } = string.Empty;

    public abstract decimal CalculateLateFee(int daysLate);
}

public interface ILendable
{
    bool IsCheckedOut { get; }
    DateTime? DueDate { get; }
    void CheckOut(int daysToLend);
    void Return();
}

public interface IReservable
{
    bool IsReserved { get; }
    string? ReservedBy { get; }
    bool TryReserve(string memberName);
    void CancelReservation();
}

public class Book : MediaItem, ILendable, IReservable
{
    public string Author { get; set; } = string.Empty;
    public int PageCount { get; set; }

    public bool IsCheckedOut { get; private set; }
    public DateTime? DueDate { get; private set; }

    public bool IsReserved { get; private set; }
    public string? ReservedBy { get; private set; }

    public override decimal CalculateLateFee(int daysLate) => daysLate * 0.25m;

    public void CheckOut(int daysToLend)
    {
        IsCheckedOut = true;
        DueDate = DateTime.Now.AddDays(daysToLend);
    }

    public void Return()
    {
        IsCheckedOut = false;
        DueDate = null;
    }

    public bool TryReserve(string memberName)
    {
        if (IsReserved) return false;
        IsReserved = true;
        ReservedBy = memberName;
        return true;
    }

    public void CancelReservation()
    {
        IsReserved = false;
        ReservedBy = null;
    }

    public override string ToString()
        => $"Book: {Title} (ISBN: {ISBN}), Author: {Author}, Pages: {PageCount}, Condition: {Condition}%";
}

public class Magazine : MediaItem
{
    public int IssueNumber { get; set; }
    public string Publisher { get; set; } = string.Empty;

    public override decimal CalculateLateFee(int daysLate) => daysLate * 1.00m;

    public override string ToString() =>
        $"Magazine: {Title} (ISBN: {ISBN}), Issue: {IssueNumber}, Publisher: {Publisher}, Condition: {Condition}%";
}

public class Catalog<T> : IEnumerable<T> where T : MediaItem
{
    private readonly List<T> _items = [];
    private readonly int _capacity;

    public Catalog(int capacity)
    {
        _capacity = capacity;
    }

    public bool AddItem(T item)
    {
        if (_items.Any(i => i.ISBN == item.ISBN)) return false;
        if (_items.Count >= _capacity) return false;
        _items.Add(item);
        return true;
    }

    public bool RemoveItem(string isbn)
    {
        var item = _items.FirstOrDefault(i => i.ISBN == isbn);
        return item != null && _items.Remove(item);
    }

    public T? FindByISBN(string isbn) => _items.FirstOrDefault(i => i.ISBN == isbn);

    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class LibrarySystem : IDisposable
{
    private readonly StreamWriter _logger;
    private bool _disposed;

    public LibrarySystem(string logPath)
    {
        _logger = new StreamWriter(logPath, append: true) { AutoFlush = true };
        Log("Library system initialized");
    }

    public void Log(string message)
    {
        _logger.WriteLine($"[{DateTime.Now}] {message}");
    }

    public void Dispose()
    {
        if (_disposed) return;
        Log("Library system shut down");
        _logger.Dispose();
        _disposed = true;
    }
}

class Program
{
    static void Main(string[] args)
    {
        using var library = new LibrarySystem("library.log");
        var books = new Catalog<Book>(2);
        library.Log("Created catalog with capacity: 2");

        var book1 = new Book
        {
            Title = "The Library Book",
            ISBN = "123-456",
            Author = "Susan Orlean",
            PageCount = 300,
            Condition = 150
        };

        var magazine1 = new Magazine
        {
            Title = "Library Weekly",
            ISBN = "789-123",
            IssueNumber = 42,
            Publisher = "Library Press"
        };

        Console.WriteLine("\nTesting catalog management:");
        if (books.AddItem(book1))
        {
            Console.WriteLine($"Added: {book1.Title}");
            library.Log($"Added book: {book1.Title} (ISBN: {book1.ISBN})");
        }
        else
        {
            Console.WriteLine($"Failed to add duplicate ISBN: {book1.ISBN}");
            library.Log($"Failed to add duplicate book: {book1.Title} (ISBN: {book1.ISBN})");
        }

        if (!books.AddItem(book1))
        {
            Console.WriteLine($"Failed to add duplicate ISBN: {book1.ISBN}");
            library.Log($"Failed to add duplicate book: {book1.Title} (ISBN: {book1.ISBN})");
        }

        Console.WriteLine("\nTesting lending system:");
        if (book1 is ILendable lendable)
        {
            lendable.CheckOut(14);
            Console.WriteLine($"Book is checked out: {lendable.IsCheckedOut}");
            Console.WriteLine($"Due date: {lendable.DueDate:d}");
            library.Log($"Book checked out: {book1.Title} (Due: {lendable.DueDate:d})");

            Console.WriteLine($"Late fee for 5 days: ${book1.CalculateLateFee(5):F2}");

            lendable.Return();
            Console.WriteLine($"Book is checked out: {lendable.IsCheckedOut}");
            library.Log($"Book returned: {book1.Title}");
        }

        Console.WriteLine("\nTesting reservation system:");
        if (book1 is IReservable reservable)
        {
            reservable.TryReserve("John Doe");
            Console.WriteLine($"Book is reserved: {reservable.IsReserved}");
            Console.WriteLine($"Reserved by: {reservable.ReservedBy}");
            library.Log($"Book reserved: {book1.Title} by {reservable.ReservedBy}");

            reservable.CancelReservation();
            Console.WriteLine($"Book is reserved: {reservable.IsReserved}");
            library.Log($"Reservation cancelled for: {book1.Title}");
        }
    }
}
