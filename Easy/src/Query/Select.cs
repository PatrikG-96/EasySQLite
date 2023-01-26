using System.Data.SQLite;
using System.Text;
using Easy.src.Schema;
using System.Data.SQLite;
using System.Linq;
namespace Easy.src.Query;

//TODO: Enforce the order of operations, ie don't allow Select.Where.From etc

public class Select : IQuery
{
    public static readonly int ASC = 1;
    public static readonly int DESC = 2;

    public string QueryString { get; set; }

    public Select(List<Column> columns)
    {
        QueryString = $"SELECT {string.Join(",", columns.Select(r => r.Name))} ";
    }

    public Select(List<string> columns)
    {
        QueryString = $"SELECT {string.Join(",", columns)} ";
    }

    public Select()
    {
        QueryString = "SELECT * ";
    }

    public Select Where(Condition condition)
    {
        QueryString += $"WHERE {condition} ";
        return this;
    }

    public Select From(Table table) => From(table.Name);

    public Select From(string tableName)
    {

        QueryString += $"FROM {tableName} ";
        return this;
    }

    public Select From(Select other)
    {
        QueryString += $"FROM ({other.QueryString}) ";
        return this;
    }

    public Select OrderBy(Column column, int order) => OrderBy(new List<Column>() { column }, order);

    public Select OrderBy(List<Column> columns, int order)
    {
        QueryString += $"ORDER BY {string.Join(",", columns.Select(r => r.Name))} {(order == ASC ? "ASC" : "DESC")} ";
        return this;
    }

    public Select OrderBy(List<string> columns, int order)
    {
        QueryString += $"ORDER BY {string.Join(",", columns)} {(order == ASC ? "ASC" : "DESC")} ";
        return this;
    }

    public Select Join(Table table, Join joinType, Condition condition) => Join(table.Name, joinType, condition);


    public Select Join(string tableName, Join joinType, Condition condition)
    {
        QueryString += $"{joinType} JOIN {tableName} ON {condition} ";
        return this;
    }


}