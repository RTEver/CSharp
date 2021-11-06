using System;
using System.Data;

using Microsoft.Data.SqlClient;

namespace Save_DataSet_changes_in_Database
{
    internal static class Program : Object
    {
        private const String connectionString = "Server = (localdb)\\mssqllocaldb; Database = testdb; Trusted_Connection = True;";

        private const String selectCommandText = "SELECT [Id], [Name], [Age] FROM [Users];";

        private static void Main(String[] args)
        {
            var adapter = new SqlDataAdapter(selectCommandText, connectionString);

            var commandBuilder = new SqlCommandBuilder(adapter);

            var dataSet = CreateFilledDataSet(adapter);

            ReadDataSet(dataSet);

            AddRowInUsersTable(dataSet, "Yura", 19);

            ReadDataSet(dataSet);

            adapter.Update(dataSet);

            dataSet.Clear();

            dataSet = CreateFilledDataSet(adapter);

            ReadDataSet(dataSet);
        }

        private static void AddRowInUsersTable(DataSet dataSet, String name, Int32 age)
        {
            if (dataSet == null)
            {
                throw new ArgumentNullException(nameof(dataSet));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(dataSet));
            }

            if (age < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(age));
            }

            var usersTable = dataSet.Tables[0];

            var newUserRow = usersTable.NewRow();

            newUserRow["Name"] = name;
            newUserRow["Age"] = age;

            usersTable.Rows.Add(newUserRow);
        }

        private static DataSet CreateFilledDataSet(SqlDataAdapter adapter)
        {
            var dataSet = new DataSet();

            var addedRowsCount = adapter.Fill(dataSet);

            Console.WriteLine("{0} added rows.", addedRowsCount);

            return dataSet;
        }

        private static void ReadDataSet(DataSet dataSet)
        {
            var tables = dataSet.Tables;

            foreach (DataTable table in tables)
            {
                ShowColumnNames(table);

                var rows = table.Rows;

                foreach (DataRow row in rows)
                {
                    ShowRow(row);
                }

                Console.WriteLine();
            }
        }

        private static String[] GetColumnNames(DataTable table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            var columns = table.Columns;

            var columnNames = new String[columns.Count];

            for (var index = 0; index < columns.Count; ++index)
            {
                columnNames[index] = columns[index].ColumnName;
            }

            return columnNames;
        }

        private static void ShowColumnNames(DataTable table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            var columnNames = GetColumnNames(table);

            Console.WriteLine(String.Join(' ', columnNames));
        }

        private static void ShowRow(DataRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            Console.WriteLine(String.Join(' ', row.ItemArray));
        }
    }
}