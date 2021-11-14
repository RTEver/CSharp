using System;
using System.IO;
using System.Data;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DatabaseBuilder
{
    internal static class Program : Object
    {
        private static readonly String connectionString;

        static Program()
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();

            connectionString = config.GetConnectionString("DefaultConnection");
        }

        private static async Task Main(String[] args)
        {
            await CreateDatabase("MobileShop");


        }

        private static async Task CreateDatabase(String databaseName)
        {
            if (String.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentNullException(nameof(databaseName));
            }

            var commandText = $"CREATE DATABASE [{databaseName}];";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(commandText, connection);

                await command.ExecuteNonQueryAsync();
            }

            Console.WriteLine($"Database {databaseName} successfully created.");
        }

        private static async Task DeleteDatabase(String databaseName)
        {
            if (String.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentNullException(nameof(databaseName));
            }

            var commandText = $"DROP DATABASE [{databaseName}];";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(commandText, connection);

                await command.ExecuteNonQueryAsync();
            }

            Console.WriteLine($"Database {databaseName} successfully deleted.");
        }

        private static async Task CreateMobilesTable()
        {
            var commandText =
            @"
                CREATE TABLE []
                (
                    [Id] INT IDENTITY(1, 1),
                    [Name] NVARCHAR(),
                    CONSTRAINT 
                );
            ";
        }
    }
}