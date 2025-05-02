namespace Business.Communication;

public class BaseResponse<T>
{
    #region Property
    public bool Success { get; private set; }
    public List<string> Message { get; private set; }
    public T Resource { get; private set; }
    #endregion

    #region Constructor
    public BaseResponse(bool isSuccess)
    {
            Resource = default;
            Success = isSuccess;
            Message = isSuccess ? new List<string>() { "Success" } : new List<string>() { "Fault" };
        }

    public BaseResponse(T resource)
    {
            Success = true;
            Message = new List<string>() { "Success" };
            Resource = resource;
        }

    public BaseResponse(string message)
    {
            Success = false;
            Resource = default;

            if (!string.IsNullOrWhiteSpace(message))
            {
                Message = new List<string>() { message };
            }
        }

    public BaseResponse(List<string> messages)
    {
            this.Success = false;
            this.Resource = default;
            this.Message = messages ?? new List<string>() { "Fault" };
        }
    #endregion
}