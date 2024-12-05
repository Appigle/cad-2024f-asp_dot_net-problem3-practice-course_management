using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA3.Models
{
  public class FeePolicy
  {
    [Key]
    public int ID { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Category")]
    public string Category { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Fee must be non-negative")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Tuition Fee")]
    public decimal TuitionFee { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Fee must be non-negative")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Registration Fee")]
    public decimal RegistrationFee { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Fee must be non-negative")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Facilities Fee")]
    public decimal FacilitiesFee { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Fee must be non-negative")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Union Fee")]
    public decimal UnionFee { get; set; }

    // Navigation property for FinancialStatements using this policy
    public virtual ICollection<FinancialStatement>? FinancialStatements { get; set; } = new List<FinancialStatement>();
  }
}