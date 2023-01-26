namespace Easy.src.Query;

public struct ForeignKeysQuery
{
    public static string Query =
        "SELECT \r\n    m.name\r\n    , p.*\r\nFROM\r\n    sqlite_master m\r\n    JOIN pragma_foreign_key_list(m.name) p ON m.name != p.\"table\"\r\nWHERE m.type = 'table'\r\nORDER BY m.name\r\n;";

    public static readonly int FromTableIndex = 0;
    public static readonly int ToTableIndex = 3;
    public static readonly int FromColumnIndex = 4;
    public static readonly int ToColumnIndex = 5;
}