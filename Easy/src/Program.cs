using System;
using Easy.src.Query;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Easy.src.Schema;
public class Program
{

    public static void Main(string[] args)
    {

        string laptop_path = "C:\\Users\\shirt\\Easy\\Easy\\src\\test.db";
        string desktop_path = "C:\\Users\\shirt\\EasySQLite\\Easy\\src\\test.db";

        Dictionary<string, Table> tables = new Dictionary<string, Table>();

        SQLiteConnection conn = new SQLiteConnection($"Data Source={laptop_path};");
        
        conn.Open();

        Console.WriteLine(conn.DataSource);

        var cmd = conn.CreateCommand();
        cmd.CommandText =
            "SELECT t.name AS tbl_name, c.name, c.type, c.pk\r\nFROM sqlite_master AS t,\r\n pragma_table_info(t.name) AS c\r\n  WHERE t.type = 'table';";
       // cmd.CommandText = "SELECT * FROM Items;";
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            int numFields = reader.FieldCount;

            string tableName = (string)reader.GetValue(0);
            string fieldName = (string)reader.GetValue(1);
            string fieldType = Regex.Replace((string)reader.GetValue(2), @"\((.*?)\)", "");
            bool isPk = (Int64)reader.GetValue(3) != 0;

            Types type;

            if (!Enum.TryParse(fieldType, out type))
            {
                Console.WriteLine($"Failed to parse type: {fieldType}");
            }

            Column c = new Column()
            {
                Name = fieldName,
                Type = type,
                IsPrimaryKey = isPk
            };

            if (!tables.ContainsKey(tableName))
            {
                tables.Add(tableName, new Table(tableName));
            }
            
            Table table = tables[tableName];

            table.AddColumn(c);
        }

        foreach (KeyValuePair<string, Table> pair in tables)
        {
            Console.WriteLine(pair.Value.Name);

            foreach (Column c in pair.Value.GetAllColumns())
            {
                Console.WriteLine($"{c.Name}:{c.Type}:{c.IsPrimaryKey}");
            }

        }

        Console.ReadKey();

        var cmd1 = conn.CreateCommand();

        cmd1.CommandText = "SELECT \r\n    m.name\r\n    , p.*\r\nFROM\r\n    sqlite_master m\r\n    JOIN pragma_foreign_key_list(m.name) p ON m.name != p.\"table\"\r\nWHERE m.type = 'table'\r\nORDER BY m.name\r\n;";

        var reader1 = cmd1.ExecuteReader();

        List<ForeignKey> fks = new List<ForeignKey>();

        while (reader1.Read())
        {
            int table = 3;
            int from = 4;
            int to = 5;

            int numFields = reader1.FieldCount;

            /**
            for (int i = 0; i < numFields; i++)
            {
                Console.WriteLine($"Value: {reader1.GetValue(i)}");
            }**/

            ForeignKey fk = new ForeignKey()
            {
                FromColumn = (string)reader1.GetValue(from),
                FromTable = (string)reader1.GetValue(0),
                ToColumn = (string)reader1.GetValue(to),
                ToTable = (string)reader1.GetValue(table),
            };

            fks.Add(fk);
            //Console.WriteLine(numFields);
        }

        foreach (ForeignKey fk in fks)
        {
            Console.WriteLine($"{fk.FromTable}:{fk.FromColumn}:{fk.ToTable}:{fk.ToColumn}");
        }
        
        Console.ReadKey();

      
    }

}