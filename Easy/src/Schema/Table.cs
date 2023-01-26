namespace Easy.src.Schema;

public class Table
{
    public string Name { get; set; }

    private List<Column> Columns;
    private List<Column> PrimaryKey;

    public Table(string name)
    {
        Name = name;
        Columns = new List<Column>();
        PrimaryKey = new List<Column>();
    }

    public List<Column> GetAllColumns()
    {
        return Columns;
    }

    public void AddColumn(Column column)
    {
        Columns.Add(column);

        if (column.IsPrimaryKey)
        {
            PrimaryKey.Add(column);
        }
    }

    public Column GetColumn(string name)
    {
        foreach (var column in Columns)
        {
            if (column.Name == name) return column;
        }
        Console.WriteLine($"Column {name} not found");
        // error
        return null;
    }

    public override string? ToString()
    {
        string output = new string('-', Name.Length + 2) + $"\n|{Name}|\n" ;

        string columns = "|" + string.Join("|", Columns) + "|";

        return output + columns;
        
    }

}