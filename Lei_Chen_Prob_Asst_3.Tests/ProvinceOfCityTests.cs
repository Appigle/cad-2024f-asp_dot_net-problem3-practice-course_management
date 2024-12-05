using PA3.Models;
using PA3.Utilities;
using Lei_Chen_Prob_Asst_3.Data;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace PA3.Tests
{
  public class ProvinceOfCityTests : IDisposable
  {
    private MvcCourseContext CreateNewContext()
    {
      var options = new DbContextOptionsBuilder<MvcCourseContext>()
          .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
          .Options;

      return new MvcCourseContext(options);
    }

    [Fact]
    public void ProvinceOfCity_Armstrong_ReturnsEmpty()
    {
      using var context = CreateNewContext();

      var bc = new Province { ID = 1, Name = "British Columbia", Code = "BC" };
      var on = new Province { ID = 2, Name = "Ontario", Code = "ON" };
      var ab = new Province { ID = 3, Name = "Alberta", Code = "AB" };
      context.Provinces.AddRange(bc, on, ab);
      context.SaveChanges();

      context.Cities.AddRange(
          new City { ID = 1, Name = "Armstrong", ProvinceID = bc.ID },
          new City { ID = 2, Name = "Armstrong", ProvinceID = on.ID },
          new City { ID = 3, Name = "Armstrong", ProvinceID = ab.ID }
      );
      context.SaveChanges();

      var result = Utility.ProvinceOfCity("Armstrong", context);
      Assert.Equal("", result);
    }

    [Fact]
    public void ProvinceOfCity_Auckland_ReturnsEmpty()
    {
      using var context = CreateNewContext();

      var on = new Province { ID = 1, Name = "Ontario", Code = "ON" };
      context.Provinces.Add(on);
      context.SaveChanges();

      context.Cities.Add(new City { ID = 1, Name = "Toronto", ProvinceID = on.ID });
      context.SaveChanges();

      var result = Utility.ProvinceOfCity("Auckland", context);
      Assert.Equal("", result);
    }

    [Fact]
    public void ProvinceOfCity_Toronto_CaseInsensitive_ReturnsOntario()
    {
      using var context = CreateNewContext();

      var on = new Province { ID = 1, Name = "Ontario", Code = "ON" };
      context.Provinces.Add(on);
      context.SaveChanges();

      context.Cities.Add(new City { ID = 1, Name = "Toronto", ProvinceID = on.ID });
      context.SaveChanges();

      var result = Utility.ProvinceOfCity("Toronto", context);
      Assert.Equal("Ontario", result);
    }

    [Fact]
    public void ProvinceOfCity_TorontoIsland_ReturnsOntario()
    {
      using var context = CreateNewContext();

      var on = new Province { ID = 1, Name = "Ontario", Code = "ON" };
      context.Provinces.Add(on);
      context.SaveChanges();

      context.Cities.Add(new City { ID = 1, Name = "toronto island", ProvinceID = on.ID });
      context.SaveChanges();

      var result = Utility.ProvinceOfCity("toronto island", context);
      Assert.Equal("Ontario", result);
    }

    [Fact]
    public void ProvinceOfCity_Ottawa_ReturnsOntario()
    {
      using var context = CreateNewContext();

      var on = new Province { ID = 1, Name = "Ontario", Code = "ON" };
      context.Provinces.Add(on);
      context.SaveChanges();

      context.Cities.Add(new City { ID = 1, Name = "OTTAWA", ProvinceID = on.ID });
      context.SaveChanges();

      var result = Utility.ProvinceOfCity("OTTAWA", context);
      Assert.Equal("Ontario", result);
    }

    [Fact]
    public void ProvinceOfCity_EmptyString_ReturnsEmpty()
    {
      using var context = CreateNewContext();
      var result = Utility.ProvinceOfCity("", context);
      Assert.Equal("", result);
    }

    public void Dispose()
    {
      // Cleanup handled by using statements
    }
  }
}