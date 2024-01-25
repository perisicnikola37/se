namespace Domain.Exceptions
{
    public class UnauthorizedException(string fileName) : Exception($"Conflict.")
    {
        public string FileName { get; } = fileName;
    }
}
