using Easy.src.Schema;

namespace Easy.src.Query;

public struct TypedValue
{
    public dynamic Value;
    public Types Type;

    public override string? ToString()
    {
        return Type == Types.TEXT ? '"' + Value + '"' : Value;
    }
}