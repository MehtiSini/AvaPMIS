﻿using AvaPMIS.PublicGateway.AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AvaPMIS.PublicGateway.AutoWrapper.Extensions
{
    public static class ModelStateExtension
    {
        public static IEnumerable<ValidationError> AllErrors(this ModelStateDictionary modelState)
        {
            return modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage))).ToList();
        }
    }

}
