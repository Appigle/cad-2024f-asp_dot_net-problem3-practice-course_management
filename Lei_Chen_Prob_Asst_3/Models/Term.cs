using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA3.Models
{
  public class Term
  {
    public int ID { get; set; }

    [Required]
    [StringLength(20)]
    public string Semester { get; set; }

    // Navigation property (if you plan to link it to Courses)
    public virtual ICollection<Course>? Courses { get; set; }
  }
}