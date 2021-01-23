using Blacksmith.Assignment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blacksmith.Assignment.Validations
{
    public class CustomValidator : ValidationAttribute
    {
        private const string alphabeticRegEx= "^[a-zA-Z][a-zA-Z]*$";
        private const string alphaNumericRegEx = "^[a-zA-Z][a-zA-Z0-9 -,]*$";
        private const string emailRegEx = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
        private const string websiteRegEx = "^(http:\\/\\/www\\.|https:\\/\\/www\\.|http:\\/\\/|https:\\/\\/)?[a-z0-9]+([\\-\\.]{1}[a-z0-9]+)*\\.[a-z]{2,5}(:[0-9]{1,5})?(\\/.*)?$";
        public int MinValue { get; }
        public int MaxValue { get; }
        public string PropertyName { get; }

        public ValidationType[] ValidationTypes { get; }

        public string ConditionalPropertyName { get; set; }
        public object DesiredValue { get; set; }



        public CustomValidator(string controlName, int minValue, int maxValue, params ValidationType[] validationTypes)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            PropertyName = controlName;
            ValidationTypes = validationTypes;
        }

        public CustomValidator(string propertyName, int minValue, int maxValue,  string conditionalPropertyName, object desiredValue,
            params ValidationType[] validationTypes)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            PropertyName = propertyName;
            ValidationTypes = validationTypes;
            ConditionalPropertyName = conditionalPropertyName;
            DesiredValue = desiredValue;
        }

        public CustomValidator(string controlName, params ValidationType[] validationTypes)
        {
            
            PropertyName = controlName;
            ValidationTypes = validationTypes;
        }

        public string GetErrorMessage(ValidationType validationType)
        {
            switch (validationType)
            {
                case ValidationType.Length:
                    return $" {PropertyName} require minimum of {MinValue} and maximum of {MaxValue} characters\n";
                case ValidationType.Alphabetic:
                    return $" {PropertyName} must be alphabetical only\n";
                case ValidationType.AlphaNumeric:
                    return $" {PropertyName} must be alphanumeric only\n";
                case ValidationType.Number:
                    return $" {PropertyName} must be numbers only\n";
                case ValidationType.Webiste:
                case ValidationType.Email:
                    return $" {PropertyName} must be valid \n";
                default:
                    return string.Empty;
            }


        }

      

        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
            string errorMessages = string.Empty;

            if(IsConditionValid(validationContext))
            {

                foreach (ValidationType validationType in ValidationTypes)
                {
                    switch (validationType)
                    {
                        case ValidationType.Length:
                            errorMessages = ValidateLength(value, errorMessages);
                            break;
                        case ValidationType.Alphabetic:

                            if (!IsValidForamt(alphabeticRegEx, value.ToString()))
                            {
                                errorMessages += GetErrorMessage(ValidationType.Alphabetic);
                            }

                            break;

                        case ValidationType.AlphaNumeric:

                            if (!IsValidForamt(alphaNumericRegEx, value.ToString()))
                            {
                                errorMessages += GetErrorMessage(ValidationType.AlphaNumeric);
                            }

                            break;

                        case ValidationType.Number:

                            if (!IsValidNumber(value.ToString()))
                            {
                                errorMessages += GetErrorMessage(ValidationType.Number);
                            }

                            break;

                        case ValidationType.Webiste:

                            if (!IsValidForamt(websiteRegEx, value.ToString()))
                            {
                                errorMessages += GetErrorMessage(ValidationType.Webiste);
                            }

                            break;
                        case ValidationType.Email:

                            if (!IsValidForamt(emailRegEx, value.ToString()))
                            {
                                errorMessages += GetErrorMessage(ValidationType.Email);
                            }

                            break;


                    }
                }
            }

            if (errorMessages.Length > 0)
            {
                return new ValidationResult(errorMessages);
            }

            return ValidationResult.Success;
        }

        private string ValidateLength(object value, string errorMessages)
        {
            if (value.ToString().Length < MinValue || value.ToString().Length > MaxValue)
            {
                errorMessages += GetErrorMessage(ValidationType.Length);
            }

            return errorMessages;
        }

        private bool IsValidNumber(object value)
        {
            int number;
            int.TryParse((string)value, out number);
            return number >0;
        }
            

        private bool IsValidForamt(string pattern, string input)
        {
            Regex rg = new Regex(pattern);
            return rg.IsMatch(input);
        }

        private bool IsConditionValid(ValidationContext validationContext)
        {
            if (ConditionalPropertyName != null && DesiredValue != null)
            {
                Object instance = validationContext.ObjectInstance;
                Type type = instance.GetType();
                Object proprtyvalue = type.GetProperty(ConditionalPropertyName).GetValue(instance, null);
                if (IsValidNumber(proprtyvalue))
                    return (Convert.ToInt32(proprtyvalue) < Convert.ToInt32(DesiredValue));
                else
                    return true;
            }
            return true;
        }


    }

    

    public enum ValidationType
    {
        Length,
        Alphabetic,
        AlphaNumeric,
        Number,
        Webiste,
        Email
    }
}

