using System;
using System.Data;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;

namespace SqlCommand
{
    internal static class Program : Object
    {
        private const String MASTER_CONNECTION_STRING = "Server = (localdb)\\mssqllocaldb; Database = master; Trusted_Connection = True;";

        private const String TEMPLATE_CONNECTION_STRING = "Server = (localdb)\\mssqllocaldb; Database = {0}; Trusted_Connection = True;";

        private static async Task Main(String[] args)
        {
            await CreateDatabaseAsync("testdb");

            await CreateUsersTableAsync("testdb");

            await CreateInsertUserStoredProcedureAsync("testdb");
            await CreateGetAgeRangeStoredProcedureAsync("testdb");

            await AddUserAsync("testdb", new User(-1, "Vitali", 21));
            await AddUserAsync("testdb", new User(-1, "Andrey", 21));
            await AddUserAsync("testdb", new User(-1, "Vitali", 20));

            await ShowUsersTableAsync("testdb");
            await ShowAgeRangeAsync("testdb", "Vitali");
        }

        private static async Task CreateDatabaseAsync(String databaseName)
        {
            var commandText =
                "\nDECLARE @template NVARCHAR(MAX);"                                         +
                "\n"                                                                         +
                "\nSET @template = N'CREATE DATABASE {DATABASE_NAME};';"                     +
                "\n"                                                                         +
                "\nDECLARE @expression NVARCHAR(MAX);"                                       +
                "\n"                                                                         +
                "\nSET @expression = REPLACE(@template, N'{DATABASE_NAME}', @databaseName);" +
                "\n"                                                                         +
                "\nEXECUTE(@expression);";

            await DeleteDatabaseAsync(databaseName);

            using (var connection = new SqlConnection(MASTER_CONNECTION_STRING))
            {
                await connection.OpenAsync();

                var command = new Microsoft.Data.SqlClient.SqlCommand(commandText, connection);

                var databaseNameParameter = new SqlParameter("@databaseName", SqlDbType.NVarChar, 100)
                {
                    Value = databaseName,
                    IsNullable = false,
                };

                command.Parameters.Add(databaseNameParameter);

                var result = await command.ExecuteNonQueryAsync();

                Console.WriteLine("{0} database is created. ({1})", databaseName, result);
            }
        }

        private static async Task DeleteDatabaseAsync(String databaseName)
        {
            var commandText =
                "\nDECLARE @template NVARCHAR(MAX);"                                         +
                "\n"                                                                         +
                "\nSET @template = N'IF DB_ID(N''{DATABASE_NAME}'') IS NOT NULL"             +
                "\n                  BEGIN"                                                  +
                "\n                      DROP DATABASE {DATABASE_NAME};"                     +
                "\n                  END';"                                                  +
                "\n"                                                                         +
                "\nDECLARE @expression NVARCHAR(MAX);"                                       +
                "\n"                                                                         +
                "\nSET @expression = REPLACE(@template, N'{DATABASE_NAME}', @databaseName);" +
                "\n"                                                                         +
                "\nEXECUTE(@expression);";

            using (var connection = new SqlConnection(MASTER_CONNECTION_STRING))
            {
                await connection.OpenAsync();

                var command = new Microsoft.Data.SqlClient.SqlCommand(commandText, connection);

                var databaseNameParameter = new SqlParameter("@databaseName", SqlDbType.NVarChar, 100)
                {
                    Value = databaseName,
                    IsNullable = false,
                };

                command.Parameters.Add(databaseNameParameter);

                var result = await command.ExecuteNonQueryAsync();

                if (result != 0)
                {
                    Console.Write("{0} database is deleted. ", databaseName);
                }

                Console.WriteLine("({0})", result);
            }
        }

        private static async Task CreateUsersTableAsync(String databaseName)
        {
            var commandText =
                Environment.NewLine + "CREATE TABLE [Users]"                                    +
                Environment.NewLine + "("                                                       +
                Environment.NewLine + "    [Id] INT IDENTITY(1, 1),"                            +
                Environment.NewLine + "    [Name] NVARCHAR(100) CHECK([Name] != N'') NOT NULL," +
                Environment.NewLine + "    [Age] INT CHECK([Age] > 0) NOT NULL,"                +
                Environment.NewLine + "    CONSTRAINT PK_Users_Id PRIMARY KEY ([Id]),"          +
                Environment.NewLine + ");";

            var connectionString = String.Format(TEMPLATE_CONNECTION_STRING, databaseName);

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new Microsoft.Data.SqlClient.SqlCommand(commandText, connection);

                var result = await command.ExecuteNonQueryAsync();

                Console.WriteLine("Users table is created. ({0})", result);
            }
        }

