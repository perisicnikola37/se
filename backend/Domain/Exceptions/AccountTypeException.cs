namespace Domain.Exceptions;

public class InvalidAccountTypeException : Exception
{
    public InvalidAccountTypeException(string message)
        : base(message)
    {
    }
}