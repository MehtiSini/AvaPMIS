

using Microsoft.IdentityModel.Tokens;

namespace AvaPMIS.PublicGateway.ApiResponseWrapper.DataModel
{
    public static class FluentValidationExtensions
    {
        public static IEnumerable<ValidationError> ToValidationErrors(this IEnumerable<FluentValidation.Results.ValidationFailure> ValidationFailures)
        {
            var res = new List<ValidationError>();
            if (ValidationFailures == null || ValidationFailures.IsNullOrEmpty())
                return res;
            ValidationFailures.ToList().ForEach(p =>
            {
                res.Add(new ValidationError(p.PropertyName, p.ErrorMessage));
            });
            return res;
        }
    }
}
