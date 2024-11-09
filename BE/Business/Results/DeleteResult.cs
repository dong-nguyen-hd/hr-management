using System.Text.Json.Serialization;

namespace Business.Results;

public class DeleteResult<T> : BaseResult<T>
{
    #region Properties

    [JsonPropertyName("totalRequest")]
    public int TotalRequest { get; set; }

    [JsonPropertyName("totalDeleted")]
    public int TotalDeleted { get; set; }

    #endregion

    #region Constructor

    public DeleteResult(int totalRequest, int TotalDeleted, T resource) : base(resource)
    {
        this.TotalRequest = totalRequest;
        this.TotalDeleted = TotalDeleted;
    }

    public DeleteResult(string message) : base(message)
    {
    }

    public DeleteResult(List<string> messages) : base(messages)
    {
    }

    public DeleteResult(bool isSuccess) : base(isSuccess)
    {
    }

    #endregion
}