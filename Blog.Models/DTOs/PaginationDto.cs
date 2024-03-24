namespace Blog.Models.DTOs;

public class PaginationDto<T> where T : class
{
    public int CurrentPage { get; set; } = 1;
    
    public int TotalPages { get; set; } = 1;
    
    public int PageSize { get; set; } = 12;
    
    public int TotalCount { get; set; } = 0;
    
    public T Data { get; set; } = null!;
}