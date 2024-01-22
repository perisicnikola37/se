namespace Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message)
    {
    }

    private NotFoundException(string fieldName, string message)
        : base(message)
    {
        FieldName = fieldName;
    }

    public string FieldName { get; }

    public static NotFoundException Create(string fieldName, string message)
    {
        return new NotFoundException(fieldName, message);
    }

    public override string ToString()
    {
        return $"{{ \"errors\": {{ \"{FieldName}\": [\"{Message}\"] }} }}";
    }
}