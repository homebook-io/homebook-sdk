using ConsoleAppFramework;
using HomeBook.Sdk.Tooling.Core.Check;

namespace HomeBook.Sdk.Tooling.Cli.Commands;

public class CheckCommands
{
    /// <summary>
    /// checks that all required dependencies are available
    /// </summary>
    [Command("check")]
    public async Task CheckAllAsync()
        => await new CheckAllCommandHandler().HandleAsync();
}
