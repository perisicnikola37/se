namespace Domain.Exceptions
{
    public class InvalidAccountTypeException(string fileName) : Exception($"Invalid account type.")
    {
        public string FileName { get; } = fileName;
    }
}
