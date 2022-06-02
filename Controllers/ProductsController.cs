using api.Data;
using api.Models;
using api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;
        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpPost()]
        public async Task<ActionResult<ProductViewModel>> AddProduct(PostProductViewModel model)
        {
            if (_context.Categories is null) return StatusCode(500);

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == model.CategoryName);

            if (category is null) return StatusCode(404, $"Kunde inte hitta någon kategori med namnet: {model.CategoryName}");

            var newProduct = new Product
            {
                Category = category,
                ProductName = model.ProductName,
                Price = model.Price
            };

            if (_context.Products is null) return StatusCode(500);

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            var product = new ProductViewModel
            {
                ProductId = newProduct.Id,
                ProductName = newProduct.ProductName,
                CategoryName = newProduct.Category.CategoryName ?? "Kategorinamn saknas",
                Price = newProduct.Price
            };

            return StatusCode(201, product);
        }

        [HttpGet()]
        public async Task<ActionResult<List<ProductViewModel>>> ListProducts()
        {
            if (_context.Products is null) return StatusCode(500);

            var result = await _context.Products.Include(c => c.Category).ToListAsync();
            var products = new List<ProductViewModel>();

            foreach (var p in result)
            {
                products.Add(new ProductViewModel { ProductId = p.Id, ProductName = p.ProductName, Price = p.Price, CategoryName = p.Category.CategoryName ?? "Kategorinamn saknas" });
            }

            return Ok(products);
        }
    }
}