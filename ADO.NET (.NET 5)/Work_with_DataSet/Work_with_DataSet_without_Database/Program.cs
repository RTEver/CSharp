using System;
using System.Data;

namespace Work_with_DataSet_without_Database
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            var dataSet = new DataSet("TestSet");

            var users = new DataTable("Users");

            dataSet.Tables.Add(users);

            var idColumn = new DataColumn("Id", Type.GetType("System.Int32"));

            idColumn.Unique = true;
            idColumn.AllowDBNull = false;
            idColumn.AutoIncrement = true;
            idColumn.AutoIncrementSeed = 1;
            idColumn.AutoIncrementStep = 1;

            var nameColumn = new DataColumn("Name", Type.GetType("System.String"));

            var ageColumn = new DataColumn("Age", Type.GetType("System.Int32"));

            ageColumn.DefaultValue = 1;

            users.Columns.Add(idColumn);
            users.Columns.Add(nameColumn);
            users.Columns.Add(ageColumn);

            users.PrimaryKey = new DataColumn[] { users.Columns["Id"] };

            var row = users.NewRow();

            row.ItemArray = new Object[] { null, "Tom", 36 };

            users.Rows.Add(row);

            users.Rows.Add(new Object[] { null, "Bob", 29 });

            Console.WriteLine("Id Name Age");
            
            foreach (DataRow userRow in users.Rows)
            {
                Console.WriteLine("{0,-2} {1,-4} {2,-3}", userRow["Id"], userRow["Name"], userRow["Age"]);
            }
        }
    }
}