using System.Diagnostics;

namespace HomeBook.Sdk.Tooling.Core.Setup.Database;

public class CommandHandler
{
    public async Task HandleAsync(DatabaseType dbType)
    {
        Console.WriteLine($"Setting up database for {dbType}...");

        EnsureDockerAvailable();

        switch (dbType)
        {
            case DatabaseType.PostgreSQL:
                await SetupPostgresAsync();
                break;

            case DatabaseType.MySQL:
                await SetupMySqlAsync();
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(dbType), dbType, "Unsupported database type");
        }
    }

    private async Task SetupPostgresAsync()
    {
        const string containerName = "homebook-sdk-db-postgres";
        const string image = "postgres:16";
        const int port = 54329;

        if (!await ContainerExistsAsync(containerName))
        {
            RunDocker($"pull {image}");

            RunDocker(
                $"run -d " +
                $"--name {containerName} " +
                $"-e POSTGRES_DB=homebook " +
                $"-e POSTGRES_USER=homebook " +
                $"-e POSTGRES_PASSWORD=homebook " +
                $"-p {port}:5432 " +
                $"-v homebook-sdk-postgres-data:/var/lib/postgresql/data " +
                image
            );
        }
        else if (!await ContainerRunningAsync(containerName))
        {
            RunDocker($"start {containerName}");
        }

        Console.WriteLine();
        Console.WriteLine("PostgreSQL ready:");
        PrintConnectionInfo(
            provider: "PostgreSQL",
            host: "localhost",
            port: port,
            database: "homebook",
            user: "homebook",
            password: "homebook",
            connectionString:
            $"Host=localhost;Port={port};Database=homebook;Username=homebook;Password=homebook"
        );
    }

    private async Task SetupMySqlAsync()
    {
        const string containerName = "homebook-sdk-db-mysql";
        const string image = "mysql:8.4";
        const int port = 33069;

        if (!await ContainerExistsAsync(containerName))
        {
            RunDocker($"pull {image}");

            RunDocker(
                $"run -d " +
                $"--name {containerName} " +
                $"-e MYSQL_DATABASE=homebook " +
                $"-e MYSQL_USER=homebook " +
                $"-e MYSQL_PASSWORD=homebook " +
                $"-e MYSQL_ROOT_PASSWORD=root " +
                $"-p {port}:3306 " +
                $"-v homebook-sdk-mysql-data:/var/lib/mysql " +
                image
            );
        }
        else if (!await ContainerRunningAsync(containerName))
        {
            RunDocker($"start {containerName}");
        }

        Console.WriteLine();
        Console.WriteLine("MySQL ready:");
        PrintConnectionInfo(
            provider: "MySQL",
            host: "localhost",
            port: port,
            database: "homebook",
            user: "homebook",
            password: "homebook",
            connectionString:
            $"Server=localhost;Port={port};Database=homebook;User=homebook;Password=homebook"
        );
    }

    private void EnsureDockerAvailable()
    {
        try
        {
            RunDocker("info", silent: true);
        }
        catch
        {
            throw new InvalidOperationException("Docker is not available or not running.");
        }
    }

    private async Task<bool> ContainerExistsAsync(string name)
    {
        var output = RunDocker($"ps -a --filter name=^{name}$ --format \"{{{{.Names}}}}\"", silent: true);
        return !string.IsNullOrWhiteSpace(output);
    }

    private async Task<bool> ContainerRunningAsync(string name)
    {
        var output = RunDocker($"ps --filter name=^{name}$ --format \"{{{{.Names}}}}\"", silent: true);
        return !string.IsNullOrWhiteSpace(output);
    }

    private string RunDocker(string args, bool silent = false)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "docker",
            Arguments = args,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false
        };

        using var process = Process.Start(psi)!;
        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        if (process.ExitCode != 0)
            throw new InvalidOperationException(error);

        if (!silent && !string.IsNullOrWhiteSpace(output))
            Console.WriteLine(output.Trim());

        return output.Trim();
    }

    private static void PrintConnectionInfo(
        string provider,
        string host,
        int port,
        string database,
        string user,
        string password,
        string connectionString)
    {
        Console.WriteLine($"Provider : {provider}");
        Console.WriteLine($"Host     : {host}");
        Console.WriteLine($"Port     : {port}");
        Console.WriteLine($"Database : {database}");
        Console.WriteLine($"Username : {user}");
        Console.WriteLine($"Password : {password}");
        Console.WriteLine();
        Console.WriteLine("Connection String:");
        Console.WriteLine(connectionString);
    }
}
