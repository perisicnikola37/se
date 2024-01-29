namespace Domain.Exceptions;

public class DatabaseException(string fileName) : Exception("Problem with a database.")
{
    public string FileName { get; } = fileName;
}