using ConsoleAppFramework;
using HomeBook.Sdk.Tooling.Cli.Commands;

var app = ConsoleApp.Create();

app.Add<CheckCommands>();
app.Add<InitCommands>();

app.Run(args);
