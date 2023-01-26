namespace Easy.src.Schema;

public class ForeignKey
{
    
    public string FromTable { get; set;}
    public string ToTable { get; set;}

    public string FromColumn { get; set;}

    public string ToColumn { get; set;}

    public override string ToString()
    {
        return $"{FromTable}:{FromColumn} - {ToTable}:{ToColumn}";
    }
}