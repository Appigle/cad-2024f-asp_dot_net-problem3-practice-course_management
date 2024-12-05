using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PA3.Utilities;

namespace PA3.Models
{
  public enum StudentStatus
  {
    ELIGIBLE,
    ENROLLED,
    NOTELIGIBLE
  }

  public class Student
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Display(Name = "Student ID")]
    public int StudentID { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [StringLength(50)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Address is required")]
    [StringLength(100)]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Postal code is required")]
    [StringLength(6)]
    [RegularExpression(@"^[A-Za-z]\d[A-Za-z]\d[A-Za-z]\d$",
        ErrorMessage = "Please enter a valid Canadian postal code (e.g., A1A1A1)")]
    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    [StringLength(50)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Type is required")]
    [StringLength(30)]
    public string Type { get; set; } = string.Empty;

    [Required]
    [EnumDataType(typeof(StudentStatus))]
    public StudentStatus Status { get; set; } = StudentStatus.ELIGIBLE;

    [Required]
    public int ProgramID { get; set; }

    [Required]
    public int CityID { get; set; }

    public virtual UGProgram? Program { get; set; }
    public virtual City? City { get; set; }
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    public virtual FinancialStatement? FinancialStatement { get; set; }

    [Display(Name = "Student Name")]
    public string FullName => $"{LastName}, {FirstName}";

    [Display(Name = "Course Load")]
    public int CourseLoad => Courses?.Count ?? 0;

    [Display(Name = "Is FullTime")]
    public bool IsFullTime => CourseLoad >= 3;

    [Display(Name = "Student Address")]
    public string FullAddress => $"{Address}, {City?.Name}, {Province}, {PostalCode}";

    public string Province { get; set; } = string.Empty;

    [Display(Name = "Total Amount Owed")]
    [DataType(DataType.Currency)]
    public decimal Balance
    {
      get
      {
        if (FinancialStatement == null)
          return 0;

        const decimal TAX_RATE = 0.129m; // 12.9%

        // Sum all statement entries
        decimal entryTotal = FinancialStatement.Entries?
            .Sum(entry => entry.Value) ?? 0m;

        // Get fee policy amounts
        var policy = FinancialStatement.FeePolicy;
        if (policy != null)
        {
          decimal policyTotal = policy.TuitionFee +
                              policy.RegistrationFee +
                              policy.FacilitiesFee +
                              policy.UnionFee;

          // Add policy total to entry total
          decimal subtotal = entryTotal + policyTotal;

          // Calculate tax
          decimal tax = subtotal * TAX_RATE;

          // Return total including tax
          return subtotal + tax;
        }

        // If no policy, just calculate tax on entries
        decimal entryTax = entryTotal * TAX_RATE;
        return entryTotal + entryTax;
      }
    }
  }
}