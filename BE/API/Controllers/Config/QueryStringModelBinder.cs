using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Controllers.Config;

class QueryStringModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var result = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (result == ValueProviderResult.None)
        {
            // Parameter is missing, interpret as string-empty
            bindingContext.Result = ModelBindingResult.Success(string.Empty);
        }
        else
        {
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, result);
            var rawValue = result.FirstValue;
            if (string.IsNullOrEmpty(rawValue?.Trim()))
            {
                // Value is empty, interpret as string-empty
                bindingContext.Result = ModelBindingResult.Success(string.Empty);
            }
            else
            {
                // Value is a valid string type, use that value
                bindingContext.Result = ModelBindingResult.Success(rawValue.ToString());
            }
        }

        return Task.CompletedTask;
    }
}

class QueryStringModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(string) &&
            context.BindingInfo.BindingSource != null &&
            context.BindingInfo.BindingSource.CanAcceptDataFrom(BindingSource.Query))
        {
            return new QueryStringModelBinder();
        }

        return null;
    }
}