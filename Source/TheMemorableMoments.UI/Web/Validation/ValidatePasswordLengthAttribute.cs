using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TheMemorableMoments.UI.Web.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateMinimumLengthAttribute : ValidationAttribute
    {
        private const string defaultErrorMessage = "'{0}' must be at least {1} characters long.";
        private readonly int _minCharacters;

        public ValidateMinimumLengthAttribute(int minLength): base(defaultErrorMessage)
        {
            _minCharacters = minLength;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                name, _minCharacters);
        }

        public override bool IsValid(object value)
        {
            string valueAsString = value as string;
            return (valueAsString != null && valueAsString.Length >= _minCharacters);
        }
    }
}