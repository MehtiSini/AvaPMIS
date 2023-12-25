﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Nozhan.Abp.Utilities.Resources;

namespace Nozhan.Abp.Utilities.Extensions.DataAnnotations
{
    public class RequiredEnumValidatorAttribute : RequiredAttribute
    {
        object Value { get; set; }
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            Value = value;
            var type = value.GetType();
            return type.IsEnum && Enum.IsDefined(type, value);
        }
        public override string FormatErrorMessage(string name)
        {
            EnsureErrorMessageResource();
            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, Value, name);
            //return base.FormatErrorMessage(name);
        }

        public void EnsureErrorMessageResource()
        {
            if (ErrorMessageResourceType == null)
            {
                ErrorMessageResourceType = typeof(FrameworkValidationMessages);
            }
            if (string.IsNullOrEmpty(ErrorMessageResourceName))
            {
                ErrorMessageResourceName = "RequiredEnumValidatorError";
            }
        }
    }
}