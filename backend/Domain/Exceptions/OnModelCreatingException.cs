namespace Domain.Exceptions;

public class OnModelCreatingException(string fileName) : Exception("Error in method: onModelCreating.")
{
    public string FileName { get; } = fileName;
}