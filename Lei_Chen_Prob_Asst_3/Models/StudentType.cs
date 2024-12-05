using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA3.Models
{
  public class StudentType
  {
    public int ID { get; set; }

    [Required]
    [StringLength(30)]
    public string Type { get; set; }

    // Navigation property (if you plan to link it to Students)
    // public virtual ICollection<Student>? Students { get; set; }
  }
}