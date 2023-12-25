using System;
using FluentValidation;
using FluentValidation.Validators;

namespace Nozhan.Abp.Utilities.Extensions.ValidationExtensions
{
    /// <summary>
    /// Ensures that the property value is a valid guid
    /// </summary>
    public class GuidValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        /// <summary>Validates a specific property value.</summary>
        /// <param name="context">The validation context. The parent object can be obtained from here.</param>
        /// <param name="value">The current property value to validate</param>
        /// <returns>True if valid, otherwise false.</returns>
        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            var valueStr = value?.ToString();

            if (string.IsNullOrEmpty(valueStr))
            {
                return true;
            }

            valueStr = valueStr.Replace("{", "").Replace("}", "");
            Guid guid;
            var res = Guid.TryParse(valueStr, out guid);
            return res;
        }

        public override string Name => "GuidValidator";
        protected override string GetDefaultMessageTemplate(string errorCode)
            => "{PropertyName} is not a valid Guid!";

    }
}