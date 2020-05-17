using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace DBScanner
{
    public class Scanner
    {
        const string TABLES = "Tables";
        const string DATABASES = "Databases";

        const string COLUMNS = "Columns";

        const string COLUMN_NAME = "COLUMN_NAME";

        const string FOREIGN_KEYS = "Foreign Keys";

        private readonly DbConnection connection;

        public Scanner(DbConnection connection)
        {
            this.connection = connection;
            this.connection.Open();
        }

        public ISet<string> GetDatabases()
        {
            ISet<string> dbNames = new HashSet<string>();
            foreach (DataRow row in this.connection.GetSchema(DATABASES).Rows)
            {
                string dbName = row[1].ToString();
                dbNames.Add(dbName);
            }
            return dbNames;
        }

        public ISet<string> GetTables(string database)
        {
            ISet<string> tables = new HashSet<string>();
            var restrictions = new string[] { null, database, null, null };
            foreach (DataRow row in this.connection.GetSchema(TABLES, restrictions).Rows)
            {
                string table = row[2].ToString();
                tables.Add(table);
            }
            return tables;
        }

        public ISet<string> GetTableColumns(string database, string table)
        {
            ISet<string> columns = new HashSet<string>();
            var restrictions = new string[] { null, database, table, null };
            DataTable dataTable = this.connection.GetSchema(COLUMNS, restrictions);
            foreach (DataRow col in dataTable.Rows)
            {
                columns.Add(col[COLUMN_NAME].ToString());
            }
            return columns;
        }

        public ISet<string> GetTableReferences(string database, string table)
        {
            ISet<string> columns = new HashSet<string>();
            var restrictions = new string[] { null, database, table, null };
            DataTable dataTable = this.connection.GetSchema(FOREIGN_KEYS, restrictions);

            foreach (DataRow col in dataTable.Rows)
            {
                // string column = "";
                // foreach (DataColumn colDef in dataTable.Columns)
                // {
                //     column += String.Format("{0} => {1}, ", colDef.ColumnName, col[colDef.ColumnName].ToString()) ;
                // }
                columns.Add(col["REFERENCED_TABLE_NAME"].ToString());
            }
            return columns;
        }
    }
}