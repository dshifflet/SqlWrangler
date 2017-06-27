using System;
using System.Collections.Generic;

namespace SqliteTools
{
    
    public static class SqliteTypes
    {
        public static Dictionary<Type, string> Types = new Dictionary<Type, string>()
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
    }
}
