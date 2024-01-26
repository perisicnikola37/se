namespace Domain.Exceptions;

public class DatabaseException(string fileName) : Exception("Problem with a database fetching.")
{
    public string FileName { get; } = fileName;
}