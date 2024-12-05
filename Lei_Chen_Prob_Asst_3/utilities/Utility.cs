using System;
using Lei_Chen_Prob_Asst_3.Data;

namespace PA3.Utilities
{
  public static class Utility
  {
    public static DateTime FirstFridayOfFirstWeek(int year, int month)
    {
      // Get the first day of the month
      var firstDay = new DateTime(year, month, 1);

      // Calculate days until first Friday
      int daysUntilFriday = ((int)DayOfWeek.Friday - (int)firstDay.DayOfWeek + 7) % 7;

      // Return the first Friday
      return firstDay.AddDays(daysUntilFriday);
    }

    public static DateTime FirstMondayOfSecondWeek(int year, int month)
    {
      // Get the first day of the month
      var firstDay = new DateTime(year, month, 1);

      // Calculate days until first Monday
      int daysUntilFirstMonday = ((int)DayOfWeek.Monday - (int)firstDay.DayOfWeek + 7) % 7;

      // Add 7 days to get to second week
      int daysUntilSecondMonday = daysUntilFirstMonday + 7;

      // Return the first Monday of second week
      return firstDay.AddDays(daysUntilSecondMonday);
    }

    // Get province
    public static string ProvinceOfCity(string cityName, MvcCourseContext context)
    {
      if (string.IsNullOrEmpty(cityName))
        return "";
      var provinces = context.Cities
          .Where(c => c.Name == cityName)
          .Select(c => c.Province.Name)
          .Distinct()
          .ToList();
      return provinces.Count == 1 ? provinces.First() : "";
    }
  }
}