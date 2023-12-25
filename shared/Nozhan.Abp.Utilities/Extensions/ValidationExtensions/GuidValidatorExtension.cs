using FluentValidation;

namespace Nozhan.Abp.Utilities.Extensions.ValidationExtensions
{
    public static class GuidValidatorExtension
    {
        /// <summary>
        /// Defines a guid validator for the current rule builder that ensures that the specified string is a valid guid.
        /// </summary>
        public static IRuleBuilderOptions<T, string> Guid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new GuidValidator<T, string>());
        }
    }
}