using FluentValidation;

namespace Nozhan.Abp.Utilities.Extensions.ValidationExtensions
{
    public static class NationalCodeValidatorExtension
    {
        /// <summary>
        /// Defines a national code validator for the current rule builder that ensures that the specified string is a valid national code.
        /// </summary>
        public static IRuleBuilderOptions<T, string> NationalCode<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new NationalCodeValidator<T, string>());
        }
    }
}