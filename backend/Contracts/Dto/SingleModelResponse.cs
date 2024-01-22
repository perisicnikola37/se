namespace Contracts.Dto;

public class Response<T>(T data)
{
    public T Data { get; set; } = data;
    public bool Succeeded { get; set; } = true;
    public string[]? Errors { get; set; } = null;
    public string? Message { get; set; } = string.Empty;
}