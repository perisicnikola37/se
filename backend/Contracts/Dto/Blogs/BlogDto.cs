namespace Contracts.Dto.Blogs;

public record BlogDto
{
    public IEnumerable<object>? Blogs { get; set; }
}