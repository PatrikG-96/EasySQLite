using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text.RegularExpressions;
using Easy.src.Query;

namespace Easy.src.Schema;

public class Schema
{

    private Dictionary<string, Table> _tables;
    
    private List<ForeignKey> _foreignKeys;

    public Schema() : this(new Dictionary<string, Table>(), new List<ForeignKey>()) {}
    
    public Schema(Dictionary<string, Table> tables, List<ForeignKey> foreignKeys)
    {
        _tables = tables;
        _foreignKeys = foreignKeys;
    }

    private static Dictionary<string, Table> ReadTables(SQLiteDataReader reader)
    {
        Dictionary<string ,Table> tables = new();

        while (reader.Read())
        {
            var tableName = (string)reader.GetValue(TableInfoQuery.TableNameIndex);
            var fieldName = (string)reader.GetValue(TableInfoQuery.ColumnNameIndex);
            var fieldType = Regex.Replace((string)reader.GetValue(TableInfoQuery.ColumnTypeIndex), @"\((.*?)\)", "");
            var isPk = (long)reader.GetValue(TableInfoQuery.ColumnPkIndex) != 0;


            if (!Enum.TryParse(fieldType, out Types type))
            {
                Console.WriteLine($"Failed to parse type: {fieldType}");
                // do something
            }

            var c = new Column()
            {
                Name = fieldName,
                Type = type,
                IsPrimaryKey = isPk
            };

            if (!tables.ContainsKey(tableName))
            {
                tables.Add(tableName, new Table(tableName));
            }

            var table = tables[tableName];

            table.AddColumn(c);
        }

        return tables;
    }

    private static List<ForeignKey> ReadForeignKeys(SQLiteDataReader reader)
    {
        List<ForeignKey> fks = new();

        while (reader.Read())
        {

            ForeignKey fk = new()
            {
                FromColumn = (string)reader.GetValue(ForeignKeysQuery.FromColumnIndex),
                FromTable = (string)reader.GetValue(ForeignKeysQuery.FromTableIndex),
                ToColumn = (string)reader.GetValue(ForeignKeysQuery.ToColumnIndex),
                ToTable = (string)reader.GetValue(ForeignKeysQuery.ToTableIndex),
            };

            fks.Add(fk);

        }

        return fks;
    }

    public static Schema FromDbFile(SQLiteConnection connection)
    {
        if (connection.State == ConnectionState.Closed)
        {
            return null; // throw exception
        }

        var tableQuery = connection.CreateCommand();
        tableQuery.CommandText = TableInfoQuery.Query;

        var tableReader = tableQuery.ExecuteReader();

        var tableDict = ReadTables(tableReader);

        tableQuery.Dispose();

        var fkQuery = connection.CreateCommand();
        fkQuery.CommandText = ForeignKeysQuery.Query;

        var fkReader = fkQuery.ExecuteReader();

        var foreignKeys = ReadForeignKeys(fkReader);

        fkQuery.Dispose();

        var schema = new Schema(tableDict, foreignKeys);
        return schema;
    }

 

    public void AddTable(string name, Table table)
    {
        _tables.Add(name, table);
    }

    public Table GetTable(string name)
    {
        if (_tables.ContainsKey(name))
            return _tables[name];

        throw new Exception();
    }

    public void AddForeignKey(ForeignKey foreignKey)
    {
        _foreignKeys.Add(foreignKey);
    }

    public List<string> GetTableNames()
    {
        return _tables.Keys.ToList();
    }

    public override string? ToString()
    {

        string output = "TABLES:\n" + string.Join("\n", _tables.Values);
        output += "\n\nFOREIGN KEYS:\n" + string.Join("\n", _foreignKeys);


        return output;
    }

}