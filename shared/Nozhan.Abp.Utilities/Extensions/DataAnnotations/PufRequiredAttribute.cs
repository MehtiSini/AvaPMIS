﻿using System.ComponentModel.DataAnnotations;
using Nozhan.Abp.Utilities.Resources;

namespace Nozhan.Abp.Utilities.Extensions.DataAnnotations
{
    public class PufRequiredAttribute : RequiredAttribute
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.PufRequiredAttribute" /> class by using a specified maximum length.</summary>
        public PufRequiredAttribute() : base()
        {
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
                ErrorMessageResourceName = "FieldRequiredError";
            }
        }

    }
}