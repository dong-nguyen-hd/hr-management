using Business.Resources;
using Microsoft.Extensions.Options;

namespace Business.Services;

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