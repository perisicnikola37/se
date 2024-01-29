namespace Contracts.Dto;

public record EmailRequestDto
{
    public string? ToEmail { get; set; }
}