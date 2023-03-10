namespace Easy.src.Schema;

public class Column
{
    public string Name { get; set; }

    public Types Type { get; set; }

    public bool IsPrimaryKey { get; set; }

    public override string? ToString()
    {
        return $"{Name}:{Type}{(IsPrimaryKey ? $":PK" : "")}";
    }

}