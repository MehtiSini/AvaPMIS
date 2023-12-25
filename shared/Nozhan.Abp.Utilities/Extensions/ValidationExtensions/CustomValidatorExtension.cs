using Nozhan.Abp.Utilities;
using FluentValidation;

namespace Nozhan.Abp.Utilities.Extensions.ValidationExtensions
{
    public static class CustomValidatorExtension
    {
        /// <summary>
        /// Defines a custom validator for the current rule builder that validate specefied object via an instance of <see cref="ICustomValidator"/>.
        /// </summary>
        public static IRuleBuilderOptions<T, TProperty> CustomValidator<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Type customValidatorType)
        {
            return ruleBuilder.SetValidator(new CustomValidator<T, TProperty>(customValidatorType));
        }

        /// <summary>
        /// Defines a custom validator for the current rule builder that validate specefied object via an instance of <see cref="ICustomValidator"/>.
        /// </summary>
        public static IRuleBuilderOptions<T, string> CustomValidator<T>(this IRuleBuilder<T, string> ruleBuilder, Type customValidatorType)
        {
            return ruleBuilder.SetValidator(new CustomValidator<T, object>(customValidatorType));
        }
    }
}