// DateCalculationTests.cs
using Xunit;
using PA3.Utilities;

namespace PA3.Tests
{
  public class DateCalculationTests
  {
    [Theory]
    [InlineData(2024, 1, 5)]  // January 2024
    [InlineData(2024, 9, 6)]  // September 2024
    [InlineData(2044, 1, 1)]  // January 2044
    [InlineData(2424, 9, 6)]  // September 2424
    public void FirstFridayOfFirstWeek_ReturnsCorrectDate(int year, int month, int expectedDay)
    {
      // Act
      var result = Utility.FirstFridayOfFirstWeek(year, month);

      // Assert
      Assert.Equal(year, result.Year);
      Assert.Equal(month, result.Month);
      Assert.Equal(expectedDay, result.Day);
      Assert.Equal(DayOfWeek.Friday, result.DayOfWeek);
    }

    [Theory]
    [InlineData(2024, 5, 13)] // May 2024
    [InlineData(2025, 5, 12)] // May 2025
    [InlineData(2044, 5, 9)] // May 2044
    [InlineData(2424, 5, 13)] // May 2424
    public void FirstMondayOfSecondWeek_ReturnsCorrectDate(int year, int month, int expectedDay)
    {
      // Act
      var result = Utility.FirstMondayOfSecondWeek(year, month);

      // Assert
      Assert.Equal(year, result.Year);
      Assert.Equal(month, result.Month);
      Assert.Equal(expectedDay, result.Day);
      Assert.Equal(DayOfWeek.Monday, result.DayOfWeek);
    }
  }
}
