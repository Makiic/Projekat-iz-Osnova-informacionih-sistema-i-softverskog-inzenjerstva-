using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Oisisiproj.Validation
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                return new ValidationResult(false, "Field cannot be empty");
            }

            return ValidationResult.ValidResult;
        }
    }

    public class NumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                double r;
                if (double.TryParse(s, out r))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Only numbers are allowed.");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }

    public class PictureValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                Regex r = new Regex("^[^,]+(,[^,]+)*$");
                if (r.IsMatch(s))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, " wrong input format !!!");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }
    public class LocationValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                Regex r = new Regex("^[a-zA-Z]+(\\s+[a-zA-Z]+)*,\\s?[a-zA-Z]+(\\s+[a-zA-Z]+)*$");
                if (r.IsMatch(s))
                {
                    return new ValidationResult(true, null);
                }
                return new ValidationResult(false, "Field is required by: 'city, country' ");
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
        }
    }
    public class DateTimeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                DateTime dt;
                if (!DateTime.TryParseExact(s, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                {
                    return new ValidationResult(false, "Invalid date format. The expected format is 'M/d/yyyy h:mm:ss tt'.");
                }
                // Add your validation logic here, for example:
                if (dt < DateTime.Now)
                {
                    return new ValidationResult(false, "Date must be in the future.");
                }
                return ValidationResult.ValidResult;
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occurred.");
            }
        }
    }


}
