using Easy.src.Schema;

namespace Easy.src.Query;

public class Insert : IQuery
{
    public string QueryString { get; set; }

    public Insert(Table table, List<Column> columns)
    {
        QueryString = $"INSERT INTO {table.Name} ({string.Join(",", columns.Select(r => r.Name))}) ";
    }

    public Insert Values(List<TypedValue> values)
    {
        QueryString += $"VALUES ({string.Join(",", values)})";
        return this;
    }

    public Insert Values(Select otherTable)
    {
        QueryString += otherTable.QueryString + " ";
        return this;
    }

}