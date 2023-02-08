using Easy.src.Schema;

namespace Easy.src.Query;

public class Insert : IQuery
{
    public string QueryString { get; set; }

    public Insert(Table table, List<Column> columns) : this(table.Name, columns.Select(r => r.Name).ToList())
    { }

    public Insert(string tableName, List<string> targetColumns)
    {
        QueryString = $"INSERT INTO {tableName} ({string.Join(",", targetColumns)}) ";
    }

    public Insert Values(List<string> values)
    {
        QueryString += $"{string.Join(",", values)}";
        return this;
    }

    public Insert Values(Select otherTable)
    {
        QueryString += otherTable.QueryString + " ";
        return this;
    }

}