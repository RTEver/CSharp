using System;
using System.Data;

using Microsoft.Data.SqlClient;

namespace SqlDataAdapter_LoadingDataInDataSet
{
    internal static class Program : Object
    {
        private const String connectionString = "Server = (localdb)\\mssqllocaldb; Database = testdb; Trusted_Connection = True;";

        private const String selectCommandText = "SELECT [Id], [Name], [Age] FROM [Users];";

        private static void Main(String[] args)
        {
            var adapter = new SqlDataAdapter(selectCommandText, connectionString);

            var dataSet = CreateFilledDataSet(adapter);

            ReadDataSet(dataSet);
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