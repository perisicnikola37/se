namespace Domain.Exceptions;

public class EmailException(string fileName) : Exception("Problem with a SMTP configuration or server is not working at the moment.")
{
    public string FileName { get; } = fileName;
}