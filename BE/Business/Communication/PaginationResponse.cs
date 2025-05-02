namespace Business.Communication;

public class PaginationResponse<T> : BaseResponse<T>
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
    public PaginationResponse(bool isSuccess) : base(isSuccess) { }
    public PaginationResponse(T resource) : base(resource) { }
    public PaginationResponse(string message) : base(message) { }
    public PaginationResponse(List<string> messages) : base(messages) { }
    #endregion
}