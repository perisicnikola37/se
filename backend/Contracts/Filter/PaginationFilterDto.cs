namespace Contracts.Filter;

public class PaginationFilterDto(int pageNumber, int pageSize)
{
    public int PageNumber { get; set; } = pageNumber < 1 ? 1 : pageNumber;
    public int PageSize { get; set; } = pageSize > 10 ? 10 : pageSize;
}