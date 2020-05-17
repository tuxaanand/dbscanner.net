using System.Collections.Generic;

namespace DBScanner
{
    public class Database
    {
        public string Name { get; }
        public List<Table> Tables { get; }

        internal Database(List<Table> tables)
        {
            this.Tables = tables;
        }
    }

    public class Table
    {
        public string Name { get; }
        public List<Column> Columns { get; }

        internal Table(List<Column> columns)
        {
            this.Columns = columns;
        }
    }

    public class Column
    {
        public string Name { get; }
        public string Type { get; }
        public Reference Reference { get; }

        internal Column(string name, string type, Reference reference)
        {
            this.Name = name;
            this.Type = type;
            this.Reference = reference;
        }
    }

    public class Reference
    {
        //
        // Summary: 
        //      Foreign Key Reference Name.
        //
        public string Name { get; }
        //Table that the column is referring
        public string ReferredTable { get; }
        //Referred Column name
        public string ReferredColumn { get; }
    }

}