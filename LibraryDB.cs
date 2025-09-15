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
        }
    }
}

