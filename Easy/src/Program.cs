using System;
using Easy.src.Query;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Easy.Session;
using Easy.src.Schema;
public class Program
{

    public static void Main(string[] args)
    {
        
        string laptop_path = "C:\\Users\\shirt\\Easy\\Easy\\src\\test.db";
        string desktop_path = "C:\\Users\\shirt\\EasySQLite\\Easy\\src\\test.db";

        Session session = Session.Instance;

        session.OpenDb(desktop_path);

        session.BuildSchema();

        Console.WriteLine(session.GetSchema());

        Console.ReadKey();

        var table = session.GetTable("User");
        var table2 = session.GetTable("Items");
        var condition1 = new Condition(table.GetColumn("UserID"), "1", Condition.Equal);
        var condition2 = new Condition(table.GetColumn("Name"), "Patrik", Condition.Equal);
        var condition3 = new Condition(table2.GetColumn("ItemName"), "Gold", Condition.Equal);

        var columns = new List<string>() {"Age", "Name", "JoinDate"};

        var select = new Select(columns).From(table).Where(condition1.And(condition2)).OrderBy(columns, Select.ASC);
        var select2 = new Select(columns).From(select).Where(condition2.And(condition3));
        var insert =
            new Insert(table, new List<Column>() {table.GetColumn("UserID")}).Values(new List<string>() {"hej"});
        Console.WriteLine(select2.QueryString);
        Console.WriteLine(insert.QueryString);

        Console.ReadKey();

        var result = session.ExecuteQuery(select);

        foreach (var r in result)
        {
            Console.WriteLine($"Value: {r}, Type: {r.GetType()}");
        }

    }

}