namespace Domain.Exceptions
{
    public class ConflictException(string fileName) : Exception($"Conflict.")
    {
        public string FileName { get; } = fileName;
    }
}
