using Business.Resources.SystemData;
using Business.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Controllers.Config;

public static class InvalidResponseFactory
{
    public static IActionResult ProduceErrorResponse(ActionContext context)
    {
        var error = context.ModelState.GetErrorMessages();
        var response = new BaseResult<object>(CodeMessage._3001);

        return new BadRequestObjectResult(response);
    }

    public static string? GetErrorMessages(this ModelStateDictionary dictionary)
        => dictionary.SelectMany(m => m.Value.Errors).Select(m => m.ErrorMessage).FirstOrDefault();
}