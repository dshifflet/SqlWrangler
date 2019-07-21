using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SqlWrangler
{

    public enum MapperAttributeType
    {
        None,
        EntityFramework,
        Linq2Db
    }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal class FieldDefinition
    {
        public string TableName { get; set; }
        public string TableSchema { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }
        public string DbFieldName { get; set; }
        public int Length { get; set; }
        public bool AllowsNull { get; set; }
        public string Type { get; set; }
        public bool IsShortBool { get; set; }
    }


    public class Wizard
    {
        private readonly Dictionary<string, string> _fieldTypes = new Dictionary<string, string>
                {
                    {"Boolean", "bool"},
                    {"Byte", "byte"},
                    {"SByte", "sbyte"},
                    {"Char", "char"},
                    {"Decimal", "decimal"},
                    {"Double", "double"},
                    {"Single", "float"},
                    {"Int32", "int"},
                    {"Int64", "long"},
                    {"UInt32", "uint"},
                    {"UInt64", "ulong"},
                    {"Object", "object"},
                    {"Int16", "short"},
                    {"UInt16", "ushort"},
                    {"String", "string"},
                    {"DateTime" , "DateTime"}
                };

        public void WriteCsWizard(DataTable table, StreamWriter sw, string thenamespace, string sql)
        {
            sw.WriteLine("using System;");
            sw.WriteLine("using NHibernate.Mapping.ByCode.Conformist;");
            sw.WriteLine("");
            sw.WriteLine("namespace {0}", thenamespace);
            sw.WriteLine("{");
            sw.WriteLine("/*");
            sw.WriteLine(sql);
            sw.WriteLine("*/");
            sw.WriteLine("");
            var fields = GetFields(table.CreateDataReader()).ToArray();
            WriteModelClass(table, sw, fields, MapperAttributeType.None);
            WriteMappings(table, sw, fields);
            WriteMaterializer(table, sw, fields);
            sw.WriteLine("}");
            sw.WriteLine("");
            sw.WriteLine("");
            sw.WriteLine("/*****************************/");
            sw.WriteLine("/****   EntityFramework   ****/");
            sw.WriteLine("/*****************************/");
            sw.WriteLine("");
            sw.WriteLine("");
            WriteModelClass(table, sw, fields, MapperAttributeType.EntityFramework);
            sw.WriteLine("/*****************************/");
            sw.WriteLine("/****      LINQ 2 DB      ****/");
            sw.WriteLine("/*****************************/");
            sw.WriteLine("");
            sw.WriteLine("");
            WriteModelClass(table, sw, fields, MapperAttributeType.Linq2Db);
        }

        private void WriteModelClass(DataTable table, StreamWriter sw, IEnumerable<FieldDefinition> fields, MapperAttributeType attributeType)
        {            
            var tableNm = GetFieldName(table.TableName);

            sw.WriteLine("\t//MODEL");
            if (attributeType==MapperAttributeType.EntityFramework || attributeType == MapperAttributeType.Linq2Db)
            {
                sw.WriteLine("\t[Table(\"{0}\", Schema = \"TODO\")]", table.TableName);
            }
            sw.WriteLine("\tpublic class {0}", tableNm);
            sw.WriteLine("\t{");
            if (attributeType==MapperAttributeType.None)
            {
                sw.WriteLine("\t\t//{0}", table.TableName);
            }
            
            foreach (var field in fields)
            {
                var fieldType = field.Type;
                if (field.IsShortBool)
                {
                    fieldType = field.AllowsNull ? "bool?" : "bool";
                }
                switch (attributeType)
                {
                    case MapperAttributeType.EntityFramework:
                        WriteEntityFrameworkField(sw, field, fieldType);
                        break;
                    case MapperAttributeType.Linq2Db:
                        WriteLinq2DbField(sw, field, fieldType);
                        break;
                    default:
                        sw.WriteLine("\t\tpublic {0} {1} {{ get; set; }} //{2}", fieldType, field.Name, field.DbFieldName);
                        break;
                }
            }
            sw.WriteLine("\t}");
            sw.WriteLine();
            sw.WriteLine();

        }

        private void WriteLinq2DbField(StreamWriter sw, FieldDefinition field, string fieldType)
        {
            sw.WriteLine("");
            if (field.Name.Equals("Id"))
            {
                sw.WriteLine("\t\t[PrimaryKey, Identity]");
            }

            sw.WriteLine("\t\t[Column(Name = \"{0}\"), {1}]", field.DbFieldName,
                field.AllowsNull ? "Nullable" : "NotNull");

            sw.WriteLine("\t\tpublic {0} {1} {{ get; set; }}", fieldType, field.Name);
        }

        private void WriteEntityFrameworkField(StreamWriter sw, FieldDefinition field, string fieldType)
        {
            sw.WriteLine("");
            if (field.Name.Equals("Id"))
            {
                sw.WriteLine("\t\t[Key]");
            }
            sw.WriteLine("\t\t[Column(\"{0}\")]", field.DbFieldName);
            if (fieldType.Equals("string"))
            {
                if (!field.AllowsNull)
                {
                    sw.WriteLine("\t\t[Required]");
                }
                sw.WriteLine("\t\t[StringLength({0})]", field.Length);
            }
            sw.WriteLine("\t\tpublic {0} {1} {{ get; set; }}", fieldType, field.Name);
        }

        private void WriteMaterializer(DataTable table, StreamWriter sw, IEnumerable<FieldDefinition> fields)
        {
            var classNm = GetFieldName(table.TableName);

            sw.WriteLine("\t//BASIC MATERIALIZER");
            sw.WriteLine("\tprivate class Materializer{0}", classNm);
            sw.WriteLine("\t{");
            sw.WriteLine("\t\tprivate {0} Materialize(DbDataReader reader)", classNm);
            sw.WriteLine("\t\t{");
            sw.WriteLine("\t\t\treturn new {0}() {{", classNm);

            foreach (var field in fields)
            {
                var elements = new string[4];
                elements[3] = ",";

                if (!field.Type.EndsWith("?") && !field.Type.Equals("string"))
                {
                    elements[0] = $"({field.Type}) ";
                }
                else
                {                    
                    elements[2] = $" as {field.Type}";
                }

                elements[1] = $"reader[\"{field.DbFieldName}\"]";
                if (field.IsShortBool)
                {
                    //(test==null) ? (bool?) null : (test==1) ? true : false;
                    if (field.AllowsNull)
                    {
                        sw.WriteLine("\t\t\t\t{0} = {1}", field.Name,
                            string.Format(
                                "reader[\"{0}\"]==DBNull.Value ? (bool?) null : (short)reader[\"{0}\"]==1,",
                                field.DbFieldName));
                    }
                    else
                    {
                        sw.WriteLine("\t\t\t\t{0} = {1}", field.Name,
                            $"(short) reader[\"{elements[1]}\"]==1,");
                    }
                }
                else
                {
                    sw.WriteLine("\t\t\t\t{0} = {1}", field.Name, string.Join("", elements));
                }                
            }
            sw.WriteLine("\t\t\t};");
            sw.WriteLine("\t\t}");
            sw.WriteLine("\t}");
            sw.WriteLine();
            sw.WriteLine();

        }

        private void WriteMappings(DataTable table, StreamWriter sw, IEnumerable<FieldDefinition> fields)
        {            
            var className = GetFieldName(table.TableName);

            sw.WriteLine("\t//NHIBERNATE MAPPING");
            sw.WriteLine("\tinternal class {0}Mapping : ClassMapping<{0}>", className);
            sw.WriteLine("\t{");
            sw.WriteLine("\t\tpublic {0}Mapping()", className);
            sw.WriteLine("\t\t{");
            sw.WriteLine("\t\t\t//Schema(\"{0}\");", "TODO");

            sw.WriteLine("\t\t\tTable(\"{0}\");", table.TableName);
            sw.WriteLine("\t\t\tLazy(false);");

            foreach (var field in fields)
            {
                if (field.Name == "Id")
                {
                    sw.WriteLine("\t\t\tId(prop => prop.Id, map =>");
                    sw.WriteLine("\t\t\t{");
                    sw.WriteLine("\t\t\t\tmap.Column(\"{0}\");", field.DbFieldName);
                    //todo sequences
                    sw.WriteLine("\t\t\t\t//map.Generator(Generators.Sequence, gmap => gmap.Params(new {sequence = \"DATA_FILE_ID_SEQ\"}));");
                    sw.WriteLine("\t\t\t});");
                    continue;
                }
                sw.WriteLine("\t\t\tProperty(prop => prop.{0}, map => map.Column(\"{1}\"));", field.Name, field.DbFieldName);
            }
            sw.WriteLine("\t\t}");
            sw.WriteLine("\t}");
            sw.WriteLine("");
            sw.WriteLine("");

        }

        private IEnumerable<FieldDefinition> GetFields(DataTableReader reader)
        {
            var result = new List<FieldDefinition>();
            
            var schema = reader.GetSchemaTable();
            var table = schema.TableName;
            
            var idx = 0;
            foreach (DataRow row in schema.Rows)
            {
                var field = new FieldDefinition {TableName = table};
                if (table.Contains("."))
                {
                    var splits = table.Split('.');
                    field.TableName = splits[1];
                    field.ClassName = GetFieldName(splits[1]);
                    field.TableSchema = splits[0];
                }
                field.DbFieldName = row["ColumnName"].ToString();
                field.Length = (int)row["ColumnSize"];
                field.AllowsNull = (bool)row["AllowDBNull"];

                if (field.DbFieldName.ToUpper().EndsWith("_ID") && idx == 0)
                {
                    //this is the PK
                    field.Name = "Id";
                }
                else
                {
                    field.Name = GetFieldName(field.DbFieldName);
                }
                field.Type = GetDataType(row["DataType"].ToString(), field.AllowsNull);
                if (field.Type.StartsWith("short"))
                {
                    if (MessageBox.Show($"[{field.Name}] is this field intended to be used as a bool?",
                        "Possible Bool Value",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        field.IsShortBool = true;
                    }
                }
                result.Add(field);
                idx++;
            }
            return result;
        }

        private string GetDataType(string input, bool allownull)
        {
            //https://msdn.microsoft.com/en-us/library/ya5y69ds.aspx            
            if (input.StartsWith("System."))
            {
                var result = input.Replace("System.", "");
                if (_fieldTypes.TryGetValue(result, out var found))
                {
                    if (found != "string" && allownull)
                    {
                        return string.Concat(found, "?");
                    }
                    return found;
                }
                return result;
            }
            return input;
        }

        //todo this can probably be replaced with ToCsharpy
        private string GetFieldName(string input)
        {
            var result = "";
            for (var i = 0; i < input.Length; i++)
            {
                if (i == 0)
                {
                    result += input[i].ToString().ToUpper();
                    continue;
                }
                if (input[i] == '_')
                {
                    continue;
                }
                if (input[i - 1] == '_')
                {
                    result += input[i].ToString().ToUpper();
                    continue;
                }
                result += input[i].ToString().ToLower();
            }
            return result;
        }

    }
}
