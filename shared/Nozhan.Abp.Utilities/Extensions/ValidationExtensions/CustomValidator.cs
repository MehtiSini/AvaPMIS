using System;
using Nozhan.Abp.Utilities;
using FluentValidation;
using FluentValidation.Validators;

namespace Nozhan.Abp.Utilities.Extensions.ValidationExtensions
{
    /// <summary>
    /// Ensures that the property value is a valid guid
    /// </summary>
    public class CustomValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        public Type CustomValidatorType { get; set; }
        public ICustomValidator CustomValidatorInstance { get; set; }
        public CustomValidator(Type customValidatorType)
        {
            CustomValidatorType = customValidatorType;
            CustomValidatorInstance = customValidatorType.CreateInstance<ICustomValidator>();
        }
        /// <summary>Validates a specific property value.</summary>
        /// <param name="context">The validation context. The parent object can be obtained from here.</param>
        /// <param name="value">The current property value to validate</param>
        /// <returns>True if valid, otherwise false.</returns>
        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            CustomValidatorInstance.Value = value;
            return CustomValidatorInstance.IsValid(value);
        }

        public override string Name => "CustomValidator";

        protected override string GetDefaultMessageTemplate(string errorCode)
        {

            var error = CustomValidatorInstance.FormatErrorMessage(errorCode);

            return error;
        }


    }

    /// <summary>
    /// Custom Validator
    /// </summary>
    public class CustomValidatorAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
    {
        /// <summary>Gets a value that indicates whether the attribute requires validation context.</summary>
        /// <returns>true if the attribute requires validation context; otherwise, false.</returns>
        public override bool RequiresValidationContext {
            get { return true; }
        }
        public Type CustomValidatorType { get; set; }
        public ICustomValidator CustomValidator { get; set; }
        public CustomValidatorAttribute(Type customValidatorType) : base()
        {
            CustomValidatorType = customValidatorType;
            CustomValidator = customValidatorType.CreateInstance<ICustomValidator>();
        }

        /// <summary>Checks that the value of the data field is valid.</summary>
        /// <param name="value">The data field value to validate.</param>
        /// <returns>true always.</returns>
        public override bool IsValid(object value)
        {
            CustomValidator.Value = value;
            return CustomValidator.IsValid(value);
        }
        public override string FormatErrorMessage(string name)
        {

            var error = CustomValidator.FormatErrorMessage(name);
            if (!string.IsNullOrEmpty(error))
                return error;
            EnsureErrorMessageResource();
            return base.FormatErrorMessage(name);
        }

        public virtual void EnsureErrorMessageResource()
        {

        }

    }
    public interface ICustomValidator
    {
        object Value { get; set; }
        /// <summary>
        /// Determine whether value is valid or not
        /// </summary>
        /// <param name="value">the value to validate</param>
        /// <returns></returns>
        bool IsValid(object value);

        /// <summary>
        /// Generating error message
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string FormatErrorMessage(string name);

    }

    public abstract class CustomValidator : ICustomValidator
    {
        public object Value { get; set; }

        /// <inheritdoc />
        public abstract bool IsValid(object value);

        /// <summary>
        /// Generating error message
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual string FormatErrorMessage(string name)
        {
            var errorMessage = string.Format("Validation for {0} property failed!", name);
            return errorMessage;
        }
    }
}