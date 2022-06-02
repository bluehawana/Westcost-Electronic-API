using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
  public class Product
  {
    public int Id { get; set; }
    public string ProductName { get; set; } = "";
    public decimal Price { get; set; }
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; } = new Category();
  }
}