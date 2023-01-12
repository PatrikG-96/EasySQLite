using System;
using Easy.src.Query;
using System.Data.SQLite;
using System.Collections.Generic;


public class Program
{

    public static void Main(string[] args)
    {
        SQLiteConnection conn = new SQLiteConnection($"Data Source=C:\\Users\\shirt\\EasySQLite\\Easy\\src\\test.db;");
        
        conn.Open();

        Console.WriteLine(conn.DataSource);

        var cmd = conn.CreateCommand();
        cmd.CommandText =
            "SELECT t.name AS tbl_name, c.name, c.type, c.pk\r\nFROM sqlite_master AS t,\r\n     pragma_table_info(t.name) AS c\r\n  WHERE t.type = 'table';";
       // cmd.CommandText = "SELECT * FROM Items;";
        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            int numFields = reader.FieldCount;

            Console.WriteLine($"Table Name: {reader.GetValue(0)}");
            Console.WriteLine($"Field Name: {reader.GetValue(1)}");
            Console.WriteLine($"Field Type: {reader.GetValue(2)}");
            Console.WriteLine($"Is PK: {reader.GetValue(3)}");
            
        }

        Console.ReadKey();

        var cmd1 = conn.CreateCommand();

        cmd1.CommandText = "SELECT \r\n    m.name\r\n    , p.*\r\nFROM\r\n    sqlite_master m\r\n    JOIN pragma_foreign_key_list(m.name) p ON m.name != p.\"table\"\r\nWHERE m.type = 'table'\r\nORDER BY m.name\r\n;";

        var reader1 = cmd1.ExecuteReader();

        while (reader1.Read())
        {
            int numFields = reader1.FieldCount;

            for (int i = 0; i < numFields; i++)
            {
                Console.WriteLine($"Value: {reader1.GetValue(i)}");
            }
       
            //Console.WriteLine(numFields);
        }
        
        Console.ReadKey();

      
    }

}