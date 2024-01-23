namespace Domain.Exceptions;

public abstract class ConflictException(string message) : Exception(message)
{
}

