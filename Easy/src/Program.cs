using System;
using Easy.src.Query;
using System.Data.SQLite;
using System.Collections.Generic;


public class Program
{

    public static void Main(string[] args)
    {
        SQLiteConnection conn = new SQLiteConnection($"Data Source=C:\\Users\\shirt\\simple_pw_manager\\Easy\\Easy\\src\\test.db;Version=3;New=False;");
        
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
    }

}