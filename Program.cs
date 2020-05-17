using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using DBScanner;

namespace dot101
{
    class Program
    {
        static string name = "Aanand";
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"Hello {name} !");
            Console.WriteLine("Time is : " + DateTime.Now);

            // string s1 = "test";
            // string s2 = Console.ReadLine();
            // Console.WriteLine(s1.Equals(s2));
            // Console.WriteLine(ReferenceEquals(s1, s2));

            Decimal d1 = new Decimal(2.0);
            Decimal d2 = new Decimal(1.9);
            Decimal d3 = d1 - d2;

            Console.WriteLine(d3);

            DbConnection conn = new MySqlConnection(@"server=localhost;userid=root;password=admin@123;");
            
            Scanner scanner = new Scanner(conn);
            foreach (var db in scanner.GetDatabases())
            {
                Console.WriteLine("{0}",db);
                foreach (var tab in scanner.GetTables(db))
                {
                    Console.WriteLine("\t{0}", tab);
                    foreach (var col in scanner.GetTableColumns(db, tab))
                    {
                        Console.WriteLine("\t\t{0}", col);
                    }
                    
                    foreach (var reference in scanner.GetTableReferences(db, tab))
                    {
                        Console.WriteLine("\t\t\t{0}", reference);
                    }
                }
            }

            conn.Close();
        }

    }
}
