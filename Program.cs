namespace _5KirjastoRyhmatehtava;

class Program
{
    static void Main(string[] args)
    {
        LibraryDB libraryDB = new LibraryDB();

        while (true)
        {
            Console.WriteLine(
                "Haluatko lisätä kirjaa (L),\n" +
                "poistaa kirjaa (D),\n" +
                "päivittää kirjojen tietoja (P),\n" +
                "Lainaa ja palauta kirjoja (O),\n" +
                "Tarkista kirjan tai henkilön lainatiedot (T) \n" +
                "vai Lopettaa(X)?"
            );

            string? command = Console.ReadLine();

            switch (command)
            {
                case "L":
                    Console.WriteLine("Kirjoita kirjan nimi.");
                    string? titleOfBook = Console.ReadLine();
                    Console.WriteLine("Kirjoita kirjan tekijän nimi.");
                    string? authorOfBook = Console.ReadLine();
                    Console.WriteLine("Kirjoita kirjan genre");
                    string? category = Console.ReadLine();
                    if (!string.IsNullOrEmpty(titleOfBook) && !string.IsNullOrEmpty(authorOfBook) && !string.IsNullOrEmpty(category))
                    {
                        libraryDB.AddingABook(titleOfBook, authorOfBook, category);
                    }
                    else
                    {
                        Console.WriteLine("Tarvitsen kaikki tiedot lisätäkseni kirjan.");
                    }
                    break;
                case "D":
                    Console.WriteLine("Minkä kirjan pitäisi poistaa?");
                    string? booksTitle = Console.ReadLine();
                    if (!string.IsNullOrEmpty(booksTitle))
                    {
                        libraryDB.DeletingABook(booksTitle);
                    }
                    else
                    {
                        Console.WriteLine("Kirjan nimi ei voi olla tyhjä.");
                    }
                    break;

                case "X":
                    return;

                default:
                    Console.WriteLine("Väärä valinta. Valitse: L, D, P, O, T, X ");
                    break;
            }
        }
    }
}
