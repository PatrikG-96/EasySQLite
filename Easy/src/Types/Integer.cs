namespace Easy.Types;

public class Integer : ISqliteType<int>
{
    public bool Validate(string data)
    {
        try
        {
            int.TryParse(data, out var val);
            return true;
        }
        catch 
        {
            return false;
        }
    }

    public int Convert(string data)
    {
        int.TryParse(data, out var value);
        return value;
    }
}