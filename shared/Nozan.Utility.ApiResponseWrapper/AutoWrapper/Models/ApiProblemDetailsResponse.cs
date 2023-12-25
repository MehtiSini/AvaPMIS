using Microsoft.AspNetCore.Mvc;

namespace AvaPMIS.Gateway.AutoWrapper.Models
{
    public class ApiProblemDetailsResponse: ProblemDetails
    {
        public bool IsError { get; set; }
    }
}
