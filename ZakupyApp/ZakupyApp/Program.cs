
using ZakupyApp;

Console.WriteLine("=============================================");
var listaparagonow = new ParagonMemory("Biedronka","23.12.2023");
Console.WriteLine($" Witamy w Programie wyliczającym tatystykę zakupu w sklepie {listaparagonow.Shop} zrobionych {listaparagonow.Data} ");
Console.WriteLine("__________statistics from MEMORY_____________");

listaparagonow.ParagonAdded += ListaParagonowSumeAdded;
void ListaParagonowSumeAdded(object sender, EventArgs args)
{
    Console.WriteLine(" Dodany nowy paragon");
}

while (true)
{
    var input = Console.ReadLine();
    if (input == "q")
    {
        break;
    }
    try
    {
        listaparagonow.AddParagon(input);
    }
    catch (Exception exception)
    {
        Console.WriteLine($"Exception catched: {exception.Message}");
    }
    Console.WriteLine("Wprowadź nową sumę akupu");
    Console.WriteLine("lub liknij 'Q' żeby Exit");
}

var statisticsfrommemory = listaparagonow.GetStatistics();
statisticsfrommemory.WriteLineStatistics();
Console.WriteLine("=============================================");
Console.WriteLine("Press any key to continue ...");
Console.ReadLine();


