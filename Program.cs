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
                case "D":
                    Console.WriteLine("Minkä kirjan pitäisi poistaa?");
                    string? kirjanNimi = Console.ReadLine();
                    if (!string.IsNullOrEmpty(kirjanNimi))
                    {
                        libraryDB.DeletingABook(kirjanNimi);
                    }
                    else
                    {
                        Console.WriteLine("Kirjan nimi ei voi olla tyhjä.");
                    }
                    break;

                case "X":
                    return;
            }
        }
    }
}
