namespace Vega.Exceptions;

public class InvalidAccountTypeException : ConflictException
{
    public InvalidAccountTypeException(string message)
        : base(message)
    {
    }
}