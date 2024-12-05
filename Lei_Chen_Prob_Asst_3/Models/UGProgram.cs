using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA3.Models
{
  public class UGProgram
  {
    public int ID { get; set; }

    [Required]
    [StringLength(5)]
    public string Code { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    // Navigation property
    public virtual ICollection<Course>? Courses { get; set; }
  }
}