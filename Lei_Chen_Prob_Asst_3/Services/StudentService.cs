using Lei_Chen_Prob_Asst_3.Data;
using PA3.Models;
using PA3.Utilities;

namespace PA3.Services
{
  public class StudentService
  {
    private readonly MvcCourseContext _context;

    public StudentService(MvcCourseContext context)
    {
      _context = context;
    }

    public string GetProvince(Student student)
    {
      if (student == null) return "";

      // First try to get province from city
      string provinceFromCity = Utility.ProvinceOfCity(student.City?.Name ?? "", _context);

      if (!string.IsNullOrEmpty(provinceFromCity))
        return provinceFromCity;

      // If city lookup fails, try postal code
      if (string.IsNullOrEmpty(student.PostalCode) || student.PostalCode.Length < 1)
        return "";

      return char.ToUpper(student.PostalCode[0]) switch
      {
        'A' => "Newfoundland and Labrador",
        'B' => "Nova Scotia",
        'C' => "Prince Edward Island",
        'E' => "New Brunswick",
        'G' or 'H' or 'J' => "Quebec",
        'K' or 'L' or 'M' or 'N' or 'P' => "Ontario",
        'R' => "Manitoba",
        'S' => "Saskatchewan",
        'T' => "Alberta",
        'V' => "British Columbia",
        'X' => "Northwest Territories/Nunavut",
        'Y' => "Yukon",
        _ => ""
      };
    }

    public bool IsStudentEligibleToEnroll(Student student)
    {
      return student != null &&
             (student.Status == StudentStatus.ELIGIBLE ||
              student.Status == StudentStatus.ENROLLED);
    }

    public void UpdateStudentStatus(Student student)
    {
      if (student == null) return;

      // If student has no courses, they should be ELIGIBLE (unless NOTELIGIBLE)
      if (student.CourseLoad == 0)
      {
        if (student.Status != StudentStatus.NOTELIGIBLE)
          student.Status = StudentStatus.ELIGIBLE;
      }
      else
      {
        // If student has courses, they should be ENROLLED
        if (student.Status != StudentStatus.NOTELIGIBLE)
          student.Status = StudentStatus.ENROLLED;
      }
    }
  }
}