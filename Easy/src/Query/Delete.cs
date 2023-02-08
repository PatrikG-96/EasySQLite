using Easy.src.Schema;

namespace Easy.src.Query;

public class Delete : IQuery
{
    public string QueryString { get; set; }

    public Delete(Table table) : this(table.Name) {}
    
    public Delete(string tableName)
    {
        QueryString = $"DELETE FROM {tableName} ";
    }

    public Delete Where(Condition condition) => Where(condition.ToString());


    public Delete Where(string conditionString)
    {
        QueryString += $" WHERE {conditionString}";
        return this;
    }
}