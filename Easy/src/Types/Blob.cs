using System.Text;

namespace Easy.Types;

public class Blob : ISqliteType<byte[]>
{
    public bool Validate(string data)
    {
        return true;
    }

    public byte[] Convert(string data)
    {
        return Encoding.UTF8.GetBytes(data);
    }
}