using System.Diagnostics;

namespace HomeBook.Sdk.Tooling.Core.Check.Dependencies;

public class CommandHandler
{
    public async Task HandleAsync()
    {
        Console.WriteLine("Checking dependencies...");

        var isDockerRunning = await CheckDockerIsRunningAsync();
        if (isDockerRunning)
        {
            Console.WriteLine("✓ Docker is installed and running");
        }
        else
        {
            Console.WriteLine("✗ Docker is not installed or running");
        }
    }

    private async Task<bool> CheckDockerIsRunningAsync()
    {
        try
        {
            // Verwende 'docker version' Command - funktioniert auf allen Plattformen
            using var process = new Process();

            process.StartInfo = new ProcessStartInfo
            {
                FileName = "docker",
                Arguments = "version --format json",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            process.Start();

            // Warte auf Prozessende mit Timeout
            var completed = await Task.Run(() => process.WaitForExit(5000));

            if (!completed)
            {
                process.Kill();
                return false;
            }

            // Prüfe Exit Code - 0 bedeutet Docker läuft
            return process.ExitCode == 0;
        }
        catch (Exception)
        {
            // Docker Command nicht gefunden oder anderer Fehler
            return false;
        }
    }
}
