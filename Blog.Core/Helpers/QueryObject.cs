namespace Blog.Core.Helpers;

public abstract class QueryObject
{
    public string? SortBy { get; set; } = null;
    public bool IsDecsending { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 12;
}