        private static async Task CreateInsertUserStoredProcedureAsync(String databaseName)
        {
            var commandText = @"CREATE PROCEDURE [dbo].[sp_InsertUser]
                                    @name NVARCHAR(100),
                                    @age  INT
                                AS
                                    INSERT INTO [Users]
                                        ([Name], [Age])
                                    VALUES
                                        (@name, @age);
   
                                    SELECT SCOPE_IDENTITY()
                                GO";

            var connectionString = String.Format(TEMPLATE_CONNECTION_STRING, databaseName);

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new Microsoft.Data.SqlClient.SqlCommand(commandText, connection);

                var result = await command.ExecuteNonQueryAsync();

                Console.WriteLine("sp_InsertUser stored procedure is added. ({0})", result);
            }
        }

        private static async Task CreateGetAgeRangeStoredProcedureAsync(String databaseName)
        {
            var commandText = @"CREATE PROCEDURE [dbo].[sp_GetAgeRange]
                                    @name   NVARCHAR(100),
                                    @minAge INT OUT,
                                    @maxAge INT OUT
                                AS
                                    SELECT
                                        @minAge = MIN([Age]),
                                        @maxAge = MAX([Age])
                                    FROM [Users]
                                    WHERE [Name] LIKE '%' + @name + '%';";

            var connectionString = String.Format(TEMPLATE_CONNECTION_STRING, databaseName);

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new Microsoft.Data.SqlClient.SqlCommand(commandText, connection);

                var result = await command.ExecuteNonQueryAsync();

                Console.WriteLine("sp_GetAgeRange stored procedure is added. ({0})", result);
            }
        }

        private static async Task AddUserAsync(String databaseName, User user)
        {
            var commandText = @"sp_InsertUser";

            var connectionString = String.Format(TEMPLATE_CONNECTION_STRING, databaseName);

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new Microsoft.Data.SqlClient.SqlCommand(commandText, connection);

                command.CommandType = CommandType.StoredProcedure;

                var nameParameter = new SqlParameter("@name", user.Name);
                var ageParameter = new SqlParameter("@age", user.Age);

                command.Parameters.Add(nameParameter);
                command.Parameters.Add(ageParameter);

                user.Id = (Int32)(Decimal)await command.ExecuteScalarAsync();

                Console.WriteLine("{0} is added.", user);
            }
        }

        private static async Task ShowAgeRangeAsync(String databaseName, String userName)
        {
            var commandText = @"sp_GetAgeRange";

            var connectionString = String.Format(TEMPLATE_CONNECTION_STRING, databaseName);

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new Microsoft.Data.SqlClient.SqlCommand(commandText, connection);

                command.CommandType = CommandType.StoredProcedure;

                var nameParameter = new SqlParameter("@name", userName);

                var minAgeParameter = new SqlParameter()
                {
                    ParameterName = "@minAge",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output,
                };

                var maxAgeParameter = new SqlParameter()
                {
                    ParameterName = "@maxAge",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output,
                };

                command.Parameters.Add(nameParameter);
                command.Parameters.Add(minAgeParameter);
                command.Parameters.Add(maxAgeParameter);

                var result = await command.ExecuteNonQueryAsync();

                var minAge = command.Parameters["@minAge"].Value;
                var maxAge = command.Parameters["@maxAge"].Value;

                Console.WriteLine("Age range of users with part '{0}' in name: ({1})", userName, result);
                Console.WriteLine("\tMinimum age: {0}", minAge);
                Console.WriteLine("\tMaximum age: {0}", maxAge);
            }
        }

        private static async Task ShowUsersTableAsync(String databaseName)
        {
            var commandText = @"SELECT
                                    [Id]   AS [User id],
                                    [Name] AS [User name],
                                    [Age]  AS [User age]
                                FROM [Users];";

            var connectionString = String.Format(TEMPLATE_CONNECTION_STRING, databaseName);

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new Microsoft.Data.SqlClient.SqlCommand(commandText, connection);

                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    var idColumnIndex   = reader.GetOrdinal("User id");
                    var nameColumnIndex = reader.GetOrdinal("User name");
                    var ageColumnIndex  = reader.GetOrdinal("User age");
                    
                    Console.WriteLine("User id User name User age");

                    while (await reader.ReadAsync())
                    {
                        var id   = reader.GetInt32(idColumnIndex);

                        var name = reader.GetString(nameColumnIndex);

                        var age  = reader.GetInt32(ageColumnIndex);

                        Console.WriteLine("{0,-7} {1,-9} {2}", id, name, age);
                    }
                }
            }
        }
    }
}