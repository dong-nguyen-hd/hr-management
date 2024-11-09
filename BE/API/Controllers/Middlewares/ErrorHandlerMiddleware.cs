using System.Net;
using API.Extensions;
using API.Resources.Exceptions;
using API.Resources.SystemData;
using API.Results;
using Business.Extensions.AddConfig;
using FluentValidation;

namespace API.Controllers.Middlewares;

public sealed class ErrorHandlerMiddleware(RequestDelegate next)
{
    public const string ErrorHandlerMiddlewareContext = nameof(ErrorHandlerMiddleware);
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
            
            // Xử lí cho mã 404
            if (context.Response.StatusCode == 404)
            {
                var response = context.Response;
                response.ContentType = MimeType.JSON;
                await response.WriteAsync(new BaseResult<object>(CodeMessage._3004).MySerialize());
            }
        }
        catch (Exception error)
        {
            ErrorHandlerMiddlewareContext.LogWithContext().Error(error, error.Message);
            
            var response = context.Response;
            response.ContentType = MimeType.JSON;
            BaseResult<object> result;

            // Using switch for custom exception
            switch (error)
            {
                // Add custom exception code below!
                case TaskCanceledException ex1:
                case OperationCanceledException ex2:
                    response.StatusCode = (int)HttpStatusCode.GatewayTimeout;
                    result = new(CodeMessage._3006);
                    break;
                case ValidationException:
                case BadRequestException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    result = new(CodeMessage._3001);
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result = new(CodeMessage._3005);
                    break;
            }

            await response.WriteAsync(result.MySerialize());

            throw;
        }
    }
}