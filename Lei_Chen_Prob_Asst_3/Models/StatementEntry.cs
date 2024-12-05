using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA3.Models
{

  public class StatementEntry
  {
    [Key]
    public int ID { get; set; }

    [Required]
    [StringLength(200)]
    public string Description { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Value must be non-negative")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Value { get; set; }

    // Foreign key
    [Required]
    public int FinancialStatementID { get; set; }

    // Navigation property
    [Required]
    public virtual FinancialStatement FinancialStatement { get; set; }
  }


}