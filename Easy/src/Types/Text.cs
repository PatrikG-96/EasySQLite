using System.Text;

namespace Easy.Types;

public class Text : ISqliteType<string>
{
   

    public bool Validate(string data)
    {
        return true;
    }

    public string Convert(string data)
    {
        return data;
    }
}