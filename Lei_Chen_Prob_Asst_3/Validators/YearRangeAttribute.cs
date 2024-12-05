using System;
using System.ComponentModel.DataAnnotations;

namespace PA3.Validators
{
  public class YearRangeAttribute : ValidationAttribute
  {
    private readonly int _yearsAhead;

    public YearRangeAttribute(int yearsAhead)
    {
      _yearsAhead = yearsAhead;
    }

    public override bool IsValid(object value)
    {
      if (value == null) return false;

      int currentYear = DateTime.Now.Year;
      int maxYear = currentYear + _yearsAhead;

      if (int.TryParse(value.ToString(), out int year))
      {
        return year >= currentYear && year <= maxYear;
      }

      return false;
    }

    public override string FormatErrorMessage(string name)
    {
      return $"The {name} must be between {DateTime.Now.Year} and {DateTime.Now.Year + _yearsAhead}";
    }
  }
}