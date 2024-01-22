namespace Contracts.Dto
{
    public class PagedResponse<T> : Response<T>
    {
        public PagedResponse(T data, int pageNumber, int pageSize)
            : base(data)  
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            FirstPage = null; 
            LastPage = null;  
            TotalPages = 0;   
            TotalRecords = 0; 
            NextPage = null;  
            PreviousPage = null; 
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri? FirstPage { get; set; }
        public Uri? LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri? NextPage { get; set; }
        public Uri? PreviousPage { get; set; }
    }
}
