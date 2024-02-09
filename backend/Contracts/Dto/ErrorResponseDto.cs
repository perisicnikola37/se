namespace Contracts.Dto;

public record ErrorResponseDto
{
	public string Error { get; init; } = default!;
	public int StatusCode { get; init; }
	public string Path { get; init; } = default!;
	public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}