using System;
using System.Data;

namespace CustomLIB.RandomLIB.DatatableToList
{
    public class Test
    {
        public static void Execute()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");
            dt.Columns.Add("foo");
            dt.Columns.Add("foo2");
            dt.Columns.Add("foo3");
            dt.Columns.Add("foo4");
            for (int i = 0; i <= 1000; i++) { dt.Rows.Add(i, "foo", 1.1, null, true, DateTime.Now); }

            var typedList = Convertion.DataTableToList<Info>(dt);

            Console.WriteLine("Spotchecking first value");
            Console.WriteLine(typedList[0].foo);
            Console.WriteLine(typedList[0].foo2);
            Console.WriteLine(typedList[0].foo3);
            Console.WriteLine(typedList[0].foo4);
        }
    }
}