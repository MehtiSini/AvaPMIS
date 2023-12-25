using AvaPMIS.PublicGateway.ApiResponseWrapper.DataModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AvaPMIS.PublicGateway.ApiResponseWrapper.Extensions
{
    public static class ModelStateExtension
    {
        public static IEnumerable<ValidationError> AllErrors(this ModelStateDictionary modelState)
        {
            return modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).ToList();
        }
    }

}
