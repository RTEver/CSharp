using System;
using System.Data;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;

namespace ConnectionPool
{
    internal static class Program : Object
    {
        private const String connectionString_1 = "Server = (localdb)\\mssqllocaldb; Database = master; Trusted_Connection = True;";
        private const String connectionString_2 = "Server = (localdb)\\mssqllocaldb; Database = testdb; Trusted_Connection = True;";

        private static async Task Main(String[] args)
        {
            await ConnectionPool_Test();
        }

        private static async Task ConnectionPool_Test()
        {
            await OpenConnection_OutputConnectionInfoInConsole_CloseConnection(connectionString_1);

            await OpenConnection_OutputConnectionInfoInConsole_CloseConnection(connectionString_2);

            await OpenConnection_OutputConnectionInfoInConsole_CloseConnection(connectionString_1);
        }

        private static async Task OpenConnection_OutputConnectionInfoInConsole_CloseConnection(String connectionString)
        {
            if (String.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                OutputInfoAboutConnectionInConsole(connection);
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
                Console.WriteLine($"\tWorkstation id: {connection.WorkstationId}");
                Console.WriteLine($"\tClient connection id: {connection.ClientConnectionId}");
                Console.WriteLine();
            }
        }
    }
}