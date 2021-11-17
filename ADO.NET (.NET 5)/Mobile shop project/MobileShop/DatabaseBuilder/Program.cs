using System;
using System.IO;
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
            //await DeleteDatabase("MobileShop");

            await CreateDatabase("MobileShop");

            await CreateMobilesTable("MobileShop");

            await CreateCustomersTable("MobileShop");

            await CreateOrdersTable("MobileShop");
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

        private static async Task CreateMobilesTable(String databaseName)
        {
            var commandText =
            $@"
                USE {databaseName};

                CREATE TABLE [Mobiles]
                (
                    [Id]   INT           IDENTITY(1, 1),
                    [Name] NVARCHAR(100) CHECK([Name] != N'') NOT NULL,
                    [Cost] INT           CHECK([Cost] >= 0)   NOT NULL,
                    CONSTRAINT PK_Mobiles_Id PRIMARY KEY ([Id])
                );
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(commandText, connection);

                await command.ExecuteNonQueryAsync();
            }

            Console.WriteLine("Mobiles table successfully created.");
        }

        private static async Task CreateCustomersTable(String databaseName)
        {
            var commandText =
            $@"
                USE {databaseName};

                CREATE TABLE [Customers]
                (
                    [Id]       INT           IDENTITY(1, 1),
                    [Name]     NVARCHAR(150) CHECK([Name] != N'') NOT NULL,
                    [Telephon] NVARCHAR(20)  CHECK([Telephon] != N'') NOT NULL,
                    CONSTRAINT PK_Customers_Id PRIMARY KEY ([Id])
                );
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(commandText, connection);

                await command.ExecuteNonQueryAsync();
            }

            Console.WriteLine("Customers table successfully created.");
        }

        private static async Task CreateOrdersTable(String databaseName)
        {
            var commandText =
            $@"
                USE {databaseName};

                CREATE TABLE [Orders]
                (
                    [Id]         INT IDENTITY(1, 1),
                    [CustomerId] INT NOT NULL,
                    [MobileId]   INT NOT NULL,
                    CONSTRAINT PK_Orders_Id PRIMARY KEY ([Id]),
                    CONSTRAINT FK_Orders_To_Customers
                        FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id])
                            ON UPDATE CASCADE
                            ON DELETE CASCADE,
                    CONSTRAINT FK_Orders_To_Mobiles
                        FOREIGN KEY ([MobileId]) REFERENCES [Mobiles] ([Id])
                            ON UPDATE CASCADE
                            ON DELETE CASCADE,
                );
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand(commandText, connection);

                await command.ExecuteNonQueryAsync();
            }

            Console.WriteLine("Orders table successfully created.");
        }
    }
}