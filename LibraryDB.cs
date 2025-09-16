namespace _5KirjastoRyhmatehtava;


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
    public void DeletingABook(string bookName){
        var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
        DELETE
        FROM books
        WHERE books.name = $name";
        command.Parameters.AddWithValue("$name", bookName);
        command.ExecuteNonQuery();

        connection.Close();
    }
}

