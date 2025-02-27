using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using DBScanner;

namespace dot101
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbName = null;
            if (args.Length > 0)
            {
                dbName = args[0];
            }

            DbConnection conn = new MySqlConnection(@"server=localhost;userid=dragon;password=Dragon_786;");
            //DbConnection conn = new OracleConnection(@"data source=//localhost:1521;user id=AU_CUSTOMER_PORTAL_MSME;password=Pt#24msmE;");

            Scanner scanner = new Scanner(conn);
            if (dbName != null)
            {
                PrintDB(scanner, dbName);
            }
            else
            {
                foreach (var db in scanner.GetDatabases())
                {
                    Console.WriteLine("{0}", db);
                    PrintDB(scanner, db);
                }
            }

            conn.Close();
        }

        private static void PrintDB(Scanner scanner, string db)
        {
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

    }
}
