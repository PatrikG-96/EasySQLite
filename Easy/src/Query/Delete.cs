using Easy.src.Schema;

namespace Easy.src.Query;

public class Delete : IQuery
{
    public string QueryString { get; set; }

    public Delete(Table table)
    {
        QueryString = $"DELETE FROM {table.Name} ";
    }

    public Delete Where(Condition condition)
    {
        QueryString += $" WHERE {condition}";
        return this;
    }

}