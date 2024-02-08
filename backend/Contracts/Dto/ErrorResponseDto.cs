namespace Contracts.Dto;

public record ErrorResponseDto
{
    public string Error { get; init; }
    public int StatusCode { get; init; }
    public string Path { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}