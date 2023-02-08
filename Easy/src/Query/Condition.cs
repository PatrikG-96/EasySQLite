using Easy.src.Schema;
using Microsoft.VisualBasic.CompilerServices;

namespace Easy.src.Query;

public class Condition
{
    private string _condition = string.Empty;

    public static string Equal = "=";
    public static string GreaterThen = ">";
    public static string LessThen = "<";
    public static string GreaterEqualThan = ">=";
    public static string NotEqual = "!=";
    public static string LessEqualThan = "<=";

    public Condition(string condition)
    {
        _condition = condition;
    }

    public Condition(string firstColumn, string secondColumn, string operatorString)
    {
        _condition = firstColumn + operatorString + secondColumn;
    }

    public Condition(Column firstColumn, Column secondColumn, string operatorString)
    {
        _condition = firstColumn.Name + operatorString + secondColumn;
    }

    public Condition(Column firstColumn, string value, string operatorString)
    {
        _condition = firstColumn.Name + operatorString + $"\"{value}\"";
    }

    public Condition And(Condition other)
    {
        _condition = $"({_condition}) AND ({other._condition})";
        return this;
    }

    public Condition Or(Condition other)
    {
        _condition = $"({_condition}) OR ({other._condition})";
        return this;
    }

    public Condition FromString(string conditionString)
    {
        return new Condition(conditionString);
    }

    public override string ToString()
    {
        return _condition;
    }
}