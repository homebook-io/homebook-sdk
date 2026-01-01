using ConsoleAppFramework;
using HomeBook.Sdk.Tooling.Core.Setup.Database;

namespace HomeBook.Sdk.Tooling.Cli.Commands;

public class SetupCommands
{
    [Command("setup --database")]
    public async Task SetupDatabaseAsync(DatabaseType dbType)
        => await new Core.Setup.Database.CommandHandler().HandleAsync(dbType);
}
