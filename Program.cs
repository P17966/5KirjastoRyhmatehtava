namespace _5KirjastoRyhmatehtava;

class Program
{
    static void Main(string[] args)
    {
        LibraryDB libraryDB = new LibraryDB();

        while (true)
        {
            Console.WriteLine(
                "Do you want to add a book (A),\n" +
                "Remove a book (R),\n" +
                "Update book information (P),\n" +
                "Borrow and return books (B),\n" +
                "Check loan information for a book or person (C) \n" +
                "or Stop(X)?"
            );

            string? command = Console.ReadLine();

            switch (command)
            {
                case "A":
                    Console.WriteLine("Enter the book title.");
                    string? titleOfBook = Console.ReadLine();
                    Console.WriteLine("Write the name of the book's author.");
                    string? authorOfBook = Console.ReadLine();
                    Console.WriteLine("Write the genre of the book");
                    string? category = Console.ReadLine();
                    if (!string.IsNullOrEmpty(titleOfBook) && !string.IsNullOrEmpty(authorOfBook) && !string.IsNullOrEmpty(category))
                    {
                        libraryDB.AddingABook(titleOfBook, authorOfBook, category);
                    }
                    else
                    {
                        Console.WriteLine("I need all the information to add a book.");
                    }
                    break;
                case "R":
                    Console.WriteLine("Which book should be removed?");
                    string? booksTitle = Console.ReadLine();
                    if (!string.IsNullOrEmpty(booksTitle))
                    {
                        libraryDB.RemovingABook(booksTitle);
                        Console.WriteLine("The book has been removed.");
                    }
                    else
                    {
                        Console.WriteLine("Book name cannot be empty.");
                    }
                    break;

                case "X":
                    return;
            }
        }
    }
}
