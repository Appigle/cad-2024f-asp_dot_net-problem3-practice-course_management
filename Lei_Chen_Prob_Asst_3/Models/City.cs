using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PA3.Models
{
  public class City
  {
    public int ID { get; set; }

    [Required]
    [StringLength(30)]
    public string Name { get; set; }

    [Required]
    public int ProvinceID { get; set; }

    public virtual Province? Province { get; set; }
  }
}