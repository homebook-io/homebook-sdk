using ConsoleAppFramework;
using HomeBook.Sdk.Tooling.Core.Init;
using HomeBook.Sdk.Tooling.Core.Init.Database;

namespace HomeBook.Sdk.Tooling.Cli.Commands;

public class InitCommands
{
    /// <summary>
    /// Initializes a database for development
    /// </summary>
    /// <param name="type">-t, the Database to create (PostgreSql, MySQL)</param>
    [Command("init database")]
    public async Task InitDatabaseAsync(DatabaseType type)
        => await new InitDatabaseCommandHandler().HandleAsync(type);
}
