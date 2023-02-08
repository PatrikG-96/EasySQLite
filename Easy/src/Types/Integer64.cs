namespace Easy.Types;

public class Integer64 : ISqliteType<long>
{
    public bool Validate(string data)
    {
        try
        {
            long.TryParse(data, out var value);
            return true;
        }
        catch 
        {
            return false;
        }
    }

    public long Convert(string data)
    {
        long.TryParse(data, out var value);
        return value;
    }
}