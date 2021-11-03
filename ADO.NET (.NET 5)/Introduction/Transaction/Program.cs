using System;
using System.Data;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;

namespace Transaction
{
    internal static class Program : Object
    {
        private const String connectionString = "Server = (localdb)\\mssqllocaldb; Database = testdb; Trusted_Connection = True;";

        private static async Task Main(String[] args)
        {
            var commandText = "sp_InsertUser";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var transaction = (SqlTransaction)(await connection.BeginTransactionAsync());

                var command = new SqlCommand(commandText, connection);

                command.Transaction = transaction;

                command.CommandType = CommandType.StoredProcedure;

                var nameParameter = new SqlParameter("@name", SqlDbType.NVarChar, 100);
                var ageParameter = new SqlParameter("@age", DbType.Int32);

                command.Parameters.Add(nameParameter);
                command.Parameters.Add(ageParameter);

                try
                {
                    await AddUserAsync(command, new User(-1, "Alla", 19));
                    await AddUserAsync(command, new User(-1, "Lena", 19));

                    await transaction.CommitAsync();

                    Console.WriteLine("Adding is completed.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    await transaction.RollbackAsync();

                    Console.WriteLine("Adding is not completed. Rollbacking...");
                }
            }
        }

        private static async Task AddUserAsync(SqlCommand command, User user)
        {
            command.Parameters["@name"].Value = user.Name;
            command.Parameters["@age"].Value = user.Age;

            user.Id = (Int32)(Decimal)await command.ExecuteScalarAsync();

            Console.WriteLine("(Waiting) Added: {0}", user);
        }
    }
}