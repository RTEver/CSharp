using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;

namespace ConnectionString
{
    internal static class Program : Object
    {
        private const String connectionString = "Server = (localdb)\\mssqllocaldb; Database = master; Trusted_Connection = True;";

        private static async Task Main(String[] args)
        {
            await OpenConnection_Case1();

            for (var i = 3; i > 0; --i)
            {
                Console.WriteLine("Wait for a {0} seconds...", i);

                Thread.Sleep(1000);
            }

            await OpenConnection_Case2();
        }

        private static async Task OpenConnection_Case1()
        {
            var connection = new SqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();

                Console.WriteLine("Connection is opened...");

                OutputInfoAboutConnectionInConsole(connection);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();

                    Console.WriteLine("Connection is closed...");
                }
            }
        }

        private static async Task OpenConnection_Case2()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                Console.WriteLine("Connection is opened...");

                OutputInfoAboutConnectionInConsole(connection);

                await connection.CloseAsync();

                Console.WriteLine("Connection is closed...");
            }
        }

        private static void OutputInfoAboutConnectionInConsole(SqlConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            if (connection.State == ConnectionState.Open)
            {
                Console.WriteLine("Properties of connection:");
                Console.WriteLine($"\tConnection string: {connection.ConnectionString}");
                Console.WriteLine($"\tDatabase: {connection.Database}");
                Console.WriteLine($"\tServer: {connection.DataSource}");
                Console.WriteLine($"\tServer version: {connection.ServerVersion}");
                Console.WriteLine($"\tState: {connection.State}");
                Console.WriteLine($"\tWorkstationld: {connection.WorkstationId}");
            }
        }
    }
}