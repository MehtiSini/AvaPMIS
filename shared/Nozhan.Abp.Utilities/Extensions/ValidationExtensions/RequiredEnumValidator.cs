using System;
using System.Globalization;
using FluentValidation;
using FluentValidation.Validators;

namespace Nozhan.Abp.Utilities.Extensions.ValidationExtensions
{
    /// <summary>
    /// Ensures that the property value is a valid enum
    /// </summary>
    public class RequiredEnumValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        object Value { get; set; }
        public RequiredEnumValidator()
        {
        }
        /// <summary>Validates a specific property value.</summary>
        /// <param name="context">The validation context. The parent object can be obtained from here.</param>
        /// <param name="value">The current property value to validate</param>
        /// <returns>True if valid, otherwise false.</returns>
        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            if (value == null)
            {
                return false;
            }

            Value = value;
            var type = value.GetType();
            return type.IsEnum && Enum.IsDefined(type, value);
        }

        public override string Name => "RequiredEnumValidator";

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            var error = string.Format(CultureInfo.CurrentCulture,
                "The value {0} is not valid for {1}", Value, errorCode);
            return error;
        }


    }
}