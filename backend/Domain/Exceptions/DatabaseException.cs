namespace Domain.Exceptions;

// TODO this exception revels the code structure and doesnt give to much info about what happened in the system
public class DatabaseException(string fileName) : Exception("Problem with a database.")
{
    public string FileName { get; } = fileName;
}