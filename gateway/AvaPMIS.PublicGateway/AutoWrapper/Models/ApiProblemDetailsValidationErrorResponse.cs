﻿using AvaPMIS.PublicGateway.AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace AvaPMIS.PublicGateway.AutoWrapper.Models
{
    public class ApiProblemDetailsValidationErrorResponse: ProblemDetails
    {
        public bool IsError { get; set; }
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
    }
}
