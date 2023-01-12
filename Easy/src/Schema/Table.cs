namespace Easy.src.Schema;

public class Table
{
    public string Name { get; set; }

    private List<Column> Columns;
    private List<Column> PrivateKey;

    public Table(string name)
    {
        Name = name;
        Columns = new List<Column>();
        PrivateKey = new List<Column>();
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
            PrivateKey.Add(column);
        }
    }

    public Column GetColumn(string name)
    {
        foreach (var column in Columns)
        {
            if (column.Name == name) return column;
        }

        // error
        return null;
    }

}