using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA3.Models
{
  public class Province
  {
    public int ID { get; set; }

    [Required]
    [StringLength(30)]
    public string Name { get; set; }

    [Required]
    [StringLength(2)]
    public string Code { get; set; }

    // Navigation property
    public virtual ICollection<City>? Cities { get; set; }
  }
}