using Microsoft.AspNetCore.Mvc;

namespace AvaPMIS.PublicGateway.AutoWrapper.Models
{
    public class ApiProblemDetailsResponse: ProblemDetails
    {
        public bool IsError { get; set; }
    }
}
