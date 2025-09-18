namespace _5KirjastoRyhmatehtava;

using System;
using Microsoft.Data.Sqlite;

public class LibraryDB
{
    private static string _connectionString = "Data Source=Library.db";

    public LibraryDB()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            // Create tables commands here
            var CreateBooksTable = connection.CreateCommand();
            CreateBooksTable.CommandText = @"
                CREATE TABLE IF NOT EXISTS Books (
                    id INTEGER PRIMARY KEY,
                    title TEXT,
                    author TEXT,
                    category TEXT)";
            CreateBooksTable.ExecuteNonQuery();
            var CreateCustomerTable = connection.CreateCommand();
            CreateCustomerTable.CommandText = @"
                CREATE TABLE IF NOT EXISTS Customers (
                    id INTEGER PRIMARY KEY,
                    name TEXT,
                    phoneNumber TEXT)";
            CreateCustomerTable.ExecuteNonQuery();
            var CreateLoansTable = connection.CreateCommand();
            CreateLoansTable.CommandText = @"
                CREATE TABLE IF NOT EXISTS Loans (
                    id INTEGER PRIMARY KEY,
                    bookId INTEGER,
                    customerId INTEGER,
                    status TEXT,
                    FOREIGN KEY(bookId) REFERENCES Books(id),
                    FOREIGN KEY(customerId) REFERENCES Customers(id))";
            CreateLoansTable.ExecuteNonQuery();
        }
    }
    public void RemovingABook(string bookName)
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
        DELETE
        FROM books
        WHERE books.title = $name";
        command.Parameters.AddWithValue("$name", bookName);
        command.ExecuteNonQuery();

        connection.Close();
    }
    public void AddingABook(string title, string author, string category)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var commandForCheck = connection.CreateCommand();
            commandForCheck.CommandText = "SELECT id FROM Books WHERE title=@Title";
            commandForCheck.Parameters.AddWithValue("Title", title);

            object? id = commandForCheck.ExecuteScalar();
            if (id == null)
            {
                var commandForInsert = connection.CreateCommand();
                commandForInsert.CommandText = "INSERT INTO Books (title, author, category) VALUES (@Title, @Author, @Category)";
                commandForInsert.Parameters.AddWithValue("Title", title);
                commandForInsert.Parameters.AddWithValue("Author", author);
                commandForInsert.Parameters.AddWithValue("Category", category);
                commandForInsert.ExecuteNonQuery();
            }
            else
            {
                Console.WriteLine("The book is already in the database.");
                return;
            }
        }
    }

    public void BorrowedBooks(string name)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var commandForCheck = connection.CreateCommand();
            commandForCheck.CommandText =
            @"SELECT Books.title 
            FROM Customers 
            LEFT JOIN Loans 
            ON Customers.id = Loans.customerId
            LEFT JOIN Books
            ON Loans.booksId = Books.id
            WHERE Customers.name = $Name
            AND Loans.status = 'borrowed'";
            commandForCheck.Parameters.AddWithValue("$Name", name);

            using (var reader = commandForCheck.ExecuteReader())
            {
                bool hasBooks = false;

                while (reader.Read())
                {
                    if (!hasBooks)
                    {
                        Console.WriteLine("Borrowed book(s):");
                        hasBooks = true;
                    }

                    string title = reader.GetString(0);
                    Console.WriteLine(title);
                }

                if (!hasBooks)
                {
                    Console.WriteLine("No borrowed books found.");
                }
            }
        }
    }

    public void BookBorrowing(int bookId, int customerId)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                var commandForCheckBook = connection.CreateCommand();
                commandForCheckBook.CommandText = "SELECT id FROM Books WHERE id=$BookId";
                commandForCheckBook.Parameters.AddWithValue("$BookId", bookId);
                object? bookExists = commandForCheckBook.ExecuteScalar();

                var commandForCheckCustomer = connection.CreateCommand();
                commandForCheckCustomer.CommandText = "SELECT id FROM Customers WHERE id=$CustomerId";
                commandForCheckCustomer.Parameters.AddWithValue("$CustomerId", customerId);
                object? customerExists = commandForCheckCustomer.ExecuteScalar();

                var commandForCheckLoan = connection.CreateCommand();
                commandForCheckLoan.CommandText = "SELECT id FROM Loans WHERE bookId=$BookId AND status='borrowed'";
                commandForCheckLoan.Parameters.AddWithValue("$BookId", bookId);
                object? loanExists = commandForCheckLoan.ExecuteScalar();

                if (bookExists == null)
                {
                    Console.WriteLine("The book does not exist.");
                    return;
                }
                if (customerExists == null)
                {
                    Console.WriteLine("The customer does not exist.");
                    return;
                }
                if (loanExists != null)
                {
                    Console.WriteLine("The book is already borrowed.");
                    return;
                }

                var commandForInsertLoan = connection.CreateCommand();
                commandForInsertLoan.CommandText = "INSERT INTO Loans (bookId, customerId, status) VALUES ($BookId, $CustomerId, 'borrowed')";
                commandForInsertLoan.Parameters.AddWithValue("$BookId", bookId);
                commandForInsertLoan.Parameters.AddWithValue("$CustomerId", customerId);
                commandForInsertLoan.ExecuteNonQuery();

                transaction.Commit();
            }
        }
    }
    public void BookReturning(int bookId, int customerId)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                var commandForCheckLoan = connection.CreateCommand();
                commandForCheckLoan.CommandText = "SELECT id FROM Loans WHERE bookId=$BookId AND customerId=$CustomerId AND status='borrowed'";
                commandForCheckLoan.Parameters.AddWithValue("$BookId", bookId);
                commandForCheckLoan.Parameters.AddWithValue("$CustomerId", customerId);
                object? loanExists = commandForCheckLoan.ExecuteScalar();

                if (loanExists == null)
                {
                    Console.WriteLine("No active loan found for this book and customer.");
                    return;
                }

                int loanId = Convert.ToInt32(loanExists);

                var commandForUpdateLoan = connection.CreateCommand();
                commandForUpdateLoan.CommandText = "UPDATE Loans SET status='returned' WHERE id=$LoanId";
                commandForUpdateLoan.Parameters.AddWithValue("$LoanId", loanId);
                commandForUpdateLoan.ExecuteNonQuery();

                transaction.Commit();
            }
        }
    }

    public void UpdateBook(string title, string newAuthor, string newCategory)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var commandForUpdate = connection.CreateCommand();
            commandForUpdate.CommandText = @"
            UPDATE Books 
            SET author = @Author, category = @Category 
            WHERE title = @Title";

            commandForUpdate.Parameters.AddWithValue("@Author", newAuthor);
            commandForUpdate.Parameters.AddWithValue("@Category", newCategory);
            commandForUpdate.Parameters.AddWithValue("@Title", title);

            int rowsAffected = commandForUpdate.ExecuteNonQuery();

        }
    }
    public void SearchLoanByBook(string title)
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            var commandForSearch = connection.CreateCommand();
            commandForSearch.CommandText = @"
            SELECT Customers.name 
            FROM Books 
            JOIN Loans ON Books.id = Loans.bookId 
            JOIN Customers ON Loans.customerId = Customers.id 
            WHERE Books.title = $Title AND Loans.status = 'borrowed'";

            commandForSearch.Parameters.AddWithValue("$Title", title);

            using (var reader = commandForSearch.ExecuteReader())
            {
                if (reader.Read())
                {
                    string customerName = reader.GetString(0);

                    Console.WriteLine($"Customer Name: {customerName}");
                }
                else
                {
                    Console.WriteLine("Book is available for borrowing.");
                }
            }
        }
    }


    public bool Connect()       //Luotu tarjoamaan testitiedostoille pääsy tietokantaan.
    {
        try
        {
            string connectionString = "Data Source=Library.db;";
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
            }


            Console.WriteLine("Yhteys onnistui.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Yhteysvirhe: " + ex.Message);
            return false;
        }
    }

}
