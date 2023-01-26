namespace Easy.src.Query;

public struct TableInfoQuery
{
    public static readonly string Query = "SELECT t.name AS tbl_name, c.name, c.type, c.pk\r\nFROM sqlite_master AS t,\r\n pragma_table_info(t.name) AS c\r\n  WHERE t.type = 'table';";
    public static readonly int TableNameIndex = 0;
    public static readonly int ColumnNameIndex = 1;
    public static readonly int ColumnTypeIndex = 2;
    public static readonly int ColumnPkIndex = 3;

    
}