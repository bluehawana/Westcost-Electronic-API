using api.Data;
using api.Models;
using api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
  [ApiController]
  [Route("api/v1/categories")]
  public class CategoriesController : ControllerBase
  {
    private readonly DataContext _context;
    public CategoriesController(DataContext context)
    {
      _context = context;
    }

    [HttpPost()]
    public async Task<ActionResult<PostCategoryViewModel>> AddCategory(PostCategoryViewModel model)
    {
      var newCategory = new Category { CategoryName = model.CategoryName };

      if (_context.Categories is null)
      {
        return StatusCode(500);
      }
      await _context.Categories.AddAsync(newCategory);
      await _context.SaveChangesAsync();

      var category = new CategoryViewModel
      {
        CategoryId = newCategory.Id,
        CategoryName = newCategory.CategoryName
      };
      return StatusCode(201, category);
    }

    [HttpGet()]
    public async Task<ActionResult<IList<CategoryViewModel>>> ListCategories()
    {

      if (_context.Categories is null) return StatusCode(500);

      var result = await _context.Categories.Include(c => c.Products).ToListAsync();
      var categories = new List<CategoryViewModel>();

      foreach (var c in result)
      {
        categories.Add(new CategoryViewModel
        {
          CategoryId = c.Id,
          CategoryName = c.CategoryName ?? "",
          Products = c.Products.ToList().ConvertAll(c => new CategoryProductViewModel { ProductId = c.Id, ProductName = c.ProductName, Price = c.Price })
        });
      }
      return Ok(categories);
    }
  }
}