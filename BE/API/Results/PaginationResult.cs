namespace API.Results;

public class PaginationResult<T> : BaseResult<T>
{
    #region Property
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int? FirstPage { get; set; }
    public int? LastPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public int? NextPage { get; set; }
    public int? PreviousPage { get; set; }
    #endregion

    #region Constructor
    public PaginationResult(bool isSuccess) : base(isSuccess) { }
    public PaginationResult(T resource) : base(resource) { }
    public PaginationResult(string message) : base(message) { }
    public PaginationResult(List<string> messages) : base(messages) { }
    #endregion
}