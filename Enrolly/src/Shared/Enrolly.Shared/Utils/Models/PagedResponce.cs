namespace Enrolly.Shared.Logging;

public class PagedResponce <T>
{
    public IEnumerable<T> Content { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int PagesCount { get; set; }
    public int TotalCount { get; set; }
    public string Next { get; set; }
}