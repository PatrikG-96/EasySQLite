namespace Easy.src.Schema;

public class Schema
{

    private Dictionary<string, Table> _tables;
    
    private List<ForeignKey> _foreignKeys;

    public Schema()
    {
        _tables = new Dictionary<string, Table>();
        _foreignKeys = new List<ForeignKey>();
    }

    public void AddTable(string name, Table table)
    {
        _tables.Add(name, table);
    }

    public void AddForeignKey(ForeignKey foreignKey)
    {
        _foreignKeys.Add(foreignKey);
    }

    public List<string> GetTableNames()
    {
        return _tables.Keys.ToList();
    }

}