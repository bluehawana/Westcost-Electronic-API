using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.ViewModels
{
  public class CategoryViewModel
  {
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = "";
    public List<CategoryProductViewModel> Products { get; set; } = new List<CategoryProductViewModel>();
  }
}