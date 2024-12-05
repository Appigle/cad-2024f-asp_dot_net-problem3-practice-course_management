using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PA3.Validators;
using PA3.Utilities;

namespace PA3.Models
{
  public class Course
  {
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "Course code is required")]
    [StringLength(50, ErrorMessage = "Course code cannot exceed 50 characters")]
    [Display(Name = "Course Code")]
    public string Code { get; set; }

    [Required(ErrorMessage = "Course title is required")]
    [StringLength(50, ErrorMessage = "Course title cannot exceed 50 characters")]
    [Display(Name = "Course Title")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Section number is required")]
    [Range(1, 9, ErrorMessage = "Section must be a single digit between 1 and 9")]
    [Display(Name = "Section Number")]
    public int Section { get; set; }

    [Required(ErrorMessage = "Term is required")]
    [StringLength(50, ErrorMessage = "Term cannot exceed 50 characters")]
    public string Term { get; set; }

    [Required(ErrorMessage = "Year is required")]
    [Display(Name = "Academic Year")]
    [Range(typeof(int), "2024", "2028", ErrorMessage = "Year must be between current year and 4 years in the future")]
    [YearRange(4, ErrorMessage = "Year must be between current year and 4 years in the future")]
    public int Year { get; set; }

    [Required]
    public int ProgramID { get; set; }

    public virtual UGProgram? Program { get; set; }

    [Display(Name = "Start Date")]
    public DateTime StartDate
    {
      get
      {
        return Term?.ToUpper() switch
        {
          "FALL" => Utility.FirstMondayOfSecondWeek(Year, 9),  // September
          "WINTER" => Utility.FirstMondayOfSecondWeek(Year, 1), // January
          "SPRING" => Utility.FirstFridayOfFirstWeek(Year, 5),  // May
          _ => DateTime.MinValue // Handle invalid term
        };
      }
    }

    [Display(Name = "Open")]
    public bool IsOpenToEnroll
    {
      get
      {
        var today = DateTime.Today;
        var enrollmentStart = StartDate.AddMonths(-3);  // 3 months before start
        var enrollmentEnd = StartDate.AddDays(14);      // 2 weeks after start

        return today >= enrollmentStart && today <= enrollmentEnd;
      }
    }
  }
}