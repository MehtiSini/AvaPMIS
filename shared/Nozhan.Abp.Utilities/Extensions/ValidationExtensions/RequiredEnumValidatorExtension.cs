using FluentValidation;

namespace Nozhan.Abp.Utilities.Extensions.ValidationExtensions
{
    public static class RequiredEnumValidatorExtension
    {
        /// <summary>
        /// Defines a custom validator that check property value is a valid enum
        /// </summary>
        public static IRuleBuilderOptions<T, TProperty> RequiredEnum<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new RequiredEnumValidator<T, TProperty>());
        }

    }
}