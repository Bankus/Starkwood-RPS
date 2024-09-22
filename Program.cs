
using Starkwood_RPS;

Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("------Starkwood RPS--------\n\n");
Console.ForegroundColor = ConsoleColor.White;

Console.Write("Enter your name: ");
var name = Console.ReadLine();

var game = new Game(name);

Console.WriteLine($"Save game record to file? y/n");
var yes = Console.ReadLine().ToLower() == "y";
if (yes)
{
    var now = DateTime.Now.Date;
    var path = AppDomain.CurrentDomain.BaseDirectory;
    var filename = $"Starkwood RPS - {game.PlayerName} - T{now.Year}{now.Month}{now.Day}.txt";
    using (StreamWriter outputFile = new
        StreamWriter(Path.Combine(path, filename)))
    {
        outputFile.Write(game.GameRecord.ToString());
        Console.WriteLine($"Record {filename} saved to {path}");
    }
}