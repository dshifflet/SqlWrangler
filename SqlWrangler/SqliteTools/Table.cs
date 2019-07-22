using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace SqliteTools
{
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
    public class Table
    {
        /// <summary>
        /// Schema of Table
        /// </summary>
        private string Schema { get; set; }
        /// <summary>
        /// Table Name
        /// </summary>
        private string Name { get; set; }

        public static readonly Dictionary<Type, string> SqlLiteTypes = new Dictionary<Type, string>()
        {
            {typeof(short),     "SMALLINT"},
            {typeof(int),       "INT"},
            {typeof(long),      "BIGINT"},
            {typeof(byte[]),    "BLOB"},
            {typeof(bool),      "BOOLEAN"},
            {typeof(string),    "VARCHAR"},
            {typeof(char[]),    "VARCHAR"},
            {typeof(DateTime),  "DATETIME"},
            {typeof(decimal),   "DECIMAL(10,5)"}, //todo need to think about this one...
            {typeof(double),    "FLOAT"},
            {typeof(float),     "REAL"},
            {typeof(byte),      "TINYINT"}
        };

        public string ActualName
        {
            get
            {
                if (Schema == null)
                {
                    return Name;
                }
                return $"{Schema}{(string.IsNullOrEmpty(Schema) ? "" : "_")}{Name}";
            }
        }
        
        /// <summary>
        /// Create a new Sqlite Table
        /// </summary>
        /// <param name="name">The name of the table</param>
        /// <param name="schema">The schema of the table.  (Note: this will be schema_tablename, null will be tablename)</param>
        public Table(string name, string schema)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Schema = schema;
        }

        /// <summary>
        /// Generate the table from a DataTable.
        /// </summary>
        /// <param name="table">The datatable to create the Sqlite table from</param>
        public string CreateSql(DataTable table)
        {
            return GenerateCreateTableSql(table);
        }

        /// <summary>
        /// Import data from a datatable to the table.
        /// </summary>
        /// <param name="table"></param>
        public string GenerateImportDataSql(DataTable table)
        {
            var sb = new StringBuilder();
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine(GenerateInsertSql(row) + ";");
            }
            return sb.ToString();
        }

        private string GenerateCreateTableSql(DataTable table)
        {
            var sql = $"create table {ActualName} (\r\n";
            string columns = null;
            var idx = 0;
            foreach (DataColumn col in table.Columns)
            {
                idx++;
                //var tst = col.ColumnName;

                //if these types need to be more precise like for decimal...
                //we would need to change it so it works off a datatble returned from something like DataReader.GetSchemaTable.
                var sqlType = SqlLiteTypes[col.DataType];

                if (col.MaxLength > 0) sqlType += $"({col.MaxLength})";

                var options = "";
                //Can be null?
                if (!col.AllowDBNull) options += " NOT NULL";
                //Is it unique
                if (col.Unique) options += " UNIQUE";

                if (col.DefaultValue != DBNull.Value)
                {
                    var defaultValue = col.DefaultValue.ToString();

                    if (ReferenceEquals(col.DefaultValue, typeof(string)))
                    {
                        //todo make sure this works, cause this is some lame escaping.
                        defaultValue = $"'{defaultValue.Replace("'", "''")}'";
                    }
                    options += $" DEFAULT({defaultValue})";
                }
                
                //primary keys
                if (table.PrimaryKey.Contains(col)) options = " PRIMARY KEY " + options;
                if (idx != table.Columns.Count)
                {
                    options += ",";
                }
                columns += $"{col.ColumnName} {sqlType}{options}\r\n";
                
            }
            sql += columns + ");\r\n";
            return sql;
        }

        private string GenerateInsertSql(DataRow row)
        {          
            string columns = null;
            string values = null;
            foreach (DataColumn col in row.Table.Columns)
            {
                if (columns != null)
                {
                    columns += ", ";
                    values += ", ";
                }
              
                columns += col.ColumnName;
                var val = row[col.ColumnName].ToString();

                //todo parameterize values, would be much simpler and safer!!!
                if (row[col.ColumnName] == DBNull.Value)
                {
                    if (col.AllowDBNull)
                    {
                        values += "null";
                    }
                    else
                    {
                        if (IsString(col.DataType))
                        {
                            values += "''"; //zero length string???    
                        }
                        else
                        {
                            throw new Exception("Unable to insert null into a column that doesn't allow nulls");
                        }
                    }
                }
                else if (IsString(col.DataType))
                {
                    values += $"'{val.Replace("'", "''")}'";
                }
                else if (IsDateTime(col.DataType))
                {
                    val = ((DateTime)row[col.ColumnName]).ToString("yyyy-MM-dd HH:mm:ss.FFF");
                    values += $"'{val}'";
                }
                else
                {
                    values += val;
                }
            }
            var sql = $"insert into {ActualName} ({columns}) values ({values})";

            return sql;
        }

        private bool IsString(Type t)
        {
            return t == typeof (string);
        }

        private bool IsDateTime(Type t)
        {
            return t == typeof(DateTime);
        }
    }
}
