using System.Data;
using System.Data.SQLite;
using Easy.src.Query;
using Easy.src.Schema;

namespace Easy.Session;

public class Session
{
    
    private SQLiteConnection? _connection;
    private Schema? _schema;

    private static readonly Lazy<Session> Lazy = new Lazy<Session>(() => new Session());

    public static Session Instance => Lazy.Value;

    private Session() {}

    public void OpenDb(string dbPath)
    {
        try
        {
            _connection = new SQLiteConnection($"Data Source={dbPath}");
            _connection.Open();
        }
        catch (Exception ex)
        {
            // do something
            throw;
        }
    }

    public bool IsOpen()
    {
        return _connection.State == ConnectionState.Open;
    }

    public void BuildSchema()
    {
        _schema = Schema.FromDbFile(_connection);
        
    }

    public Schema GetSchema()
    {
        return _schema;
    }

    public Table GetTable(string name)
    {
        return _schema.GetTable(name);
    }

    public List<object> ExecuteQuery(IQuery query)
    {
        var command = _connection.CreateCommand();
        command.CommandText = query.QueryString+";";
        Console.WriteLine(query.QueryString);
        var reader = command.ExecuteReader();

        var result = new List<object>();

        // reduce nesting

        while (reader.Read())
        {
            int numFields = reader.FieldCount;

            for (int i = 0; i < numFields; i++)
            {
                /**var row = new ResultRow()
                {
                    Name = reader.GetName(i),
                    Value = new TypedValue()
                    {
                        Value = reader.GetValue(i),
                        Type = Types.ANY
                    }
                    
                };**/
                
                result.Add(reader.GetValue(i));
            }
            
        }

        return result;
    }
    
}