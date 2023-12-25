﻿using System;
using Nozhan.Abp.Utilities.Resources;

namespace Nozhan.Abp.Utilities.Extensions.DataAnnotations
{
    /// <summary>
    /// Validate persian shaba code format
    /// </summary>
    public class PersianShabaCodeValidatorAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        public PersianShabaCodeValidatorAttribute() : base()
        {
        }

        /// <summary>Checks that the value of the data field is valid.</summary>
        /// <param name="value">The data field value to validate.</param>
        /// <returns>true always.</returns>
        public override bool IsValid(object value)
        {
            var val = value?.ToString();
            if (val == null || val.Trim().Length == 0)
                return true;
            else
            {
                return val.IsValidIranShebaNumber();
            }
        }

        public override string FormatErrorMessage(string name)
        {
            EnsureErrorMessageResource();
            return base.FormatErrorMessage(name);
        }

        public void EnsureErrorMessageResource()
        {
            if (ErrorMessageResourceType == null)
            {
                ErrorMessageResourceType = typeof(FrameworkValidationMessages);
            }
            if (string.IsNullOrEmpty(ErrorMessageResourceName))
            {
                ErrorMessageResourceName = "InvalidShabaCodeError";
            }
        }

    }
}