using Business.Communication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Controllers.Config;

public static class InvalidResponseFactory
{
    public static IActionResult ProduceErrorResponse(ActionContext context)
    {
            var errors = context.ModelState.GetErrorMessages();
            var response = new BaseResponse<object>(messages: errors);

            return new BadRequestObjectResult(response);
        }
}

public static class ModelStateExtensions
{
    public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        => dictionary.SelectMany(m => m.Value.Errors)
            .Select(m => m.ErrorMessage)
            .ToList();
}