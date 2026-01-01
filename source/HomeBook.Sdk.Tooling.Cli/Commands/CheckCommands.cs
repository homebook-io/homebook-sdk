using ConsoleAppFramework;

namespace HomeBook.Sdk.Tooling.Cli.Commands;

public class CheckCommands
{
    [Command("check --dependencies")]
    public async Task CheckDependenciesAsync()
        => await new Core.Check.Dependencies.CommandHandler().HandleAsync();
}
