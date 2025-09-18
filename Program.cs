namespace _5KirjastoRyhmatehtava;

class Program
{
    static void Main(string[] args)
    {
        LibraryDB libraryDB = new LibraryDB();

        while (true)
        {
            Console.WriteLine(
                "Do you want to add a book (A),\n" + //1
                "Remove a book (R),\n" +             //2
                "Do you want to add a customer (O),\n" +             //7
                "Remove a customer (D),\n" +             
                "Update book information (P),\n" +   //6
                "Borrow books (B), \n" +             //4
                "Return books(T),\n" +               //5
                "Check a list of a customer's borrowed books (C) \n" + //3
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

                case "C":
                    Console.WriteLine("Write the customer's name");
                    string? name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        libraryDB.BorrowedBooks(name);
                    }
                    else
                    {
                        Console.WriteLine("Customers name cannot be empty.");
                    }
                    break;

                case "B":
                    Console.WriteLine("Enter the book.Id.");
                    int bookId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Write the customer.Id");
                    int customerId = Convert.ToInt32(Console.ReadLine());
                    if (bookId != 0 && customerId != 0)
                    {
                        libraryDB.BookBorrowing(bookId, customerId);
                    }
                    else
                    {
                        Console.WriteLine("I need all the information to borrow a book.");
                    }
                    break;

                case "T":
                    Console.WriteLine("Enter the book.Id.");
                    int returnedBookId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Write the customer.Id");
                    int returnedCustomerId = Convert.ToInt32(Console.ReadLine());
                    if (returnedBookId != 0 && returnedCustomerId != 0)
                    {
                        libraryDB.BookReturning(returnedBookId, returnedCustomerId);
                    }
                    else
                    {
                        Console.WriteLine("I need all the information to return a book.");
                    }
                    break;

                case "P":
                    Console.WriteLine("Enter the book's title.");
                    string? newTitle = Console.ReadLine();
                    Console.WriteLine("Enter the book's author.");
                    string? newAuthor = Console.ReadLine();
                    Console.WriteLine("Enter the book's category.");
                    string? newCategory = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newTitle) && !string.IsNullOrWhiteSpace(newAuthor) && !string.IsNullOrWhiteSpace(newCategory))
                    {
                        libraryDB.UpdateBook(newTitle, newAuthor, newCategory);
                    }
                    else
                    {
                        Console.WriteLine("I need all the information to update a book.");
                    }
                    break;

                case "O":
                    Console.WriteLine("Enter the customer's name.");
                    string? customersName = Console.ReadLine();
                    Console.WriteLine("Add the customer's phone number");
                    string? customersPhoneNumber = Console.ReadLine();
                    if (!string.IsNullOrEmpty(customersName) && !string.IsNullOrEmpty(customersPhoneNumber))
                    {
                        libraryDB.AddCustomer(customersName, customersPhoneNumber);
                    }
                    else
                    {
                        Console.WriteLine("I need all the information to add a customer.");
                    }
                    break;

                case "X":
                    return;

                default:
                    Console.WriteLine("Wrong choice. Choose: L, D, P, O, T, X");
                    break;
            }
        }
    }
}
