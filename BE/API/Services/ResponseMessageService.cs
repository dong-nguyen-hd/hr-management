using Microsoft.Extensions.Options;

namespace API.Services;

public abstract class ResponseMessageService
{
    #region Property
    protected readonly ResponseMessage ResponseMessage;
    #endregion

    #region Constructor
    public ResponseMessageService(IOptionsMonitor<ResponseMessage> responseMessage)
    {
            this.ResponseMessage = responseMessage.CurrentValue;
        }
    #endregion
}