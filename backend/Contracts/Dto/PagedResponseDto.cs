namespace Contracts.Dto;

public class PagedResponseDto<T>(T data, int pageNumber, int pageSize) : Response<T>(data)
{
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
    public Uri? FirstPage { get; set; } = null;
    public Uri? LastPage { get; set; } = null;
    public int TotalPages { get; set; } = 0;
    public int TotalRecords { get; set; } = 0;
    public Uri? NextPage { get; set; } = null;
    public Uri? PreviousPage { get; set; } = null;
}