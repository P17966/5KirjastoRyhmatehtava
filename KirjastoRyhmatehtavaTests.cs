using System;
namespace _5KirjastoRyhmatehtava;

public class KirjastoRyhmatehtavaTests
{
    public static void TestConnection()
    {
        var db = new LibraryDB();
        var result = db.Connect();
        Console.WriteLine(result ? "OK" : "FAIL");
    }

    public static void TestAddingABook()
    {
        var db = new LibraryDB();
        db.Connect();
        db.AddingABook("1984", "George Orwell", "Fiction");
        Console.WriteLine("Kirja on lisätty.");
    }

    public static void TestRemovingABook()
    {
        var db = new LibraryDB();
        db.Connect();
        db.AddingABook("1984", "George Orwell", "Fiction");
        db.RemovingABook("1984");
        Console.WriteLine("Kirja on poistettu.");
    }
}