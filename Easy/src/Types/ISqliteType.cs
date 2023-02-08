namespace Easy.Types;

/// <summary>
/// Representation of a generic SQLite type.
/// </summary>
/// <typeparam name="T">The C# type to associate the SQLite type with.</typeparam>
public interface ISqliteType<T>
{
    /// <summary>
    /// Method <c>Validate</c> checks if the input string is a valid representation of an object representing an SQLite type.
    /// </summary>
    /// <param name="data">Input string to validate</param>
    /// <returns>True if the string input is valid, false otherwise</returns>
    bool Validate(string data);

    /// <summary>
    /// Method <c>Convert</c> attempts to convert the string input into this specific SQLite data type.
    /// </summary>
    /// <param name="data">Input string to attempt to convert to the SQLite type</param>
    /// 
    /// <returns>Result of converting string to SQLite type.</returns>
    T Convert(string data);

}