using API.Resources.SystemData;

namespace API.Results;

public record DeleteResult<T> : BaseResult<T>
{
    #region Properties

    [JsonPropertyName("totalRequest")]
    public int? TotalRequest { get; set; }

    [JsonPropertyName("totalDeleted")]
    public int? TotalDeleted { get; set; }

    #endregion

    #region Constructor

    public DeleteResult() : base()
    {
    }

    public DeleteResult(int totalRequest, int totalDeleted) : base()
    {
        this.TotalRequest = totalRequest;
        this.TotalDeleted = totalDeleted;
    }

    public DeleteResult(int totalRequest, int totalDeleted, CodeMessage codeMessage, string? message = "") : base(codeMessage, message)
    {
        this.TotalRequest = totalRequest;
        this.TotalDeleted = totalDeleted;
    }

    #endregion
}