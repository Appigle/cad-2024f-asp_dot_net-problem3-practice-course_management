using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA3.Models
{
  public class FinancialStatement
  {
    [Key]
    public int ID { get; set; }

    [Required]
    [Display(Name = "Last Updated")]
    [DataType(DataType.DateTime)]
    public DateTime LastChanged { get; set; }

    // Foreign keys
    [Required]
    public int StudentID { get; set; }

    [Required]
    public int FeePolicyID { get; set; }

    // Navigation properties
    [Required]
    public virtual Student Student { get; set; }

    [Required]
    public virtual FeePolicy FeePolicy { get; set; }

    public virtual ICollection<StatementEntry> Entries { get; set; } = new List<StatementEntry>();
  }
}