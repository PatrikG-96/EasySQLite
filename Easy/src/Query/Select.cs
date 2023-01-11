using System.Data.SQLite;
using System.Text;
using Easy.src.Schema;
using System.Data.SQLite;

namespace Easy.src.Query;

public class Select : IQuery
{
    public static readonly int ASC = 1;
    public static readonly int DESC = 2;

    public string QueryString { get; set; }

    public IResult Execute(SQLiteConnection connection)
    {
        throw new NotImplementedException();
    }

    public Select Where(ICondition condition)
    {
        throw new NotImplementedException();
    }

    public Select From(Table table) => From(table.Name);

    public Select From(string tableName)
    {
        throw new NotImplementedException();
    }

    public Select From(Select other)
    {
        throw new NotImplementedException();
    }

    public Select OrderBy(Column column, int order) => OrderBy(new List<Column>() { column }, order);

    public Select OrderBy(List<Column> columns, int order)
    {
        throw new NotImplementedException();
    }

    public Select Join(Table table, Join joinType, ICondition condition) => Join(table.Name, joinType, condition);


    public Select Join(string tableName, Join joinType, ICondition condition)
    {
        throw new NotImplementedException();
    }


}