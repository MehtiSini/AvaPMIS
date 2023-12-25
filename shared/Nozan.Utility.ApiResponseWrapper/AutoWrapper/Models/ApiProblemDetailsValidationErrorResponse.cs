using AvaPMIS.Gateway.AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace AvaPMIS.Gateway.AutoWrapper.Models
{
    public class ApiProblemDetailsValidationErrorResponse: ProblemDetails
    {
        public bool IsError { get; set; }
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}
