using System.Globalization;

namespace Easy.Types;

public class Date : ISqliteType<DateTime>
{

    private static readonly string ISO8601 = "yyyy-MM-dd";
    public bool Validate(string data)
    {
        DateTime.TryParseExact(data, ISO8601, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result);

        return result != DateTime.MinValue;
    }

    public DateTime Convert(string data)
    {
        DateTime.TryParseExact(data, ISO8601, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result);
        return result;
    }
}