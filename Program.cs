using System;
using System.Collections.Generic;

// 1. Abstract class Book
public abstract class Book
{
    public string Code { get; set; }
    public string Title { get; set; }
    public string Publisher { get; set; }
    public int Year { get; set; }

    // Abstract method getTitle()
    public abstract string getTitle();
}

// Class Novel inheriting from Book
public class Novel : Book
{
    public string Author { get; set; }

    // Implementation of abstract method getTitle()
    public override string getTitle()
    {
        return Title;
    }
}

// Class Magazine inheriting from Book
public class Magazine : Book
{
    // Implementation of abstract method getTitle()
    public override string getTitle()
    {
        return Title;
    }
}

// 2. Interface inventoryService
public interface IInventoryService
{
    void AddBook(Book book);
    Book SearchBook(string title);
    List<Book> GetAllBooks();
}

// 3. Implementation of interface IInventoryService
public class InventoryServiceImpl : IInventoryService
{
    private List<Book> books;

    public InventoryServiceImpl()
    {
        books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public Book SearchBook(string title)
    {
        return books.Find(book => book.getTitle().Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public List<Book> GetAllBooks()
    {
        return books;
    }
}

class Program
{
    static void Main(string[] args)
    {
        IInventoryService inventoryService = new InventoryServiceImpl();

        // Example usage:
        Novel novel = new Novel
        {
            Code = "N001",
            Title = "Fundamental .NETCore",
            Publisher = "MNCGroub",
            Year = 1999,
            Author = "Dicky Setiawan"
        };

        Magazine Magazine = new Magazine
        {
            Code = "M001",
            Title = "Learning Golang",
            Publisher = "Gramedia",
            Year = 2021
        };

        inventoryService.AddBook(novel);
        inventoryService.AddBook(Magazine);

        // Display menu
        Console.WriteLine("Enigpus Library Inventory");
        Console.WriteLine("1. Add Book");
        Console.WriteLine("2. Search Book");
        Console.WriteLine("3. Show All Books");
        Console.WriteLine("4. Exit");

        bool exit = false;
        while (!exit)
        {
            Console.Write("Choose an option: ");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    // Add Book
                    Console.WriteLine("Enter book details:");
                    Console.Write("Code: ");
                    string code = Console.ReadLine();
                    Console.Write("Title: ");
                    string title = Console.ReadLine();
                    Console.Write("Publisher: ");
                    string publisher = Console.ReadLine();
                    Console.Write("Year: ");
                    int year = int.Parse(Console.ReadLine());
                    Console.Write("Author (for novel only, press enter for Magazine): ");
                    string author = Console.ReadLine();

                    if (!string.IsNullOrEmpty(author))
                    {
                        // Add Novel
                        Novel newNovel = new Novel
                        {
                            Code = code,
                            Title = title,
                            Publisher = publisher,
                            Year = year,
                            Author = author
                        };
                        inventoryService.AddBook(newNovel);
                    }
                    else
                    {
                        // Add Magazine
                        Magazine newMagazine = new Magazine
                        {
                            Code = code,
                            Title = title,
                            Publisher = publisher,
                            Year = year
                        };
                        inventoryService.AddBook(newMagazine);
                    }
                    break;

                case 2:
                    // Search Book
                    Console.Write("Enter book title to search: ");
                    string searchTitle = Console.ReadLine();
                    Book foundBook = inventoryService.SearchBook(searchTitle);
                    if (foundBook != null)
                    {
                        Console.WriteLine("Book Found:");
                        Console.WriteLine($"Code: {foundBook.Code}");
                        Console.WriteLine($"Title: {foundBook.Title}");
                        Console.WriteLine($"Publisher: {foundBook.Publisher}");
                        Console.WriteLine($"Year: {foundBook.Year}");
                        if (foundBook is Novel)
                        {
                            Novel foundNovel = (Novel)foundBook;
                            Console.WriteLine($"Author: {foundNovel.Author}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Book not found.");
                    }
                    break;

                case 3:
                    // Show All Books
                    List<Book> allBooks = inventoryService.GetAllBooks();
                    if (allBooks.Count > 0)
                    {
                        Console.WriteLine("All Books:");
                        foreach (Book book in allBooks)
                        {
                            Console.WriteLine($"Code: {book.Code}");
                            Console.WriteLine($"Title: {book.Title}");
                            Console.WriteLine($"Publisher: {book.Publisher}");
                            Console.WriteLine($"Year: {book.Year}");
                            if (book is Novel)
                            {
                                Novel novelBook = (Novel)book;
                                Console.WriteLine($"Author: {novelBook.Author}");
                            }
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No books available.");
                    }
                    break;

                case 4:
                    // Exit
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }
        }
    }
}
