using System;
using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Validation;

namespace Microsoft.Gaming.PlayFab.CommandLine
{
    internal class BaseOptionValidator : IOptionValidator
    {
        private CommandOption _newOption;
        private Func<object, (bool IsValid, string ErrorMessage)> _validator;

        public BaseOptionValidator(
            CommandOption newOption,
            Func<object, (bool IsValid, string ErrorMessage)> validator)
        {
            _newOption = newOption;
            _validator = validator;
        }

        public ValidationResult GetValidationResult(CommandOption option, ValidationContext context)
        {
            bool isValid = false;
            string errorMessage = "";

            try
            {
                if (!option.HasValue())
                {
                    return ValidationResult.Success;
                }

                (bool IsValid, string ErrorMessage) response = _validator.Invoke(option.Value());

                isValid = response.IsValid;

                if (!isValid)
                {
                    errorMessage = response.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                isValid = false;
                errorMessage = ex.ToString();
            }

            if (isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(errorMessage);
            }
        }
    }
}