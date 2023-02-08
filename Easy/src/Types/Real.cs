namespace Easy.Types;

public class Real : ISqliteType<float>
{
    public bool Validate(string data)
    {
        try
        {
            float.TryParse(data, out var value);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public float Convert(string data)
    {
        float.TryParse(data, out var value);
        return value;
    }
}