using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Passport_MRZ.ViewModels
{
   public class ValidationRules : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {

            if (value is int)
            {
                int i;
                if (int.TryParse(value.ToString(), out i))
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false, "Please enter a valid value.");
                }
            }
            else if (value is string)
            {
                string a;
                if (value.ToString().Length > 0)
                {
                    return new ValidationResult(true, null);
                }
                else
                {
                    return new ValidationResult(false, "Please enter a valid value.");
                }

            }
            else if (value is DateTime)
            {
                return DateTime.TryParse((value ?? "").ToString(),
              CultureInfo.CurrentCulture,
              DateTimeStyles.AssumeLocal | DateTimeStyles.AllowWhiteSpaces,
              out _)
              ? ValidationResult.ValidResult
              : new ValidationResult(false, "Invalid date");
            }
            return new ValidationResult(false, "Please enter a value.");
        }
    }
}
