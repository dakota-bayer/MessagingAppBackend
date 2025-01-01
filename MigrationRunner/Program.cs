using System.Reflection;
using DbUp;

namespace MigrationRunner;

public class Program
{
    static int Main(string[] args)
    {
        var connectionString = "Host=localhost;Port=5432;Database=messaging;User Id=postgres;Password=admin;";

        // Ensure the DB exists
        EnsureDatabase.For.PostgresqlDatabase(connectionString);

        // Execute the migration scripts
        var upgrader = DeployChanges.To
            .PostgresqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();
            return -1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Database upgrade successful!");
        Console.ResetColor();

        return 0;
    }
}